namespace Arkanis.Overlay.Infrastructure.Data;

using Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddOverlaySqliteDatabaseServices(this IServiceCollection services)
        => services
            .AddSingleton<IDbContextFactory<OverlayDbContext>, ClientOverlayDbContextFactory>()
            .AddScoped<OverlayDbContext>(provider => provider.GetRequiredService<IDbContextFactory<OverlayDbContext>>().CreateDbContext())
            .AddDatabaseMappers();
}
