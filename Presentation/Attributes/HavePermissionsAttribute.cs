using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes
{
    public class HavePermissionsAttribute: AuthorizeAttribute
    {
        public HavePermissionsAttribute(params Permissions[] permissions)
        {
            Roles = string.Join(",", permissions);
        }
    }
}
