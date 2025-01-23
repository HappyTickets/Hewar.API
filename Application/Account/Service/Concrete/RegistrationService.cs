using Application.Account.Service.Interfaces;
using Domain.Events.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Account.Service.Concrete
{
    public class RegistrationService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager, IMediator mediator) : IRegistrationService
    {
        public async Task<Result<Empty>> ValidateRegistrationAsync(string phone, string email, string role)
        {
            if (await IsPhoneNumberTaken(phone))
                return new ConflictError(ErrorCodes.PhoneExists);

            if (await userManager.Users.AnyAsync(u => u.Email == email))
                return new ConflictError(ErrorCodes.EmailExists);

            if (!await roleManager.RoleExistsAsync(role))
                return new NotFoundError(ErrorCodes.RoleNotExists);

            return null;
        }

        public ApplicationUser CreateUserBase(string email, string phone, AccountTypes accountType, string imageUrl, bool isConfirmed = false)
        {
            return new ApplicationUser
            {
                Email = email,
                EmailConfirmed = isConfirmed,
                PhoneNumber = phone,
                PhoneNumberConfirmed = isConfirmed,
                AccountType = accountType,
                ImageUrl = imageUrl
            };
        }

        public async Task<Result<Empty>> RegisterAccountAsync(ApplicationUser user, string password, string role)
        {
            var registrationResults = await userManager.CreateAsync(user, password);

            if (!registrationResults.Succeeded)
                return new ValidationError(registrationResults.Errors.Select(er => er.Description));

            await userManager.AddToRoleAsync(user, role);

            var accountId = GetAccountId(user);

            if (string.IsNullOrEmpty(accountId))
                return new ValidationError("User must be associated with either Guard, Company, or Facility.");


            await userManager.AddClaimsAsync(user, [
                        new Claim(CustomClaims.AccountId, accountId),
                        new Claim(CustomClaims.IdentityId, user.Id.ToString())
                    ]);


            await mediator.Publish(new AccountCreated(user));

            return new()
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

        private async Task<bool> IsPhoneNumberTaken(string phoneNumber)
            => await userManager.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
    }

}

