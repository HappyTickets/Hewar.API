using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permission)
        {
            Policy = permission.ToString();
        }
    }
}
