using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;

namespace Application.Account.Validators
{
    internal class UserValidator : IUserValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
