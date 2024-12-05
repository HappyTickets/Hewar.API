using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class ConflictException : ExceptionBase
    {
        public override int Status => StatusCodes.Status409Conflict;

        public ConflictException(string? message = null) : base(message ?? "There is a conflict happened while processing your request.")
        {
            
        }
    }
}
