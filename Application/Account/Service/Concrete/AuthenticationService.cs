using Application.Account.Service.Interfaces;
using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Dtos.User;
using Application.AccountManagement.Service.Interfaces;
using AutoMapper;
using Domain.Entities.CompanyAggregate;
using Domain.Entities.FacilityAggregate;
using Domain.Events.Accounts;
using LanguageExt.Pipes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using LoginRequest = Application.AccountManagement.Dtos.Authentication.LoginRequest;

namespace Application.AccountManagement.Service.Concrete;
public class AuthenticationService(
    UserManager<ApplicationUser> userManager,
    ITokensService tokensService,
    IPublisher publisher,
    IRegistrationService registrationService,
    IMapper mapper) : IAuthenticationService
{

    public async Task<Result<Empty>> RegisterGuardAsync(RegisterGuardRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var validationResult = await registrationService.ValidateRegistrationAsync(registerRequest.Phone, registerRequest.Email);

        if (validationResult != null) return validationResult;

        var guard = mapper.Map<Guard>(registerRequest);

        var registrationResults = await userManager.CreateAsync(guard, registerRequest.Password);

        if (!registrationResults.Succeeded)
            return new ValidationError(registrationResults.Errors.Select(er => er.Description));

        await publisher.Publish(new AccountCreated(guard));

        //await userManager.AddToRoleAsync(guard, Roles.Guard);

        await userManager.AddClaimsAsync(guard, [new Claim(CustomClaims.UserId, guard.Id.ToString()), new Claim(CustomClaims.FirstName, guard.FirstName)]);

        return new()
        {
            Status = StatusCodes.Status200OK,
            IsSuccess = true,
            SuccessCode = SuccessCodes.UserRegistered
        };

    }

    public async Task<Result<Empty>> RegisterFacilityAsync(RegisterFacilityRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var adminUser = mapper.Map<ApplicationUser>(registerRequest.AdminInfo);
        var facility = mapper.Map<Facility>(registerRequest);
        var roleName = $"{facility.Name} Admin";

        return await registrationService.RegisterEntityWithAdminAsync(adminUser, registerRequest.AdminInfo.Password, roleName, () => registrationService.CreateFacilityAsync(facility), cancellationToken);
    }

    public async Task<Result<Empty>> RegisterCompanyAsync(RegisterCompanyRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var adminUser = mapper.Map<ApplicationUser>(registerRequest.AdminInfo);
        var company = mapper.Map<Company>(registerRequest);
        var roleName = $"{company.Name} Admin";

        return await registrationService.RegisterEntityWithAdminAsync(adminUser, registerRequest.AdminInfo.Password, roleName, () => registrationService.CreateCompanyAsync(company), cancellationToken);
    }



    public async Task<Result<AccountSessionDto>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        var user = await FindUserWithRolesAsync(u => u.Email == loginRequest.Email);

        if (user is null)
            return new NotFoundError(ErrorCodes.InvalidEmailOrPassword);

        if (!await userManager.CheckPasswordAsync(user, loginRequest.Password))
            return new ValidationError(ErrorCodes.InvalidEmailOrPassword);

        if (!await userManager.IsEmailConfirmedAsync(user))
        {
            return new UnauthorizedError(ErrorCodes.UnconfirmedEmail);
        }

        var tokens = await tokensService.GenerateTokensAsync(user);

        var userSessionDto = await BuildAccountSessionDto(user, tokens);
        return new()
        {
            Status = StatusCodes.Status200OK,
            IsSuccess = true,
            SuccessCode = SuccessCodes.LoggedInSuccessfully,
            Data = userSessionDto
        };
        throw new NotImplementedException();

    }

    public async Task<Result<Empty>> LogoutAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await tokensService.RemoveRefreshTokenAsync();
        }
        catch (Exception ex)
        {
            return new UnauthorizedError(ErrorCodes.NotFound, ex.Message);
        }

        return new()
        {
            Status = StatusCodes.Status200OK,
            IsSuccess = true,
            SuccessCode = SuccessCodes.LoggedoutSuccessfully
        };
    }

    public async Task<Result<AccountSessionDto>> RefreshTokenAsync(RefreshAuthTokenRequest tokens, CancellationToken cancellationToken = default)
    {
        TokensInfo tokensInfo;
        try
        {
            tokensInfo = await tokensService.RefreshAsync(tokens.AccessToken);
        }
        catch (Exception ex)
        {
            return new UnauthorizedError(ErrorCodes.InvalidToken, ex.Message);
        }

        if (tokensInfo is null) return new UnauthorizedError();

        var user = await FindUserWithRolesAsync(u => u.Id == tokensInfo.UserId);
        if (user == null) return new NotFoundError(ErrorCodes.Unauthorized);

        var userSessionDto = await BuildAccountSessionDto(user, tokensInfo);
        return new()
        {
            Status = StatusCodes.Status200OK,
            IsSuccess = true,
            SuccessCode = SuccessCodes.RefreshedToken,
            Data = userSessionDto
        };
    }

    #region Private Helper Methods

    private async Task<ApplicationUser?> FindUserWithRolesAsync(Expression<Func<ApplicationUser, bool>> predicate)
      => await userManager.Users
          .Include(u => u.ApplicationUserRoles)
          .ThenInclude(ur => ur.Role)
          .ThenInclude(r => r.Permissions)
          .FirstOrDefaultAsync(predicate);

    private async Task<AccountSessionDto> BuildAccountSessionDto(ApplicationUser user, TokensInfo tokens)
    {
        var account = new AccountSessionDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            FirstName = user.FirstName,
            Email = user.Email!,
            ImageUrl = user.ImageUrl,
            Permissions = ExtractPermissions(user),
            AccessToken = tokens.JWT.Token,
            AccessTokenExpDate = tokens.JWT.ExpiryDate.UtcDateTime,
        };

        return account;
    }

    private Permissions[] ExtractPermissions(ApplicationUser user)
    {
        return user.ApplicationUserRoles!
            .SelectMany(ur => ur.Role!.Permissions!)
            .Select(rp => rp.Permission)
            .Distinct()
            .ToArray();
    }

    #endregion
}
