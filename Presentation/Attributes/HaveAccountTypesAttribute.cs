using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes
{
    public class HaveAccountTypesAttribute: AuthorizeAttribute
    {
        public HaveAccountTypesAttribute(params AccountTypes[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
