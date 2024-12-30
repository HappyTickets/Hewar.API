using Microsoft.AspNetCore.Http;

namespace Application.Common.Errors
{
    public class UnauthorizedError : ErrorBase
    {
        public UnauthorizedError(ErrorCodes errorCode, string? message = null) : base(StatusCodes.Status401Unauthorized, errorCode, message ?? "You are not authorized")
        {
        }
        
        public UnauthorizedError(string? message = null) : base(StatusCodes.Status401Unauthorized, ErrorCodes.Unauthorized, message ?? "You are not authorized")
        {
        }
    }
}
