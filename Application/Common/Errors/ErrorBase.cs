namespace Application.Common.Errors
{
    public abstract class ErrorBase
    {
        public int Status { get; }
        public ErrorCodes ErrorCode { get; }


        public ErrorBase(int status, ErrorCodes errorCode, string? message = null)
        {
            Status = status;
            ErrorCode = errorCode;

        }
    }
}
