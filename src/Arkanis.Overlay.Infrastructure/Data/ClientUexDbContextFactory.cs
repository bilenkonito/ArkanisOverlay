namespace Arkanis.Overlay.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class ClientUexDbContextFactory(IServiceProvider serviceProvider, IConfiguration configuration) : IDbContextFactory<UEXContext>
{
    private const string ConnectionName = "UexDatabase";

    public UEXContext CreateDbContext()
    {
        var connectionString = configuration.GetConnectionString(ConnectionName);
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();

        var optionsBuilder = new DbContextOptionsBuilder<UEXContext>();
        optionsBuilder.UseApplicationServiceProvider(serviceProvider);
        optionsBuilder.UseLoggerFactory(loggerFactory);
        optionsBuilder.UseSqlite(connectionString);

        if (hostEnvironment.IsDevelopment())
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        return new UEXContext(optionsBuilder.Options);
    }
}
