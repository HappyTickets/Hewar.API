using Infrastructure.Authentication.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Authentication.Handlers
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUnitOfWorkService _ufw;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ICurrentUserService _currentUser;
        private readonly IMemoryCache _cache;

        public PermissionAuthorizationHandler(IUnitOfWorkService ufw, ICurrentUserService currentUser, IMemoryCache cache, RoleManager<ApplicationRole> roleManager)
        {
            _ufw = ufw;
            _currentUser = currentUser;
            _cache = cache;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var cacheKey = $"{_currentUser.Type}-{_currentUser.Id}";
            var permissions = _cache.Get<List<Permissions>>(cacheKey);

            if(permissions == null)
            {
                 permissions = await _roleManager.Roles
                    .Where(r=>r.ApplicationUserRoles!.Any(ur => ur.UserId == _currentUser.Id))
                    .SelectMany(r => r.Permissions)
                    .Select(r=>r.Permission)
                    .Distinct()
                    .ToListAsync();

                _cache.Set(cacheKey, permissions, TimeSpan.FromMinutes(15));
            }

            if (permissions.Any(p => p == requirement.Permission))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
