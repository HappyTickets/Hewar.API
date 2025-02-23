using Application;
using Infrastructure;
using Infrastructure.Notifications;
using Infrastructure.Persistence;
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
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

// configs
builder.Services
    .ConfigureApiBehaviour();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseCors();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationsHub>("/hubs/notifications");
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider
        .GetRequiredService<AppDbContextIntializer>()
        .InitialiseAsync();
}

app.Run();

