using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Dtos.User;
using Application.AccountManagement.Service.Interfaces;
using AutoMapper;
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
    ICurrentUserService currentUser,
    IEmailConfirmationService emailConfirmationService,
    IMapper mapper,
    RoleManager<ApplicationRole> roleManager) : IAuthenticationService
{

    public async Task<Result<Empty>> RegisterGuardAsync(RegisterGuardRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateRegistrationAsync(registerRequest.Phone, registerRequest.Email, Roles.Guard);
        if (validationResult != null) return validationResult;

        var user = CreateUser(registerRequest.Email, registerRequest.Phone, AccountTypes.Guard, registerRequest.ImageUrl);
        user.UserName = registerRequest.UserName;
        user.Guard = new Guard
        {
            FirstName = registerRequest.FirstName,
            LastName = registerRequest.LastName,
            DateOfBirth = registerRequest.DateOfBirth,
            NationalId = registerRequest.NationalId,
            Qualification = registerRequest.Qualification,
            City = registerRequest.City,
            BloodType = registerRequest.BloodType,
            Height = registerRequest.Height,
            Weight = registerRequest.Weight,
            Skills = mapper.Map<ICollection<Skill>>(registerRequest.Skills),
            PrevCompanies = mapper.Map<ICollection<PrevCompany>>(registerRequest.PrevCompanies),
        };

        return await RegisterUserAsync(user, registerRequest.Password, Roles.Guard);
    }

    public async Task<Result<Empty>> RegisterFacilityAsync(RegisterFacilityRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateRegistrationAsync(registerRequest.Phone, registerRequest.Email, Roles.Facility);
        if (validationResult != null) return validationResult;

        var user = CreateUser(registerRequest.Email, registerRequest.Phone, AccountTypes.Facility, registerRequest.ImageUrl);

        user.Facility = new Facility
        {
            Name = registerRequest.Name,
            Type = registerRequest.Type,
            CommercialRegistration = registerRequest.CommercialRegistration,
            ActivityType = registerRequest.ActivityType,
            Address = registerRequest.Address,
            City = registerRequest.City,
            ResponsibleName = registerRequest.ResponsibleName,
            ResponsiblePhone = registerRequest.ResponsiblePhone,
        };

        return await RegisterUserAsync(user, registerRequest.Password, Roles.Facility);
    }

    public async Task<Result<Empty>> RegisterCompanyAsync(RegisterCompanyRequest registerRequest, CancellationToken cancellationToken = default)
    {
        var validationResult = await ValidateRegistrationAsync(registerRequest.Phone, registerRequest.Email, Roles.Company);
        if (validationResult != null) return validationResult;

        var user = CreateUser(registerRequest.Email, registerRequest.Phone, AccountTypes.Company, registerRequest.ImageUrl);

        user.Company = new Company
        {
            Name = registerRequest.Name,
            Address = registerRequest.Address,
        };

        return await RegisterUserAsync(user, registerRequest.Password, Roles.Company);
    }

    // Shared Validation Logic
    private async Task<Result<Empty>?> ValidateRegistrationAsync(string phone, string email, string role)
    {
        if (await IsPhoneNumberTaken(phone))
            return new ConflictError(ErrorCodes.PhoneExists);

        if (await userManager.Users.AnyAsync(u => u.Email == email))
            return new ConflictError(ErrorCodes.EmailExists);

        if (!await roleManager.RoleExistsAsync(role))
            return new NotFoundError(ErrorCodes.RoleNotExists);

        return null;
    }

    // User Creation Helper
    private ApplicationUser CreateUser(string email, string phone, AccountTypes accountType, string imageUrl)
    {
        return new ApplicationUser
        {
            Email = email,
            PhoneNumber = phone,
            AccountType = accountType,
            ImageUrl = imageUrl
        };
    }

    // Registration Helper
    private async Task<Result<Empty>> RegisterUserAsync(ApplicationUser user, string password, string role)
    {
        var registrationResults = await userManager.CreateAsync(user, password);

        if (!registrationResults.Succeeded)
            return new ValidationError(registrationResults.Errors.Select(er => er.Description));

        await userManager.AddToRoleAsync(user, role);

        var accountId = GetAccountId(user);

        if (string.IsNullOrEmpty(accountId))
            return new ValidationError("User must be associated with either Guard, Company, or Facility.");


        await userManager.AddClaimAsync(user, new Claim(CustomClaims.AccountId, accountId));
        await userManager.AddClaimAsync(user, new Claim(CustomClaims.IdentityId, user.Id.ToString()));

        // TO DO : Email confirmation 
        //emailConfirmationService.SendEmailConfirmationAsync(new SendEmailConfirmationRequest { Email = user.Email });

        return new Result<Empty>
        {
            Status = StatusCodes.Status200OK,
            IsSuccess = true,
            SuccessCode = SuccessCodes.UserRegistered
        };
    }
    private string? GetAccountId(ApplicationUser user)
    {
        return user.Guard?.Id.ToString()
               ?? user.Company?.Id.ToString()
               ?? user.Facility?.Id.ToString();
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
            //emailConfirmationService.SendEmailConfirmationAsync(new SendEmailConfirmationRequest { Email = loginRequest.Email });
            return new UnauthorizedError(ErrorCodes.UnconfirmedEmail);
        }

        var tokens = await tokensService.GenerateTokensAsync(user);

        var userSessionDto = await BuildAccountSessionDto(user, tokens);
        return new Result<AccountSessionDto>
        {
            Status = StatusCodes.Status200OK,
            IsSuccess = true,
            SuccessCode = SuccessCodes.LoggedInSuccessfully,
            Data = userSessionDto
        };
    }

    public async Task<Result<Empty>> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        try
        {
            await tokensService.RemoveRefreshTokenAsync(refreshToken);
        }
        catch (Exception ex)
        {
            return new UnauthorizedError(ErrorCodes.NotFound, ex.Message);
        }

        return new Result<Empty>
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
            tokensInfo = await tokensService.RefreshAsync(tokens.AccessToken, tokens.RefreshToken);
        }
        catch (Exception ex)
        {
            return new UnauthorizedError(ErrorCodes.InvalidToken, ex.Message);
        }

        if (tokensInfo is null) return new UnauthorizedError();

        var user = await FindUserWithRolesAsync(u => u.Id == tokensInfo.UserId);
        if (user == null) return new NotFoundError(ErrorCodes.Unauthorized);

        var userSessionDto = await BuildAccountSessionDto(user, tokensInfo);
        return new Result<AccountSessionDto>
        {
            Status = StatusCodes.Status200OK,
            IsSuccess = true,
            SuccessCode = SuccessCodes.RefreshToken,
            Data = userSessionDto
        };
    }

    #region Private Helper Methods

    private async Task<bool> IsPhoneNumberTaken(string phoneNumber)
        => await userManager.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);

    private async Task<ApplicationUser?> FindUserWithRolesAsync(Expression<Func<ApplicationUser, bool>> predicate)
      => await userManager.Users
          .Include(u => u.ApplicationUserRoles)
          .ThenInclude(ur => ur.Role)
          .ThenInclude(r => r.Permissions)
          .FirstOrDefaultAsync(predicate);

    private async Task<AccountSessionDto> BuildAccountSessionDto(ApplicationUser user, TokensInfo tokens)
    {
        var accountClaims = await ClaimsHelper.ExtractClaimsFromToken(tokens.JWT.Token);

        var account = new AccountSessionDto
        {
            IdentityId = user.Id,
            AccountId = ClaimsHelper.GetClaimValue<long>(accountClaims, CustomClaims.AccountId),
            UserName = user.UserName,
            FirstName = ClaimsHelper.GetClaimValue(accountClaims, CustomClaims.FirstName),
            Name = ClaimsHelper.GetClaimValue(accountClaims, CustomClaims.Name),
            Email = user.Email!,
            AccountType = user.AccountType,
            ImageUrl = user.ImageUrl,
            Permissions = ExtractPermissions(user),
            AccessToken = tokens.JWT.Token,
            RefreshToken = tokens.Refresh.Token,
            AccessTokenExpDate = tokens.JWT.ExpiryDate.UtcDateTime,
            RefreshTokenExpDate = tokens.Refresh.ExpiryDate.UtcDateTime
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
