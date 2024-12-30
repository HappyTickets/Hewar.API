using Microsoft.AspNetCore.Http;

namespace Application.Common.Errors
{
    public class ConflictError : ErrorBase
    {
        public ConflictError(ErrorCodes errorCode, string? message = null) : base(StatusCodes.Status409Conflict, errorCode, message ?? "There is a conflict happened while processing your request.")
        {
        } 
        
        public ConflictError(string? message = null) : base(StatusCodes.Status409Conflict, ErrorCodes.Conflict, message ?? "There is a conflict happened while processing your request.")
        {
        }
    }
}
