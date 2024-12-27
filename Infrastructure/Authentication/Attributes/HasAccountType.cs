using Microsoft.AspNetCore.Authorization;

namespace Presentation.Authentication.Attributes
{
    public class HasAccountType:AuthorizeAttribute
    {
        public HasAccountType(AccountTypes type)
        {
            Roles = type.ToString();
        }
    }
}
