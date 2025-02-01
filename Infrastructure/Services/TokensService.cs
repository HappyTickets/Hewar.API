using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Service.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Infrastructure.Services;
internal class TokensService
        (JwtSettings jwtSettings, UserManager<ApplicationUser> userManager, AppDbContext dbcontext, IHttpContextAccessor httpContextAccessor) :
        ITokensService
{
    #region Public Methods

    public async Task<TokensInfo> GenerateTokensAsync(ApplicationUser user)
    {
        var accessJwt = await GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();
        await SaveRefreshTokenAsync(user.Id, refreshToken);
        SetHttpOnlyCookie(nameof(RefreshToken), refreshToken.Token, refreshToken.ExpiryDate);

        return new TokensInfo { UserId = user.Id, JWT = accessJwt };
    }

    public async Task RemoveRefreshTokenAsync()
    {
        var refreshToken = GetRefreshTokenCookie();
        var token = await dbcontext.RefreshTokens.FirstOrDefaultAsync(t => t.Token.Equals(refreshToken));
        if (token is null) throw new Exception("refresh token is not found!!");

        dbcontext.RefreshTokens.Remove(token);
        await dbcontext.SaveChangesAsync();
        ClearRefreshTokenCookie();
    }
    public async Task RemoveExpiredTokensAsync()
    {
        var rowsAffected = await dbcontext.RefreshTokens
       .Where(t => DateTime.UtcNow >= t.ExpiryDate || t.RevokedOn.HasValue)
       .ExecuteDeleteAsync();

        if (rowsAffected > 0)
        {
            Console.WriteLine($"{rowsAffected} expired or revoked tokens removed.");
        }
    }
    public async Task<TokensInfo?> RefreshAsync(string accessToken)
    {
        var refreshToken = GetRefreshTokenCookie();

        if (!await ValidateAccessTokenAsync(accessToken))
            throw new Exception("Invalid access token!");

        if (!await ValidateRefreshTokenAsync(refreshToken))
            throw new Exception("Invalid refresh token!");

        var rt = await dbcontext.RefreshTokens
            .Include(rt => rt.User)
                .ThenInclude(u => u.ApplicationUserRoles)
                  .ThenInclude(ur => ur.Role)
                            .FirstOrDefaultAsync(t => t.Token.Equals(refreshToken));
        var user = rt?.User;
        if (user is null)
        {
            throw new Exception("refresh token is not found!");
        }
        // Revoke the token
        rt.RevokedOn = DateTime.UtcNow;
        dbcontext.RefreshTokens.Update(rt);
        await dbcontext.SaveChangesAsync();

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
    private void SetHttpOnlyCookie(string key, string value, DateTimeOffset expiry)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = expiry
        };
        httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, cookieOptions);
    }
    private async Task SaveRefreshTokenAsync(
       long userId,
       RefreshTokenDto refreshToken)
    {
        await dbcontext.RefreshTokens
         .AddAsync(new RefreshToken
         {
             UserId = userId,
             Token = refreshToken.Token,
             CreationDate = refreshToken.CreationDate,
             ExpiryDate = refreshToken.ExpiryDate,
         });
        await dbcontext.SaveChangesAsync();
    }


    private async Task<TokenDto> GenerateAccessToken(ApplicationUser user)
    {
        var expires = DateTime.UtcNow.AddMinutes(jwtSettings.TokenExpMinutes);
        var claims = await GetClaimsAsync(user);
        var jwt = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            jwtSettings.Audiences[0],
            claims,
            expires: expires,
            signingCredentials: new SigningCredentials(jwtSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

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
                ExpiryDate = now.AddMinutes(jwtSettings.RefreshTokenExpMinutes),
            };
        }
    }
    private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
    {
        // Retrieve existing claims
        var claims = (await userManager.GetClaimsAsync(user)).ToList();
        claims.Add(new Claim(CustomClaims.UserId, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Email, user.Email!));

        return claims;
    }
    //private async Task<IEnumerable<Claim>> GetAccountSpecificClaimsAsync(AccountTypes accountType, long accountId)
    //{
    //    var additionalClaims = new List<Claim>();

    //    switch (accountType)
    //    {
    //        case AccountTypes.Guard:
    //            //var guardFirstName = await dbcontext.Guards.FindAsync(accountId).Select(g => g.FirstName);
    //            //additionalClaims.Add(new Claim(CustomClaims.FirstName, guardFirstName));
    //            break;

    //        case AccountTypes.Company:
    //            var companyName = await dbcontext.Companies.FindAsync(accountId).Select(g => g.Name);
    //            additionalClaims.Add(new Claim(CustomClaims.Name, companyName));
    //            break;

    //        case AccountTypes.Facility:
    //            var facilityName = await dbcontext.Facilities.FindAsync(accountId).Select(g => g.Name);
    //            additionalClaims.Add(new Claim(CustomClaims.Name, facilityName));
    //            break;
    //    }

    //    return additionalClaims;
    //}

    private async Task<bool> ValidateAccessTokenAsync(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claimsPrincipal = tokenHandler.ValidateToken(accessToken, GetTokenValidationParameters(jwtSettings), out var _);

        var claims = claimsPrincipal.Claims.ToList();

        var hasId = long.TryParse(claims.FirstOrDefault(c => c.Type.Equals(CustomClaims.UserId))?.Value, out long userIdClaim);

        if (!hasId) return false;

        var userEmailClaim = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));
        if (userEmailClaim is null) return false;

        var userExist = await userManager.Users.AnyAsync(u => u.Id == userIdClaim && u.Email.Equals(userEmailClaim.Value));

        return userExist;
    }
    private async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
    {
        var token = await dbcontext.RefreshTokens.FirstOrDefaultAsync(t => t.Token.Equals(refreshToken));
        return token is not null && token.IsActive;
    }
    private string GetRefreshTokenCookie()
    {
        return httpContextAccessor.HttpContext.Request.Cookies[nameof(RefreshToken)];
    }
    private void ClearRefreshTokenCookie()
    {
        httpContextAccessor.HttpContext.Response.Cookies.Delete(nameof(RefreshToken));
    }
    #endregion
}