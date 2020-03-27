namespace PhungDKH.Microservice.Domain.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using PhungDKH.Microservice.Domain.Entities.Contexts;

    public class MigrateAppService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public MigrateAppService(ILogger<MigrateAppService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("===================== Starting =====================");

            _timer = new Timer(Migrate, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(5));

            return Task.CompletedTask;
        }

        private void Migrate(object state)
        {
            _logger.LogInformation($"==================== Background is running =====================");

            using IServiceScope scope = _scopeFactory.CreateScope();
            scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("===================== Stoping =====================");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
