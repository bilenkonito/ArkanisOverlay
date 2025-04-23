namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Domain.Abstractions.Services;
using Domain.Models.Game;

public class UexGameCommodityPriceHydrationService(
    ServiceDependencyResolver dependencyResolver,
    IPurchasePriceProvider purchasePriceProvider,
    ISellPriceProvider sellPriceProvider
) : IHydrationServiceFor<GameCommodity>
{
    public async Task HydrateAsync(GameCommodity entity, CancellationToken cancellationToken = default)
    {
        await purchasePriceProvider.UpdatePriceTagAsync(entity);
        await sellPriceProvider.UpdatePriceTagAsync(entity);
    }

    public bool IsReady { get; private set; }

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, purchasePriceProvider, sellPriceProvider)
            .WaitUntilReadyAsync(cancellationToken)
            .ContinueWith(_ => IsReady = true, cancellationToken)
            .ConfigureAwait(false);
}
