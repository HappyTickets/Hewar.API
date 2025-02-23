using Infrastructure.Authentication.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Authentication.Handlers
{
    internal class PermissionAuthorizationHandler(IUnitOfWorkService ufw, ICurrentUserService currentUser, IMemoryCache cache, RoleManager<ApplicationRole> roleManager) : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var cacheKey = $"{currentUser.UserId}";
            var permissions = cache.Get<List<Permissions>>(cacheKey);

            if (permissions == null)
            {
                permissions = await roleManager.Roles
                   .Where(r => r.ApplicationUserRoles!.Any(ur => ur.UserId == currentUser.UserId))
                   .SelectMany(r => r.Permissions)
                   .Select(r => r.Permission)
                   .Distinct()
                   .ToListAsync();

                cache.Set(cacheKey, permissions, TimeSpan.FromMinutes(15));
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
