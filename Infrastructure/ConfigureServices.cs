using Application.AccountManagement.OTP;
using Application.AccountManagement.Service.Interfaces;
using Application.AccountManagement.Validators;
using Application.Authorization.Service;
using Domain.Entities.UserEntities;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            // service
            services
                .AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("Sql")));

            services
                .AddIdentityServices()
                .AddJWTConfiguration(config)
                .AddEmailConfiguration(config)
                .AddSignalR();

            // DI
            services
                .AddScoped<ITokensService, TokensService>()
                .AddScoped<IEmailSender, EmailSender>()
                .AddScoped<IAuthorizationRepository, AuthorizationRepository>()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<IUnitOfWorkService, UnitOfWorkService>()
                .AddScoped<INotificationService, NotificationService>();

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Tokens.EmailConfirmationTokenProvider = nameof(EmailOtpTokenProvider);
                options.User.AllowedUserNameCharacters = string.Empty;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
            })
                .AddUserValidator<UsernameValidator<ApplicationUser>>()
                .AddTokenProvider<EmailOtpTokenProvider>(nameof(EmailOtpTokenProvider))
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            return services;
        }
        private static void AddAuthenticationServices(this IServiceCollection services, TokenValidationParameters tokenValidationParameters)
        {
            if (tokenValidationParameters is null)
                throw new ArgumentNullException(nameof(tokenValidationParameters), "TokenValidationParameters cannot be null.");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/hubs")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        private static IServiceCollection AddJWTConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            JwtSettings jwtSettings = new();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            TokenValidationParameters tokenValidationParameters = TokensService.GetTokenValidationParameters(jwtSettings, true);

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthenticationServices(tokenValidationParameters);

            return services;
        }
        private static IServiceCollection AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            EmailConfig emailConfig = new();
            configuration.Bind(nameof(EmailConfig), emailConfig);
            services.AddSingleton(emailConfig);
            return services;
        }

    }
}
