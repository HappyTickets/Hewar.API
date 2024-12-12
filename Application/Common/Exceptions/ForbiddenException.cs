using Localization.ResourceFiles;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class ForbiddenException : ExceptionBase
    {
        public override int Status => StatusCodes.Status403Forbidden;

        public ForbiddenException(string? message = null):base(message ?? Resource.Forbbiden)
        {
            
        }
    }
}
