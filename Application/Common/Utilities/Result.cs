using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Utilities
{
    public class Result<TData>
    {
        public int Status { get; init; }
        public bool IsSuccess { get; init; }
        public string? Message { get; init; }
        public IEnumerable<string>? Errors { get; init; }
        public TData? Data { get; init; }


        public static implicit operator Result<TData>(TData data)
            => new()
            {
                IsSuccess = true,
                Status = StatusCodes.Status200OK,
                Data = data
            };

        public static implicit operator Result<TData>(ExceptionBase ex)
            => new()
            {
                IsSuccess = false,
                Status = ex.Status,
                Message = ex.Message,
            };

        public static implicit operator Result<TData>(ValidationException ex)
            => new()
            {
                IsSuccess = false,
                Status = ex.Status,
                Message = ex.Message,
                Errors = ex.Errors
            };
    }
}
