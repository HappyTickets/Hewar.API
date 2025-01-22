using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext? _httpcontext;

        public CurrentUserService(IHttpContextAccessor httpcontextAccessor)
        {
            _httpcontext = httpcontextAccessor.HttpContext;
        }

        public long? AccountId
        {
            get
            {
                var isParsed = long.TryParse(_httpcontext?.User.FindFirstValue(CustomClaims.AccountId), out var result);
                return isParsed ? result : null;
            }
        }
        public long? IdentityId
        {
            get
            {
                var isParsed = long.TryParse(_httpcontext?.User.FindFirstValue(CustomClaims.IdentityId), out var result);
                return isParsed ? result : null;
            }
        }

        public AccountTypes? Type
        {
            get
            {
                var type = _httpcontext?.User.FindFirstValue(CustomClaims.AccountType);

                if (string.IsNullOrEmpty(type))
                    return null;

                return Enum.Parse<AccountTypes>(type);
            }
        }

        public string? Email => _httpcontext?.User.FindFirstValue(ClaimTypes.Email);

    }
}
