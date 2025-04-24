namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders.UEX;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Trade;

public class UexPurchasePriceProvider(
    ServiceDependencyResolver resolver,
    IGameCommodityPricingRepository commodityPriceRepository,
    IGameItemPurchasePricingRepository itemPriceRepository,
    IGameVehiclePurchasePricingRepository vehiclePriceRepository
) : UexPriceProviderBase, IPurchasePriceProvider
{
    public async ValueTask UpdatePriceTagAsync(IGamePurchasable gameEntity)
    {
        if (gameEntity.EntityCategory is GameEntityCategory.Commodity)
        {
            await UpdateCommodityAsync(gameEntity);
        }
        else if (gameEntity.EntityCategory is GameEntityCategory.Item)
        {
            await UpdateItemAsync(gameEntity);
        }
        else if (gameEntity.EntityCategory is GameEntityCategory.GroundVehicle or GameEntityCategory.SpaceShip)
        {
            await UpdateVehicleAsync(gameEntity);
        }
    }

    public async ValueTask<Bounds<PriceTag>> GetPriceTagAtAsync(IGamePurchasable gameEntity, IGameLocation gameLocation)
        => gameEntity.EntityCategory switch
        {
            GameEntityCategory.Commodity => await GetCommodityPriceTagAsync(gameEntity, gameLocation),
            GameEntityCategory.Item => await GetItemPriceTagAsync(gameEntity, gameLocation),
            GameEntityCategory.GroundVehicle or GameEntityCategory.SpaceShip => await GetVehiclePriceTagAsync(gameEntity, gameLocation),
            _ => Bounds.All(PriceTag.Unknown),
        };

    private async ValueTask<Bounds<PriceTag>> GetVehiclePriceTagAsync(IGamePurchasable gameEntity, IGameLocation gameLocation)
    {
        var prices = await vehiclePriceRepository.GetPurchasePricesForVehicleAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.Price);
    }

    private async ValueTask<Bounds<PriceTag>> GetItemPriceTagAsync(IGamePurchasable gameEntity, IGameLocation gameLocation)
    {
        var prices = await itemPriceRepository.GetPurchasePricesForItemAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.Price);
    }

    private async ValueTask<Bounds<PriceTag>> GetCommodityPriceTagAsync(IGamePurchasable gameEntity, IGameLocation gameLocation)
    {
        var prices = await commodityPriceRepository.GetAllForCommodityAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.PurchasePrice);
    }

    private async ValueTask UpdateCommodityAsync(IGamePurchasable gameEntity)
    {
        var prices = await commodityPriceRepository.GetAllForCommodityAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.PurchasePrice);
        gameEntity.UpdatePurchasePrices(priceBounds);
    }

    private async ValueTask UpdateItemAsync(IGamePurchasable gameEntity)
    {
        var prices = await itemPriceRepository.GetPurchasePricesForItemAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.Price);
        gameEntity.UpdatePurchasePrices(priceBounds);
    }

    private async ValueTask UpdateVehicleAsync(IGamePurchasable gameEntity)
    {
        var prices = await vehiclePriceRepository.GetPurchasePricesForVehicleAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.Price);
        gameEntity.UpdatePurchasePrices(priceBounds);
    }

    protected override async Task InitializeAsyncCore(CancellationToken cancellationToken)
        => await resolver.DependsOn(this, commodityPriceRepository, itemPriceRepository, vehiclePriceRepository)
            .WaitUntilReadyAsync(cancellationToken);
}
