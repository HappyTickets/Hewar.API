using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication.Attributes
{
    public class AnyEntityTypeAttribute(params EntityTypes[] allowedTypes) : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUser = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();

            if (context.HttpContext.User.IsInRole(Roles.Admin))
            {
                return;
            }

            if (currentUser.EntityType == null || !allowedTypes.Contains(currentUser.EntityType.Value))
            {
                context.Result = new ForbidResult();
            }


        }
    }

}
