using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Requirements
{
    internal class PermissionRequirement(Permissions permission) : IAuthorizationRequirement
    {
        public Permissions Permission { get; } = permission;
    }
}
