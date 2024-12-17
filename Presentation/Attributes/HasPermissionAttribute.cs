using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes
{
    public class HasPermissionAttribute: AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permission)
        {
            Roles = permission.ToString();
        }
    }
}
