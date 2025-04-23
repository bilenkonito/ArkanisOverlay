namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Domain.Abstractions.Services;
using Domain.Models.Game;

public class UexGameItemPriceHydrationService(
    ServiceDependencyResolver dependencyResolver,
    IPurchasePriceProvider purchasePriceProvider
) : IHydrationServiceFor<GameItem>
{
    public async Task HydrateAsync(GameItem entity, CancellationToken cancellationToken = default)
        => await purchasePriceProvider.UpdatePriceTagAsync(entity);

    public bool IsReady { get; private set; }

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, purchasePriceProvider)
            .WaitUntilReadyAsync(cancellationToken)
            .ContinueWith(_ => IsReady = true, cancellationToken)
            .ConfigureAwait(false);
}
