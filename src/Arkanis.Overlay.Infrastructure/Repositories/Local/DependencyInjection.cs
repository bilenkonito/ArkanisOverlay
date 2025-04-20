namespace Arkanis.Overlay.Infrastructure.Repositories.Local;

using Domain.Abstractions.Services;
using Domain.Models.Game;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    internal static IServiceCollection AddUexInMemoryRepositories(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IGameEntityRepository<>), typeof(UexGameEntityInMemoryRepository<>));
        services.AddSingleton<IGameEntityAggregateRepository, GameEntityAggregateRepository>();

        var genericRepositoryAdapterType = typeof(UexGameEntityGenericRepositoryAdapter<>);
        foreach (var gameEntityType in GameEntityConstants.GameEntityTypes)
        {
            var serviceType = genericRepositoryAdapterType.MakeGenericType(gameEntityType);
            services.AddSingleton(typeof(IGameEntityRepository), serviceType);
        }

        return services;
    }
}
