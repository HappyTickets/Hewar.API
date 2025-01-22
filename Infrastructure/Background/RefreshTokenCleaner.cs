using Application.AccountManagement.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Background
{
    internal class RefreshTokenCleaner : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RefreshTokenCleaner> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        // Constructor for dependency injection
        public RefreshTokenCleaner(IConfiguration configuration,
                                    ILogger<RefreshTokenCleaner> logger,
                                    IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Token cleanup service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var intervalInHours = _configuration.GetValue("CleanupOptions:TokenIntervalInHours", 24);

                    _logger.LogInformation("Running token cleanup...");

                    // Create a scope to resolve scoped services
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var tokensService = scope.ServiceProvider.GetRequiredService<ITokensService>();
                        await tokensService.RemoveExpiredTokensAsync();
                    }

                    await Task.Delay(TimeSpan.FromHours(intervalInHours), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred during token cleanup.");
                }
            }

            _logger.LogInformation("Token cleanup service is stopping.");
        }
    }

}
