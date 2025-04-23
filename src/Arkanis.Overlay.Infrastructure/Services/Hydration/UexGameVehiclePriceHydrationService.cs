namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Domain.Abstractions.Services;
using Domain.Models.Game;

public class UexGameVehiclePriceHydrationService(
    ServiceDependencyResolver dependencyResolver,
    IPurchasePriceProvider purchasePriceProvider,
    IRentPriceProvider rentPriceProvider
) : IHydrationServiceFor<GameVehicle>
{
    public async Task HydrateAsync(GameVehicle entity, CancellationToken cancellationToken = default)
    {
        await purchasePriceProvider.UpdatePriceTagAsync(entity);
        await rentPriceProvider.UpdatePriceTagAsync(entity);
    }

    public bool IsReady { get; private set; }

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, purchasePriceProvider, rentPriceProvider)
            .WaitUntilReadyAsync(cancellationToken)
            .ContinueWith(_ => IsReady = true, cancellationToken)
            .ConfigureAwait(false);
}
