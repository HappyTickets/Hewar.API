using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authentication.Attributes
{
    public class HaveAccountTypes : AuthorizeAttribute
    {
        public HaveAccountTypes(params AccountTypes[] types)
        {
            Roles = string.Join(",", types.Select(t => t.ToString()));
        }
    }
}
