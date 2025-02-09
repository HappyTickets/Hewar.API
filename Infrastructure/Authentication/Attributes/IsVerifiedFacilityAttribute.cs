using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Authentication.Attributes
{


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IsVerifiedFacilityAttribute
        (IFacilityInspector facilityInspector, ICurrentUserService currentUser)
        : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (currentUser.EntityType is EntityTypes.Facility)
            {
                var facilityId = currentUser.EntityId;
                if (!facilityId.HasValue || !await facilityInspector.IsAuthorized(facilityId.Value))
                {
                    context.Result = new UnauthorizedObjectResult(new Result<Empty>
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        IsSuccess = false,
                        ErrorCode = ErrorCodes.NoActiveContracts,
                        Message = "Authorization failed. No active contracts."
                    });
                    return;
                }
            }

            await next();
        }
    }

}
