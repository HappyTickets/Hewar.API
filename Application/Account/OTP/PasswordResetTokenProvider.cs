using Application.AccountManagement.OTP;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using static System.Net.WebRequestMethods;

namespace Application.Account.OTP
{
    public class PasswordResetTokenProvider : IUserTwoFactorTokenProvider<ApplicationUser>
    {
        private readonly IMemoryCache _cache;

        public PasswordResetTokenProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(true);
        }

        public Task<string> GenerateAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            var otp = Enumerable.Range(1, 6)
                .Select(_ => Random.Shared.Next(0, 9).ToString())
                .Aggregate((acc, num) => acc + num);

            _cache.Set($"PasswordReset-{user.Id}", otp, TimeSpan.FromMinutes(15));

            return Task.FromResult(otp);
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            var otp = _cache.Get<string>($"PasswordReset-{user.Id}");

            if (otp == null)
                return Task.FromResult(false);

            return Task.FromResult(otp == token);
        }
    }
}
