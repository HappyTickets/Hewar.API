using Microsoft.AspNetCore.Http;

namespace Application.Common.Utilities
{
    public class Result<TData>
    {
        public int Status { get; init; }
        public bool IsSuccess { get; init; }
        public ErrorCodes ErrorCode { get; init; }
        public SuccessCodes SuccessCode { get; init; }
        public TData? Data { get; init; }


        public static implicit operator Result<TData>(TData data)
            => new()
            {
                Status = StatusCodes.Status200OK,
                IsSuccess = true,
                ErrorCode = ErrorCodes.None,
                Data = data,

            };
        public static Result<TData> Success(TData data, SuccessCodes successCode)
          => new()
          {
              Status = StatusCodes.Status200OK,
              IsSuccess = true,
              ErrorCode = ErrorCodes.None,
              SuccessCode = successCode,
              Data = data
          };

        public static implicit operator Result<TData>(ErrorBase err)
            => new()
            {
                Status = err.Status,
                IsSuccess = false,
                ErrorCode = err.ErrorCode,
                SuccessCode = SuccessCodes.None
            };

        public static implicit operator Result<TData>(ValidationError err)
            => new()
            {
                Status = err.Status,
                IsSuccess = false,
                ErrorCode = err.ErrorCode,
                SuccessCode = SuccessCodes.None


            };
    }
}
