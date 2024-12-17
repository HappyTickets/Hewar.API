namespace Presentation.Extensions
{
    public static class CorsConfigsExtension
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("http://127.0.0.1:4200", "https://127.0.0.1:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();         
                });
            });

            return services;
        }
    }
}
