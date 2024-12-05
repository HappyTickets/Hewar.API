using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public override int Status => StatusCodes.Status404NotFound;

        public NotFoundException(string? message = null): base(message ?? "Resource wasn't found")
        {
            
        }
    }
}
