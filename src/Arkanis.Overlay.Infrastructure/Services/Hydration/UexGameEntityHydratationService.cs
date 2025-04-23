namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Domain.Abstractions.Game;
using Abstractions;

public class GameEntityPriceHydrationService(
    ServiceDependencyResolver dependencyResolver,
    IEnumerable<IHydrationServiceFor> hydrationServices
) : IGameEntityHydrationService
{
    public async Task HydrateAsync<T>(T gameEntity, CancellationToken cancellationToken) where T : IGameEntity
    {
        foreach (var hydrationService in hydrationServices)
        {
            await hydrationService.HydrateAsync(gameEntity, cancellationToken);
        }
    }

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, hydrationServices)
            .WaitUntilReadyAsync(cancellationToken)
            .ConfigureAwait(false);
}
