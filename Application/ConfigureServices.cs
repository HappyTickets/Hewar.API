using Application.Account.Service.Concrete;
using Application.Account.Service.Interfaces;
using Application.Account.Validators;
using Application.AccountManagement.Service.Concrete;
using Application.AccountManagement.Service.Interfaces;
using Application.Authorization.Service;
using Application.Companies.Service;
using Application.Files.Service;
using Application.Notifications.Service;
using Application.Tickets.Service;
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
              .AddScoped<IRegistrationService, RegistrationService>()
              .AddScoped<IAuthenticationService, AuthenticationService>()
              .AddScoped<IAuthorizationService, AuthorizationService>()
              .AddScoped<INotificationsService, NotificationsService>()
              .AddScoped<IFileService, FileService>()
              .AddScoped<ITicketsService, TicketsService>()
              .AddScoped<ICompaniesService, CompaniesService>()
              .AddScoped<IUserValidator<ApplicationUser>, UserValidator>();
            //.AddScoped<IPriceRequestsService, PriceRequestsService>()
            //.AddScoped<IGuardsService, GuardsService>()
            //.AddScoped<IFacilitiesService, FacilitiesService>()
            //.AddScoped<IInsuranceAdsService, InsuranceAdsService>();


            return services;
        }
    }
}
