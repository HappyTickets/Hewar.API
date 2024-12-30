using Microsoft.AspNetCore.Http;

namespace Application.Common.Errors
{
    public class ForbiddenError : ErrorBase
    {
        public ForbiddenError(ErrorCodes errorCode, string? message = null) :base(StatusCodes.Status403Forbidden, errorCode, message ?? "You are not allowed to perform this operation.")
        {
        }
        
        public ForbiddenError(string? message = null) :base(StatusCodes.Status403Forbidden, ErrorCodes.Forbidden, message ?? "You are not allowed to perform this operation.")
        {
        }
    }
}
