namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions.Services;
using Domain.Models.Game;
using External.UEX;
using Local;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    private const string InterfaceName = nameof(IGameEntityExternalSyncRepository<GameEntity>);

    private static readonly Type ReferenceSourceType = typeof(DependencyInjection);

    public static IServiceCollection AddUexSyncRepositoryServices(this IServiceCollection services)
        => services
            .AddUexApiMappers()
            .AddAllUexApiClients()
            .AddSingleton<GameEntityLocalRepositoryDependencyResolver>()
            .Scan(scan => scan
                .FromAssembliesOf(ReferenceSourceType)
                .AddClasses(type => type.InNamespaceOf(ReferenceSourceType), false)
                .AsImplementedInterfaces(type => type.Name.StartsWith(InterfaceName, StringComparison.InvariantCulture))
            );
}
