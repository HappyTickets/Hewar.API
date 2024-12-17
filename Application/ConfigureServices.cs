using Application.Account.Validators;
using Application.AccountManagement.Service.Concrete;
using Application.AccountManagement.Service.Interfaces;
using Application.Authorization.Service;
using Application.Files.Service;
using Application.Notifications.Service;
using Application.PriceRequests.Service;
using Application.Tickets.Service;
using Domain.Entities.UserEntities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
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
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
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
              .AddScoped<ITicketsService, TicketsService>()
              .AddScoped<IUserValidator<ApplicationUser>, UserValidator>();


            return services;
        }
    }
}
