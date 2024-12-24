using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Requirements
{
    internal class PermissionRequirement: IAuthorizationRequirement
    {
        public Permissions Permission { get; }

        public PermissionRequirement(Permissions permission)
        {
            Permission = permission;
        }
    }
}
