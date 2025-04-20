namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Local;
using Microsoft.Extensions.DependencyInjection;
using Services;

public static class DependencyInjection
{
    private static readonly Type ReferenceSourceType = typeof(DependencyInjection);

    internal static IServiceCollection AddUexSyncRepositoryServices(this IServiceCollection services)
        => services
            .AddUexApiMappers()
            .AddSingleton<UexGameDataStateProvider>()
            .AddSingleton<GameEntityRepositoryDependencyResolver>()
            .AddSingleton(typeof(GameEntityRepositorySyncManager<>))
            .Scan(scan => scan
                .FromAssembliesOf(ReferenceSourceType)
                .AddClasses(type => type.InNamespaceOf(ReferenceSourceType), false)
                .AsImplementedInterfaces()
            );
}
