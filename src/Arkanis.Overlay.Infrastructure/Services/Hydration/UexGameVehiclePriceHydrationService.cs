namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Arkanis.Overlay.Domain.Abstractions.Services;
using Domain.Models.Game;
using Abstractions;

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

    public async Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => await dependencyResolver.DependsOn(this, purchasePriceProvider, rentPriceProvider)
            .WaitUntilReadyAsync(cancellationToken)
            .ConfigureAwait(false);
}
