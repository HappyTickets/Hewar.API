using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes
{
    public class HasAccountTypeAttribute: AuthorizeAttribute
    {
        public HasAccountTypeAttribute(AccountTypes role)
        {
            Roles = role.ToString();
        }
    }
}
