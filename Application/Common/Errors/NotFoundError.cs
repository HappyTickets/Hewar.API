using Microsoft.AspNetCore.Http;

namespace Application.Common.Errors
{
    public class NotFoundError : ErrorBase
    {
        public NotFoundError(ErrorCodes errorCode, string? message = null) : base(StatusCodes.Status404NotFound, errorCode, message ?? "Resource wasn't found")
        {
        }
        
        public NotFoundError(string? message = null) : base(StatusCodes.Status404NotFound, ErrorCodes.NotFound, message ?? "Resource wasn't found")
        {
        }
    }
}
