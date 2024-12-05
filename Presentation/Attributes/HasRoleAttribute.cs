using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes
{
    public class HasRoleAttribute: AuthorizeAttribute
    {
        public HasRoleAttribute(Roles role)
        {
            Roles = role.ToString();
        }
    }
}
