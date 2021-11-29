using GenshinBotCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenshinBotCore.Services.Workers
{
    public class SqliteDbInitializer : BackgroundService
    {
        public SqliteDbInitializer(IServiceProvider serviceProvider) : base()
        {
            this.serviceProvider = serviceProvider;
        }

        private readonly IServiceProvider serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SqliteDbInitializer>>();

            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                logger.LogWarning("Database schema out of date, updating...");
                dbContext.Database.Migrate();
            }
            logger.LogInformation("Database schema is latest.");
        }
    }
}
