namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders.UEX;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Trade;

public class UexRentPriceProvider(
    ServiceDependencyResolver resolver,
    IGameVehicleRentalPricingRepository vehiclePriceRepository
) : UexPriceProviderBase, IRentPriceProvider
{
    public async ValueTask UpdatePriceTagAsync(IGameRentable gameEntity)
    {
        if (gameEntity.EntityCategory is GameEntityCategory.Commodity)
        {
            await UpdateVehicleAsync(gameEntity);
        }
    }

    public async ValueTask<Bounds<PriceTag>> GetPriceTagAtAsync(IGameRentable gameEntity, IGameLocation gameLocation)
        => gameEntity.EntityCategory switch
        {
            GameEntityCategory.Commodity => await GetVehiclePriceTagAsync(gameEntity, gameLocation),
            _ => Bounds.All(PriceTag.Unknown),
        };

    private async ValueTask<Bounds<PriceTag>> GetVehiclePriceTagAsync(IGameRentable gameEntity, IGameLocation gameLocation)
    {
        var prices = await vehiclePriceRepository.GetRentalPricesForVehicleAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.Price);
    }

    private async ValueTask UpdateVehicleAsync(IGameRentable gameEntity)
    {
        var prices = await vehiclePriceRepository.GetRentalPricesForVehicleAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.Price);
        gameEntity.UpdateRentPrices(priceBounds);
    }

    protected override async Task InitializeAsyncCore(CancellationToken cancellationToken)
        => await resolver.DependsOn(this, vehiclePriceRepository)
            .WaitUntilReadyAsync(cancellationToken);
}
