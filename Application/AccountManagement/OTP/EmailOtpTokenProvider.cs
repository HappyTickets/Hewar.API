using Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Application.AccountManagement.OTP
{
    public class EmailOtpTokenProvider : EmailTokenProvider<ApplicationUser>
    {

        public static string EmailOtpPurpose = "EmailActivation";

        public override async Task<string> GenerateAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            var securityToken = new SecurityToken(await manager.CreateSecurityTokenAsync(user));
            var modifier = await GetUserModifierAsync(purpose, manager, user);
            var code = OtpService.GenerateCode(securityToken, modifier, 6).ToString("D6", CultureInfo.InvariantCulture);
            return code;
        }


        public async Task<string> GenerateOtpTokenAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            var securityToken = new SecurityToken(await manager.CreateSecurityTokenAsync(user));
            var modifier = await GetUserModifierAsync(purpose, manager, user);

            var code = OtpService.GenerateCode(securityToken, modifier, 6).ToString("D6", CultureInfo.InvariantCulture);
            return code;
        }

        public override async Task<bool> ValidateAsync(string purpose, string token, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            if (!int.TryParse(token, out int code))
            {
                return false;
            }

            var securityToken = new SecurityToken(await manager.CreateSecurityTokenAsync(user));
            var modifier = await GetUserModifierAsync(purpose, manager, user);
            return OtpService.ValidateCode(securityToken, code, modifier, token.Length);
        }
    }

}
