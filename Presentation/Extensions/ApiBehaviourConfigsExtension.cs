using Application.Common.Exceptions;
using Application.Common.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Extensions
{
    public static class ApiBehaviourConfigsExtension
    {
        public static IServiceCollection ConfigureApiBehaviour(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
                {
                    opt.InvalidModelStateResponseFactory = (context) =>
                    {
                        var errors = context.ModelState.Values
                        .Select(v => v.Errors.Select(e => e.ErrorMessage))
                        .SelectMany(e => e);

                        return new BadRequestObjectResult((Result<Empty>)new ValidationException(errors));
                    };
                });

            return services;
        }
    }
}
