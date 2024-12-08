using Application;
using Application.Common.Exceptions;
using Application.Common.Utilities;
using Infrastructure;
using Localization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// default
builder.Services
    .AddControllers()
    .AddDataAnnotationsLocalization();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddLocalizationService(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

// configs
builder.Services
    .Configure<ApiBehaviorOptions>(opt =>
    {
        opt.InvalidModelStateResponseFactory = (context) =>
        {
            var errors = context.ModelState.Values
            .Select(v => v.Errors.Select(e => e.ErrorMessage))
            .SelectMany(e => e);

            return new BadRequestObjectResult((Result<Empty>)new ValidationException(errors));
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();