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

        public long? Id
        {
            get
            {
                var isParsed = long.TryParse(_httpcontext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var result);
                return isParsed ? result : null;
            }
        }

        public string? Email => _httpcontext?.User.FindFirstValue(ClaimTypes.Email);
        public string? Role => _httpcontext?.User.FindFirstValue(ClaimTypes.Role);
    }
}
