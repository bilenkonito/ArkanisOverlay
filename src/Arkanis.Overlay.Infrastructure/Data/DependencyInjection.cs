namespace Arkanis.Overlay.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddUexDatabaseServices(this IServiceCollection services)
        => services
            .AddSingleton<IDbContextFactory<UEXContext>, ClientUexDbContextFactory>()
            .AddScoped<UEXContext>(provider => provider.GetRequiredService<IDbContextFactory<UEXContext>>().CreateDbContext());
}
