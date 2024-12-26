using Microsoft.AspNetCore.Identity;

namespace Application.AccountManagement.OTP.Extensions
{
    public static class OtpExtensions
    {
        public static async Task<string> GenerateOtpTokenAsync(this UserManager<ApplicationUser> manager,
            string purpose, ApplicationUser user)
        {
            var tokenProvider = new EmailOtpTokenProvider();
            return await tokenProvider.GenerateOtpTokenAsync(purpose, manager, user);
        }
    }

}
