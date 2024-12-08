using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Localization
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddLocalizationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCulture = new[] { new CultureInfo("ar-SA"), new CultureInfo("ar-EG"), new CultureInfo("en-GB"), new CultureInfo("en-US") };
                options.DefaultRequestCulture = new RequestCulture(supportedCulture[0]);
                options.SupportedCultures = supportedCulture;
                options.SupportedUICultures = supportedCulture;
            });
            return services;
        }
    }
}
