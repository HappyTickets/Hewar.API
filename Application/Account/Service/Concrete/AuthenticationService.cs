﻿using Application.AccountManagement.Dtos.Authentication;
using Application.AccountManagement.Dtos.Token;
using Application.AccountManagement.Dtos.User;
using Application.AccountManagement.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Application.AccountManagement.Service.Concrete;
public class AuthenticationService(
    UserManager<ApplicationUser> userManager,
    ITokensService tokensService,
    ICurrentUserService currenUser,
    IMapper mapper,
    RoleManager<ApplicationRole> roleManager) : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ITokensService _tokensService = tokensService;
    private readonly IMapper _mapper = mapper;
    private readonly ICurrentUserService _currenUser = currenUser;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    public async Task<Result<Empty>> RegisterGuardAsync(RegisterGuardRequest registerRequest, CancellationToken cancellationToken = default)
    {
        if (await IsPhoneNumberTaken(registerRequest.Phone))
            return new ConflictError(ErrorCodes.PhoneExists, Resource.PhoneNumber_Unique_Validation);

        if (await _userManager.Users.AnyAsync(u => u.Email == registerRequest.Email))
            return new ConflictError(ErrorCodes.EmailExists, Resource.EmailExistsError);

        if (await _userManager.Users.AnyAsync(u => u.UserName == registerRequest.UserName))
            return new ConflictError(ErrorCodes.UserNameExists, Resource.UserNameExistsError);

        if (!await _roleManager.RoleExistsAsync(Roles.Guard))
            return new NotFoundError(ErrorCodes.RoleNotExists, Resource.RoleNotExistError);

        var user = new ApplicationUser
        {
            UserName = registerRequest.UserName,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.Phone,
            AccountType = AccountTypes.Guard,
            ImageUrl = registerRequest.ImageUrl,
            Guard = new()
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
                Skills = _mapper.Map<ICollection<Skill>>(registerRequest.Skills),
                PrevCompanies = _mapper.Map<ICollection<PrevCompany>>(registerRequest.PrevCompanies),
            }
        };

        var registrationResults = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!registrationResults.Succeeded)
            return new ValidationError(registrationResults.Errors.Select(er => er.Description));

        await _userManager.AddToRoleAsync(user, Roles.Guard);
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Guard.Id.ToString()));

        return Empty.Default;
    }

    public async Task<Result<Empty>> RegisterFacilityAsync(RegisterFacilityRequest registerRequest, CancellationToken cancellationToken = default)
    {
        if (await IsPhoneNumberTaken(registerRequest.Phone))
            return new ConflictError(ErrorCodes.PhoneExists, Resource.PhoneNumber_Unique_Validation);

        if (await _userManager.Users.AnyAsync(u => u.Email == registerRequest.Email))
            return new ConflictError(ErrorCodes.EmailExists, Resource.EmailExistsError);


        if (!await _roleManager.RoleExistsAsync(Roles.Facility))
            return new NotFoundError(ErrorCodes.RoleNotExists, Resource.RoleNotExistError);

        var user = new ApplicationUser
        {
            UserName = null,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.Phone,
            AccountType = AccountTypes.Facility,
            ImageUrl = registerRequest.ImageUrl,
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
              return new ValidationError(registrationResults.Errors.Select(er => er.Description));

        await _userManager.AddToRoleAsync(user, Roles.Facility);
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Facility.Id.ToString()));

        return Empty.Default;
    }

    public async Task<Result<Empty>> RegisterCompanyAsync(RegisterCompanyRequest registerRequest, CancellationToken cancellationToken = default)
    {
        if (await IsPhoneNumberTaken(registerRequest.Phone))
            return new ConflictError(ErrorCodes.PhoneExists, Resource.PhoneNumber_Unique_Validation);

        if (await _userManager.Users.AnyAsync(u => u.Email == registerRequest.Email))
            return new ConflictError(ErrorCodes.EmailExists, Resource.EmailExistsError);

        if (!await _roleManager.RoleExistsAsync(Roles.Company))
            return new NotFoundError(ErrorCodes.RoleNotExists, Resource.RoleNotExistError);

        var user = new ApplicationUser
        {
            UserName = null,
            Email = registerRequest.Email,
            PhoneNumber = registerRequest.Phone,
            AccountType = AccountTypes.Company,
            ImageUrl = registerRequest.ImageUrl,
            Company = new()
            {
                Name = registerRequest.Name,
                Address = registerRequest.Address
            }
        };

        var registrationResults = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!registrationResults.Succeeded)
            return new ValidationError(registrationResults.Errors.Select(er => er.Description));

        await _userManager.AddToRoleAsync(user, Roles.Company);
        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Company.Id.ToString()));

        return Empty.Default;
    }


    public async Task<Result<UserSessionDto>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
    {
        var user = await FindUserWithRolesAsync(u => u.Email == loginRequest.Email);

        if (user == null)
            return new NotFoundError(ErrorCodes.InvalidEmailOrPassword, Resource.Invalid_UserName_Password);

        if (!await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            return new ValidationError(ErrorCodes.InvalidEmailOrPassword, Resource.Credentials_Invalid);

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return new UnauthorizedError(ErrorCodes.UnconfirmedEmail, Resource.Email_NotConfirmed);

        var tokens = await _tokensService.GenerateTokensAsync(user);

        return await BuildUserSessionDto(user, tokens);
    }

    public async Task<Result<Empty>> LogoutAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        await _tokensService.RemoveRefreshTokenAsync(refreshToken);
        return new();
    }

    public async Task<Result<UserSessionDto>> RefreshTokenAsync(RefreshAuthTokenRequest tokens, CancellationToken cancellationToken = default)
    {
        var tokensInfo = await _tokensService.RefreshAsync(tokens.AccessToken, tokens.RefreshToken);

        if (tokensInfo == null) return new UnauthorizedError();

        var user = await FindUserWithRolesAsync(u => u.Id == tokensInfo.UserId);
        if (user == null) return new NotFoundError(ErrorCodes.Unauthorized, Resource.NotFoundInDB_Message);

        return await BuildUserSessionDto(user, tokensInfo);
    }

    #region Private Helper Methods

    private async Task<bool> IsPhoneNumberTaken(string phoneNumber)
        => await _userManager.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);

    private async Task<ApplicationUser?> FindUserWithRolesAsync(Expression<Func<ApplicationUser, bool>> predicate)
      => await _userManager.Users
          .Include(u => u.ApplicationUserRoles)
          .ThenInclude(ur => ur.Role)
          .ThenInclude(r=>r.Permissions)
          .FirstOrDefaultAsync(predicate);

    private async Task<UserSessionDto> BuildUserSessionDto(ApplicationUser user, TokensInfo tokens)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        return new UserSessionDto
        {
            Id = long.Parse(claims.First(c=>c.Type == ClaimTypes.NameIdentifier)!.Value),
            UserName = user.UserName!,
            Email = user.Email!,
            AccountType = user.AccountType,
            ImageUrl = user.ImageUrl,
            Permissions = ExtractPermissions(user),
            AccessToken = tokens.JWT.Token,
            RefreshToken = tokens.Refresh.Token,
            AccessTokenExpDate = tokens.JWT.ExpiryDate.UtcDateTime,
            RefreshTokenExpDate = tokens.Refresh.ExpiryDate.UtcDateTime
        };
    }

    private Permissions[] ExtractPermissions(ApplicationUser user)
    {
        return user.ApplicationUserRoles!
            .SelectMany(ur => ur.Role!.Permissions!)
            .Select(rp=>rp.Permission)
            .Distinct()
            .ToArray();
    }

    #endregion
}
