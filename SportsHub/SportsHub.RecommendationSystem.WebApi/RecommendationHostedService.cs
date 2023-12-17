using SportsHub.RecommendationSystem.Services;

namespace SportsHub.RecommendationSystem.WebApi
{
    public class RecommendationHostedService : BackgroundService
    {
        private readonly ILogger<RecommendationHostedService> _logger;
        private readonly IRecommendationsService _recommendationsService;

        public RecommendationHostedService(IServiceProvider services,
            ILogger<RecommendationHostedService> logger)
        {
            _logger = logger;
            var scope = services.CreateScope();

            _recommendationsService =
                scope.ServiceProvider
                    .GetRequiredService<IRecommendationsService>();

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("RecommendationHostedService started");

            while (!stoppingToken.IsCancellationRequested)
            {
                int min = 1;
                int hrs = 0;

                try
                {
                    if (!_recommendationsService.HasNewRecords())
                    {
                        _logger.LogInformation("There are NO new records to train");
                        await Task.Delay(new TimeSpan(hrs, min, 0));
                        continue;
                    }
                    _logger.LogInformation("RecommendationHostedService started building new reccomendations");

                    _recommendationsService.PerformNewRecommendations();

                    _logger.LogInformation("RecommendationHostedService ended building new reccomendations");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"RecommendationHostedService failed: {ex.ToString()}");
                }
                finally
                {
                    await Task.Delay(new TimeSpan(hrs, min, 0));
                }
            }
            _logger.LogInformation("RecommendationHostedService ended");
        }
    }
}
