namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Domain.Abstractions.Game;
using Domain.Abstractions.Services;

/// <summary>
///     Hydrates any <see cref="IGamePurchasable" /> entity with all appropriate data points.
/// </summary>
/// <param name="dependencyResolver">A dependency resolver</param>
/// <param name="purchasePriceProvider">A purchase price provider</param>
public class UexGamePurchasableHydrationService(
    ServiceDependencyResolver dependencyResolver,
    IPurchasePriceProvider purchasePriceProvider
) : IHydrationServiceFor<IGamePurchasable>
{
    public async Task HydrateAsync(IGamePurchasable entity, CancellationToken cancellationToken = default)
        => await purchasePriceProvider.UpdatePriceTagAsync(entity);

    public bool IsReady { get; private set; }

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, purchasePriceProvider)
            .WaitUntilReadyAsync(cancellationToken)
            .ContinueWith(_ => IsReady = true, cancellationToken)
            .ConfigureAwait(false);
}
