using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Service.Interfaces;
using Domain.Entities.UserEntities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Infrastructure.Services;
internal class TokensService
        (JwtSettings jwtSettings, UserManager<ApplicationUser> userManager, AppDbContext context) :
        ITokensService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AppDbContext _dbcontext = context;
    private readonly JwtSettings _jwtSettings = jwtSettings;

    #region Public Methods
    public async Task SaveRefreshTokenAsync(
        long userId,
        RefreshTokenDto refreshToken)
    {
        await _dbcontext.RefreshTokens
         .AddAsync(new RefreshToken
         {
             UserId = userId,
             Token = refreshToken.Token,
             CreationDate = refreshToken.CreationDate,
             ExpiryDate = refreshToken.ExpiryDate,
         });
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<TokensInfo> GenerateTokensAsync(ApplicationUser user)
    {
        var accessJwt = await GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();
        await SaveRefreshTokenAsync(user.Id, refreshToken);

        return new TokensInfo { UserId = user.Id, JWT = accessJwt, Refresh = refreshToken };
    }
    public async Task RemoveRefreshTokenAsync(string refreshToken)
    {
        var token = await _dbcontext.RefreshTokens.FirstOrDefaultAsync(t => t.Token.Equals(refreshToken));
        if (token is null) throw new Exception("refresh token is not found!!");

        _dbcontext.RefreshTokens.Remove(token);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task RemoveExpiredTokensAsync()
    {
        var rowsAffected = await _dbcontext.RefreshTokens
       .Where(t => DateTime.UtcNow >= t.ExpiryDate || t.RevokedOn.HasValue)
       .ExecuteDeleteAsync();

        if (rowsAffected > 0)
        {
            Console.WriteLine($"{rowsAffected} expired or revoked tokens removed.");
        }
    }
    public async Task<TokensInfo?> RefreshAsync(string accessToken, string refreshToken)
    {
        if (!await ValidateAccessTokenAsync(accessToken))
            throw new UnauthorizedException("Invalid access token!");

        if (!await ValidateRefreshTokenAsync(refreshToken))
            throw new UnauthorizedException("Invalid refresh token!");

        var rt = await _dbcontext.RefreshTokens
            .Include(rt => rt.User)
                .ThenInclude(u => u.ApplicationUserRoles)
                  .ThenInclude(ur => ur.Role)
                            .FirstOrDefaultAsync(t => t.Token.Equals(refreshToken));
        var user = rt?.User;
        if (user is null)
        {
            throw new NotFoundException("refresh token is not found!");
        }
        // Revoke the token
        rt.RevokedOn = DateTime.UtcNow;
        _dbcontext.RefreshTokens.Update(rt);
        await _dbcontext.SaveChangesAsync();

        return await GenerateTokensAsync(user);
    }
    public static TokenValidationParameters GetTokenValidationParameters(JwtSettings settings, bool validateLifetime = false) =>
    new TokenValidationParameters()
    {
        ValidateIssuer = settings.ValidateIssuer,
        ValidIssuer = settings.Issuer,

        ValidateAudience = settings.ValidateAudience,
        ValidAudience = settings.Audiences.FirstOrDefault(),

        ClockSkew = TimeSpan.Zero,

        ValidateLifetime = validateLifetime,
        IssuerSigningKey = settings.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
    #endregion
    #region Private Methods
    private async Task<TokenDto> GenerateAccessToken(ApplicationUser user)
    {
        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpMinutes);
        var claims = await GetClaimsAsync(user);
        var jwt = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            _jwtSettings.Audiences[0],
            claims,
            expires: expires,
            signingCredentials: new SigningCredentials(_jwtSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new TokenDto { Token = encodedJwt, ExpiryDate = expires };
    }
    private RefreshTokenDto GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(randomNumber);
            var now = DateTimeOffset.UtcNow;
            return new()
            {
                Token = Convert.ToBase64String(randomNumber),
                CreationDate = now,
                ExpiryDate = now.AddMinutes(_jwtSettings.RefreshTokenExpMinutes),
            };
        }
    }
    private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
    {
        var claims = (await _userManager.GetClaimsAsync(user)).AsEnumerable();
        claims = claims
            .Union([
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(CustomeClaims.AccountType, user.AccountType.ToString()),
            ]);

        return claims;
    }
    private async Task<bool> ValidateAccessTokenAsync(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claimsPrincipal = tokenHandler.ValidateToken(accessToken, GetTokenValidationParameters(_jwtSettings), out var _);

        var claims = claimsPrincipal.Claims.ToList();

        var hasId = long.TryParse(claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value, out long userIdClaim);

        if (!hasId) return false;

        var userEmailClaim = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));
        if (userEmailClaim is null) return false;

        var userExist = await _userManager.Users.AnyAsync(u => u.Id == userIdClaim && u.Email.Equals(userEmailClaim.Value));

        return userExist;
    }
    private async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
    {
        var token = await _dbcontext.RefreshTokens.FirstOrDefaultAsync(t => t.Token.Equals(refreshToken));
        return token is not null && token.IsActive;
    }

    #endregion
}