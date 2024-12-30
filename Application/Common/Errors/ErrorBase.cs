namespace Application.Common.Errors
{
    public abstract class ErrorBase
    {
        public int Status { get; }
        public ErrorCodes ErrorCode { get; }
        public string? Message { get; init; }

        public ErrorBase(int status, ErrorCodes errorCode, string? message = null)
        {
            Status = status;
            ErrorCode = errorCode;
            Message = message;
        }
    }
}
