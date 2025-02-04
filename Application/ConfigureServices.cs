using Application.Account.Service.Concrete;
using Application.Account.Service.Interfaces;
using Application.Account.Validators;
using Application.AccountManagement.Service.Concrete;
using Application.AccountManagement.Service.Interfaces;
using Application.Ads.Service;
using Application.Authorization.Service;
using Application.Chats.Service;
using Application.Companies.Service;
using Application.Companies.Service.ProvidedServices;
using Application.Companies.Service.ServicesProvided;
using Application.Facilities.Service;
using Application.Files.Service;
using Application.Guards.Service;
using Application.Hewar.Service;
using Application.Notifications.Service;
using Application.PriceOffers.Services;
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
              .AddScoped<IRegistrationService, RegistrationService>()
              .AddScoped<IAuthenticationService, AuthenticationService>()
              .AddScoped<IAuthorizationService, AuthorizationService>()
              .AddScoped<INotificationsService, NotificationsService>()
              .AddScoped<IFileService, FileService>()
              .AddScoped<ITicketsService, TicketsService>()
              .AddScoped<ICompaniesService, CompaniesService>()
              .AddScoped<ICompanyProvidedService, CompanyProvidedService>()
              .AddScoped<IFacilitiesService, FacilitiesService>()
              .AddScoped<IGuardsService, GuardsService>()
              .AddScoped<IHewarProvidedService, HewarProvidedService>()
              .AddScoped<IPriceRequestsService, PriceRequestsService>()
              .AddScoped<IPriceOfferService, PriceOfferService>()
              .AddScoped<IChatService, ChatService>()
              .AddScoped<IUserValidator<ApplicationUser>, UserValidator>()
            .AddScoped<IAdsService, AdsService>();


            return services;
        }
    }
}
