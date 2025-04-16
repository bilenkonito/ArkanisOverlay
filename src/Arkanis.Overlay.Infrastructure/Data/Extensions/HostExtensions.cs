namespace Arkanis.Overlay.Infrastructure.Data.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class HostExtensions
{
    public static async Task MigrateDatabaseAsync<TContext>(this IHost host, CancellationToken cancellationToken = default) where TContext : DbContext
    {
        await using var serviceScope = host.Services.CreateAsyncScope();

        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
        await using var context = serviceScope.ServiceProvider.GetRequiredService<TContext>();

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync(cancellationToken);
        pendingMigrations = pendingMigrations.ToList();
        foreach (var pendingMigration in pendingMigrations)
        {
            logger.LogDebug("Detected pending database migration: {Migration}", pendingMigration);
        }

        logger.LogInformation("Applying {PendingMigrationCount} pending migrations", pendingMigrations.Count());
        await context.Database.MigrateAsync(cancellationToken);
    }
}
