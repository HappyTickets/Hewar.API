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

    public async Task<Result<Empty>> RegisterGuardAsync(RegisterGuardRequest registerRequest, CancellationToken cancellationToken = default)
    {
        if (await IsPhoneNumberTaken(registerRequest.Phone))
            return new ValidationException(Resource.PhoneNumber_Unique_Validation);

        var user = new ApplicationUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.Phone,
            Guard = new()
            {
                Name = registerRequest.Name,
                DateOfBirth = registerRequest.DateOfBirth,
                Skills = registerRequest.Skills
            }
        };

        var registrationResults = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!registrationResults.Succeeded)
            return new ValidationException(registrationResults.Errors.Select(er => er.Description));
        
        try
        {
            await _userManager.AddToRoleAsync(user, Roles.Guard.ToString());
        }
        catch
        {
            await _userManager.DeleteAsync(user);
            throw;
        }

        return Empty.Default;
    }

    public async Task<Result<Empty>> RegisterFacilityAsync(RegisterFacilityRequest registerRequest, CancellationToken cancellationToken = default)
    {
        if (await IsPhoneNumberTaken(registerRequest.Phone))
            return new ValidationException(Resource.PhoneNumber_Unique_Validation);

        var user = new ApplicationUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.Phone,
            Facility = new()
            {
                Name = registerRequest.Name,
                Type = registerRequest.Type,
                CommercialRegistration = registerRequest.CommercialRegistration,
                ActivityType = registerRequest.ActivityType,
                Address = registerRequest.Address,
                City = registerRequest.City,
                ResponsibleName = registerRequest.ResponsibleName,
                ResponsiblePhone = registerRequest.ResponsiblePhone,
            }
        };
        var registrationResults = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!registrationResults.Succeeded)
              return new ValidationException(registrationResults.Errors.Select(er => er.Description));
        
        try
        {
            await _userManager.AddToRoleAsync(user, Roles.Facility.ToString());
        }
        catch
        {
            await _userManager.DeleteAsync(user);
            throw;
        }

        return Empty.Default;
    }

    public async Task<Result<Empty>> RegisterCompanyAsync(RegisterCompanyRequest registerRequest, CancellationToken cancellationToken = default)
    {
        if (await IsPhoneNumberTaken(registerRequest.Phone))
            return new ValidationException(Resource.PhoneNumber_Unique_Validation);

        var user = new ApplicationUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.Phone,
            Company = new()
            {
                Name = registerRequest.Name,
                Address = registerRequest.Address
            }
        };

        var registrationResults = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!registrationResults.Succeeded)
            return new ValidationException(registrationResults.Errors.Select(er => er.Description));

        try
        {
            await _userManager.AddToRoleAsync(user, Roles.Company.ToString());
        }
        catch
        {
            await _userManager.DeleteAsync(user);
            throw;
        }

        return Empty.Default;
    }


    public async Task<Result<UserSessionDto>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        var user = await FindUserWithRolesAsync(u => u.Email == loginRequest.Email);

        if (user == null)
            return new NotFoundException(Resource.Invalid_UserName_Password);

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return new UnauthorizedException(Resource.Email_NotConfirmed);

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

    private UserSessionDto BuildUserSessionDto(ApplicationUser user, TokensInfo tokens)
        => new UserSessionDto
        {
            Id = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            Role = user.ApplicationUserRoles?.Select(ur => ur.Role!.Name).Single()!,
            AccessToken = tokens.JWT.Token,
            RefreshToken = tokens.Refresh.Token,
            AccessTokenExpDate = tokens.JWT.ExpiryDate.UtcDateTime,
            RefreshTokenExpDate = tokens.Refresh.ExpiryDate.UtcDateTime
        };

    #endregion
}
