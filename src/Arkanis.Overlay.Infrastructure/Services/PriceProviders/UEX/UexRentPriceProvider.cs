namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders.UEX;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Game;
using Domain.Models.Trade;

public class UexRentPriceProvider(
    ServiceDependencyResolver resolver,
    IGameVehicleRentalPricingRepository vehiclePriceRepository
) : UexPriceProviderBase, IRentPriceProvider
{
    public async ValueTask UpdatePriceTagAsync(IGameRentable gameEntity)
        => await (gameEntity switch
        {
            GameVehicle item => UpdateVehicleAsync(item),
            _ => ValueTask.CompletedTask,
        });

    public async ValueTask<Bounds<PriceTag>> GetPriceTagAtAsync(IGameRentable gameEntity, IGameLocation gameLocation)
        => gameEntity switch
        {
            GameVehicle vehicle => await GetVehiclePriceTagAsync(vehicle, gameLocation),
            _ => Bounds.All(PriceTag.Unknown),
        };

    private async ValueTask<Bounds<PriceTag>> GetVehiclePriceTagAsync(GameVehicle gameEntity, IGameLocation gameLocation)
    {
        var prices = await vehiclePriceRepository.GetRentalPricesForVehicleAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.Price, fallback: PriceTag.MissingFor(gameLocation));
    }

    private async ValueTask UpdateVehicleAsync(GameVehicle gameEntity)
    {
        var prices = await vehiclePriceRepository.GetRentalPricesForVehicleAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.Price);
        gameEntity.UpdateRentPrices(priceBounds);
    }

    protected override async Task InitializeAsyncCore(CancellationToken cancellationToken)
        => await resolver.DependsOn(this, vehiclePriceRepository)
            .WaitUntilReadyAsync(cancellationToken);
}
