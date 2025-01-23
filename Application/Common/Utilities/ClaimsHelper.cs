using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Common.Utilities
{
    public class ClaimsHelper
    {

        public static T GetClaimValue<T>(IEnumerable<Claim> claims, string claimType, T defaultValue = default)
        {
            var value = claims.FirstOrDefault(c => c.Type == claimType)?.Value;
            return value != null && typeof(T) != typeof(string)
                ? (T)Convert.ChangeType(value, typeof(T))
                : (T)(object)value ?? defaultValue;
        }
        public static Task<IEnumerable<Claim>> ExtractClaimsFromToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return Task.FromResult(Enumerable.Empty<Claim>());

            try
            {
                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(token))
                    return Task.FromResult(Enumerable.Empty<Claim>());

                var jwtToken = handler.ReadJwtToken(token);
                return Task.FromResult(jwtToken.Claims.AsEnumerable());
            }
            catch
            {
                return Task.FromResult(Enumerable.Empty<Claim>());
            }
        }

        public static string? GetClaimValue(IEnumerable<Claim> claims, string claimType)
        {
            return claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? null;
        }

    }
}
