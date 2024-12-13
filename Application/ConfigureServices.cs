using Application.AccountManagement.Service.Concrete;
using Application.AccountManagement.Service.Interfaces;
using Application.Authorization.Service;
using Application.Files.Service;
using Application.Notifications.Service;
using Application.PriceRequests.Service;
using Application.Tickets.Service;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // services
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();

            // DI
            services
              .AddScoped<IPasswordResetService, PasswordResetService>()
              .AddScoped<IEmailConfirmationService, EmailConfirmationService>()
              .AddScoped<IAuthenticationService, AuthenticationService>()
              .AddScoped<IAuthorizationService, AuthorizationService>()
              .AddScoped<INotificationsService, NotificationsService>()
              .AddScoped<IFileService, FileService>()
              .AddScoped<IPriceRequestsService, PriceRequestsService>()
              .AddScoped<ITicketsService, TicketsService>();


            return services;
        }
    }
}
