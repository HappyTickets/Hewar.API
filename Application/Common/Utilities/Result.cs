using Microsoft.AspNetCore.Http;

namespace Application.Common.Utilities
{
    public class Result<TData>
    {
        public int Status { get; init; }
        public bool IsSuccess { get; init; }
        public ErrorCodes ErrorCode { get; init; }
        public string? Message { get; init; }
        public IEnumerable<string>? Errors { get; init; }
        public TData? Data { get; init; }


        public static implicit operator Result<TData>(TData data)
            => new()
            {
                Status = StatusCodes.Status200OK,
                IsSuccess = true,
                ErrorCode = ErrorCodes.None,
                Data = data
            };

        public static implicit operator Result<TData>(ErrorBase err)
            => new()
            {
                Status = err.Status,
                IsSuccess = false,
                ErrorCode = err.ErrorCode,
                Message = err.Message,
            };

        public static implicit operator Result<TData>(ValidationError err)
            => new()
            {
                Status = err.Status,
                IsSuccess = false,
                ErrorCode = err.ErrorCode,
                Message = err.Message,
                Errors = err.Errors
            };
    }
}
