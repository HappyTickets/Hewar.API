using Application;
using Infrastructure;
using Infrastructure.Notifications;
using Localization;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// default
builder.Services
    .AddControllers()
    .AddDataAnnotationsLocalization();

// services
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerService()
    .AddHttpContextAccessor()
    .AddCorsService()
    .AddLocalizationService(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

// configs
builder.Services
    .ConfigureApiBehaviour();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationsHub>("/hubs/notifications");
app.MapControllers();

app.Run();