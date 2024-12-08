using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Dtos.User;
using Application.AccountManagement.Service.Interfaces;
using Application.Authorization.DTOs.Response;
using AutoMapper;
using Domain.Entities.UserEntities;
using Localization.ResourceFiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.AccountManagement.Service.Concrete;
public class AuthenticationService(
    UserManager<ApplicationUser> userManager,
    ITokensService tokensService,
    ICurrentUserService currenUser,
    IMapper mapper) : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ITokensService _tokensService = tokensService;
    private readonly IMapper _mapper = mapper;
    private readonly ICurrentUserService _currenUser = currenUser;

    public async Task<Result<Empty>> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken = default)
    {
        if (await IsPhoneNumberTaken(registerRequest.PhoneNumber))
            return new ValidationException(Resource.PhoneNumber_Unique_Validation);

        var user = new ApplicationUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.PhoneNumber,
            CreatedDate = DateTime.UtcNow,
        };

        var registrationResults = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!registrationResults.Succeeded)
            return new ValidationException(registrationResults.Errors.Select(er => er.Description));

        return new();
    }

    public async Task<Result<UserSessionDto>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        var user = await FindUserWithRolesAsync(u => u.Email == loginRequest.Email);

        if (user == null)
            return new NotFoundException(Resource.Invalid_UserName_Password);

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return new() { Data = new UserSessionDto { IsEmailConfirmed = false }, IsSuccess = false };

        if (!await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            return new ValidationException(Resource.Credentials_Invalid);

        var tokens = await _tokensService.GenerateTokensAsync(user);

        return BuildUserSessionDto(user, tokens);
    }

    public async Task<Result<Empty>> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {

        await _tokensService.RemoveRefreshTokenAsync(refreshToken);
        return new();
    }

    public async Task<Result<UserSessionDto>> RefreshTokenAsync(RefreshAuthTokenRequest tokens, CancellationToken cancellationToken = default)
    {
        var tokensInfo = await _tokensService.RefreshAsync(tokens.AccessToken, tokens.RefreshToken);

        if (tokensInfo == null) return new UnauthorizedException();

        var user = await FindUserWithRolesAsync(u => u.Id == tokensInfo.UserId);
        if (user == null) return new NotFoundException(Resource.NotFoundInDB_Message);

        return BuildUserSessionDto(user, tokensInfo);
    }

    #region Private Helper Methods

    private async Task<bool> IsPhoneNumberTaken(string phoneNumber)
        => await _userManager.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);

    private async Task<ApplicationUser?> FindUserWithRolesAsync(Expression<Func<ApplicationUser, bool>> predicate)
      => await _userManager.Users
          .Include(u => u.ApplicationUserRoles)
          .ThenInclude(ur => ur.Role)
          .FirstOrDefaultAsync(predicate);
    private IEnumerable<RoleDto> MapRoles(ApplicationUser user)
        => _mapper.Map<IEnumerable<RoleDto>>(user.ApplicationUserRoles.Select(ur => ur.Role));
    private UserSessionDto BuildUserSessionDto(ApplicationUser user, TokensInfo tokens)
        => new UserSessionDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Roles = MapRoles(user),
            IsEmailConfirmed = user.EmailConfirmed,
            AccessToken = tokens.JWT.Token,
            RefreshToken = tokens.Refresh.Token,
            AccessTokenExpDate = tokens.JWT.ExpiryDate.UtcDateTime,
            RefreshTokenExpDate = tokens.Refresh.ExpiryDate.UtcDateTime
        };

    #endregion
}
