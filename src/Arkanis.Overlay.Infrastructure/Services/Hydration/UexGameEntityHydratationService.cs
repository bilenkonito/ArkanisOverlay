namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Domain.Abstractions.Game;

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

    public bool IsReady { get; private set; }

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, hydrationServices)
            .WaitUntilReadyAsync(cancellationToken)
            .ContinueWith(_ => IsReady = true, cancellationToken)
            .ConfigureAwait(false);
}
