namespace Arkanis.Overlay.Infrastructure.Repositories.Local;

using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    internal static IServiceCollection AddUexInMemoryRepositories(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IGameEntityRepository<>), typeof(UexGameEntityInMemoryRepository<>));
        return services;
    }
}
