using Microsoft.AspNetCore.Http;

namespace Application.Common.Errors
{
    public class ValidationError : ErrorBase
    {
        public IEnumerable<string> Errors { get; }

        public ValidationError(ErrorCodes errorCode, string? message = null) : base(StatusCodes.Status400BadRequest, errorCode, message ?? "One or more validations have occurred")
        {
        }
        
        public ValidationError(string? message = null) : base(StatusCodes.Status400BadRequest, ErrorCodes.Validation, message ?? "One or more validations have occurred")
        {
        }

        public ValidationError(IEnumerable<string> errors) : base(StatusCodes.Status400BadRequest, ErrorCodes.Validation, "One or more validations have occurred")
        {
            Errors = errors;
        }
    }
}
