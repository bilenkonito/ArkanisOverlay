namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Domain.Abstractions.Game;
using Domain.Abstractions.Services;

/// <summary>
///     Hydrates any <see cref="IGameSellable" /> entity with all appropriate data points.
/// </summary>
/// <param name="dependencyResolver">A dependency resolver</param>
/// <param name="salePriceProvider">A sale price provider</param>
public class UexGameSellableHydrationService(
    ServiceDependencyResolver dependencyResolver,
    ISalePriceProvider salePriceProvider
) : IHydrationServiceFor<IGameSellable>
{
    public async Task HydrateAsync(IGameSellable entity, CancellationToken cancellationToken = default)
        => await salePriceProvider.UpdatePriceTagAsync(entity);

    public bool IsReady { get; private set; }

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, salePriceProvider)
            .WaitUntilReadyAsync(cancellationToken)
            .ContinueWith(_ => IsReady = true, cancellationToken)
            .ConfigureAwait(false);
}
