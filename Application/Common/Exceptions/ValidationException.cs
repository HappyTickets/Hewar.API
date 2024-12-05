using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public override int Status => StatusCodes.Status400BadRequest;
        public IEnumerable<string> Errors { get; }

        public ValidationException(string? message = null): base(message ?? "One or more validations have occurred")
        {
            
        }

        public ValidationException(IEnumerable<string> errors) : this("One or more validations have occurred")
        {
            Errors = errors;
        }
    }
}
