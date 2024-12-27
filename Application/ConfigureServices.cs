using Application.Account.Validators;
using Application.AccountManagement.Service.Concrete;
using Application.AccountManagement.Service.Interfaces;
using Application.Authorization.Service;
using Application.Companies.Service;
using Application.Facilities.Service;
using Application.Files.Service;
using Application.Guards.Service;
using Application.InsuranceAds.Service;
using Application.Notifications.Service;
using Application.PriceRequests.Service;
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
              .AddScoped<IAuthenticationService, AuthenticationService>()
              .AddScoped<IAuthorizationService, AuthorizationService>()
              .AddScoped<INotificationsService, NotificationsService>()
              .AddScoped<IFileService, FileService>()
              .AddScoped<IPriceRequestsService, PriceRequestsService>()
              .AddScoped<ITicketsService, TicketsService>()
              .AddScoped<IUserValidator<ApplicationUser>, UserValidator>()
              .AddScoped<IGuardsService, GuardsService>()
              .AddScoped<ICompaniesService, CompaniesService>()
              .AddScoped<IFacilitiesService, FacilitiesService>()
              .AddScoped<IInsuranceAdsService, InsuranceAdsService>();


            return services;
        }
    }
}
