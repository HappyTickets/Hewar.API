using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            // service
            services
                .AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("Sql")));

            // DI
            services
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<IUnitOfWorkService, UnitOfWorkService>();

            return services;
        }
    }
}
