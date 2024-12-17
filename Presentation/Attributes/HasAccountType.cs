using Microsoft.AspNetCore.Authorization;

namespace Presentation.Attributes
{
    public class HasAccountType:AuthorizeAttribute
    {
        public HasAccountType(AccountTypes type)
        {
            Policy = type.ToString();
        }
    }
}
