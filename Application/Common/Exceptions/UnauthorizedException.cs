using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class UnauthorizedException : ExceptionBase
    {
        public override int Status => StatusCodes.Status401Unauthorized;

        public UnauthorizedException(string? message = null): base(message ?? "You are not authorized")
        {
            
        }
    }
}
