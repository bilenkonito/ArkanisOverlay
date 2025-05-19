namespace Arkanis.Overlay.Infrastructure.Services;

using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using PriceProviders;

public static class DependencyInjection
{
    public static IServiceCollection AddFakePriceProviders(this IServiceCollection services)
        => services
            .AddSingleton<FakePriceProvider>()
            .AddSingleton<IPriceProvider>(provider => provider.GetRequiredService<FakePriceProvider>())
            .AddSingleton<IMarketPriceProvider>(provider => provider.GetRequiredService<FakePriceProvider>())
            .AddSingleton<IPurchasePriceProvider>(provider => provider.GetRequiredService<FakePriceProvider>())
            .AddSingleton<ISalePriceProvider>(provider => provider.GetRequiredService<FakePriceProvider>())
            .AddSingleton<IRentPriceProvider>(provider => provider.GetRequiredService<FakePriceProvider>());

    public static IServiceCollection AddUexPriceProviders(this IServiceCollection services)
        => services
            .AddSingleton<IPriceProvider, PriceProviderAggregate>()
            .AddPriceProviderServices();

    public static IServiceCollection AddDatabaseExternalSyncCacheProviders(this IServiceCollection services)
        => services
            .AddSingleton(typeof(IExternalSyncCacheProvider<>), typeof(ExternalSyncDatabaseCacheProvider<>));

    public static IServiceCollection AddInMemorySearchServices(this IServiceCollection services)
        => services.AddScoped<ISearchService, InMemorySearchService>();

    public static IServiceCollection AddUserPreferencesFileManagerServices(this IServiceCollection services)
        => services
            .AddSingleton<IUserPreferencesManager, UserPreferencesJsonFileManager>()
            .AddSingleton<IUserPreferencesProvider>(provider => provider.GetRequiredService<IUserPreferencesManager>())
            .AddHostedService<AutoStartUserPreferencesUpdater>()
            .AddHostedService<UserPreferencesLoader>();
}
