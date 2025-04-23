namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Arkanis.Overlay.Domain.Abstractions.Services;
using Domain.Models.Game;
using Abstractions;

public class UexGameItemPriceHydrationService(
    ServiceDependencyResolver dependencyResolver,
    IPurchasePriceProvider purchasePriceProvider
) : IHydrationServiceFor<GameItem>
{
    public async Task HydrateAsync(GameItem entity, CancellationToken cancellationToken = default)
        => await purchasePriceProvider.UpdatePriceTagAsync(entity);

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, purchasePriceProvider)
            .WaitUntilReadyAsync(cancellationToken)
            .ConfigureAwait(false);
}
