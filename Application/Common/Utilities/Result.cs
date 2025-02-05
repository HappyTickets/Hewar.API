using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Application.Common.Utilities
{
    public class Result<TData>
    {
        public int Status { get; init; }
        public bool IsSuccess { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Message { get; init; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string>? Errors { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ErrorCodes? ErrorCode { get; init; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SuccessCodes? SuccessCode { get; init; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData? Data { get; init; }


        public static implicit operator Result<TData>(TData data)
            => new()
            {
                Status = StatusCodes.Status200OK,
                IsSuccess = true,
                Data = data,

            };
        public static Result<TData> Success(TData data, SuccessCodes successCode)
          => new()
          {
              Status = StatusCodes.Status200OK,
              IsSuccess = true,
              SuccessCode = successCode,
              Data = data
          };

        public static implicit operator Result<TData>(ErrorBase err)
            => new()
            {
                Status = err.Status,
                IsSuccess = false,
                ErrorCode = err.ErrorCode,
            };

        public static implicit operator Result<TData>(ValidationError err)
            => new()
            {
                Status = err.Status,
                IsSuccess = false,
                ErrorCode = err.ErrorCode,
                Errors = err.Errors
            };
    }
}
