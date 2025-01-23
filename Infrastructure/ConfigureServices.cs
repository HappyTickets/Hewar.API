using Application.Account.OTP;
using Application.AccountManagement.OTP;
using Application.AccountManagement.Service.Interfaces;
using Application.Authorization.Service;
using Infrastructure.Authentication.Handlers;
using Infrastructure.Authentication.Requirements;
using Infrastructure.Background;
using Infrastructure.Mail;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
                .AddMemoryCache()
                .AddSignalR();

            // DI
            services
                .AddScoped<ITokensService, TokensService>()
                .AddScoped<IEmailSender, EmailSender>()
                .AddScoped<IAuthorizationRepository, AuthorizationRepository>()
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<IUnitOfWorkService, UnitOfWorkService>()
                .AddScoped<INotificationService, NotificationService>()
                .AddScoped<AppDbContextIntializer>()
                .AddHostedService<RefreshTokenCleaner>();

            // configs
            services
                .Configure<MailSettings>(config.GetSection(MailSettings.SectionName));

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = nameof(PasswordResetTokenProvider);
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
                .AddTokenProvider<EmailOtpTokenProvider>(nameof(EmailOtpTokenProvider))
                .AddTokenProvider<PasswordResetTokenProvider>(nameof(PasswordResetTokenProvider))
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

            services.AddAuthorization(opt =>
            {
                foreach (var permission in Enum.GetValues<Permissions>())
                {
                    opt.AddPolicy(permission.ToString(), builder =>
                    {
                        builder
                        .RequireAuthenticatedUser()
                        .AddRequirements(new PermissionRequirement(permission));
                    });
                }

                //foreach (var type in Enum.GetNames(typeof(AccountTypes)))
                //{
                //    opt.AddPolicy(type, builder =>
                //    {
                //        builder
                //        .RequireAuthenticatedUser()
                //        .RequireClaim(CustomeClaims.AccountType, type);
                //    });
                //}
            });

            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
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
