namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Domain.Abstractions.Game;
using Domain.Abstractions.Services;

/// <summary>
///     Hydrates any <see cref="IGameRentable" /> entity with all appropriate data points.
/// </summary>
/// <param name="dependencyResolver">A dependency resolver</param>
/// <param name="rentPriceProvider">A rental price provider</param>
public class UexGameRentableHydrationService(
    ServiceDependencyResolver dependencyResolver,
    IRentPriceProvider rentPriceProvider
) : IHydrationServiceFor<IGameRentable>
{
    public async Task HydrateAsync(IGameRentable entity, CancellationToken cancellationToken = default)
        => await rentPriceProvider.UpdatePriceTagAsync(entity);

    public bool IsReady { get; private set; }

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, rentPriceProvider)
            .WaitUntilReadyAsync(cancellationToken)
            .ContinueWith(_ => IsReady = true, cancellationToken)
            .ConfigureAwait(false);
}
