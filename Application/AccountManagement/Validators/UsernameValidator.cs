using Domain.Entities.UserEntities;
using Localization.ResourceFiles;
using Microsoft.AspNetCore.Identity;

namespace Application.AccountManagement.Validators
{
    public class UsernameValidator<TUser> : IUserValidator<TUser> where TUser : ApplicationUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            if (user.UserName.Any(x => x == ' '))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidCharactersUsername",
                    Description = string.Format(Resource.UserName_NoSpaces)
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
