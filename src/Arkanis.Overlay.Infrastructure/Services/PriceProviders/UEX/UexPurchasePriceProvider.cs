namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders.UEX;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Game;
using Domain.Models.Trade;

public class UexPurchasePriceProvider(
    ServiceDependencyResolver resolver,
    IGameCommodityPricingRepository commodityPriceRepository,
    IGameItemPurchasePricingRepository itemPriceRepository,
    IGameVehiclePurchasePricingRepository vehiclePriceRepository
) : UexPriceProviderBase, IPurchasePriceProvider
{
    public async ValueTask UpdatePriceTagAsync(IGamePurchasable gameEntity)
        => await (gameEntity switch
        {
            GameCommodity commodity => UpdateCommodityAsync(commodity),
            GameItem item => UpdateItemAsync(item),
            GameVehicle item => UpdateVehicleAsync(item),
            _ => ValueTask.CompletedTask,
        });

    public async ValueTask<Bounds<PriceTag>> GetPriceTagAtAsync(IGamePurchasable gameEntity, IGameLocation gameLocation)
        => gameEntity switch
        {
            GameCommodity commodity => await GetCommodityPriceTagAsync(commodity, gameLocation),
            GameItem item => await GetItemPriceTagAsync(item, gameLocation),
            GameVehicle vehicle => await GetVehiclePriceTagAsync(vehicle, gameLocation),
            _ => Bounds.All(PriceTag.Unknown),
        };

    private async ValueTask<Bounds<PriceTag>> GetVehiclePriceTagAsync(GameVehicle gameEntity, IGameLocation gameLocation)
    {
        var prices = await vehiclePriceRepository.GetPurchasePricesForVehicleAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.Price, PriceTag.MissingFor(gameLocation));
    }

    private async ValueTask<Bounds<PriceTag>> GetItemPriceTagAsync(GameItem gameEntity, IGameLocation gameLocation)
    {
        var prices = await itemPriceRepository.GetPurchasePricesForItemAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.PurchasePrice, PriceTag.MissingFor(gameLocation));
    }

    private async ValueTask<Bounds<PriceTag>> GetCommodityPriceTagAsync(GameCommodity gameEntity, IGameLocation gameLocation)
    {
        var prices = await commodityPriceRepository.GetAllForCommodityAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.PurchasePrice, PriceTag.MissingFor(gameLocation));
    }

    private async ValueTask UpdateCommodityAsync(GameCommodity gameEntity)
    {
        var prices = await commodityPriceRepository.GetAllForCommodityAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.PurchasePrice);
        gameEntity.UpdatePurchasePrices(priceBounds);
    }

    private async ValueTask UpdateItemAsync(GameItem gameEntity)
    {
        var prices = await itemPriceRepository.GetPurchasePricesForItemAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.PurchasePrice);
        gameEntity.UpdatePurchasePrices(priceBounds);
    }

    private async ValueTask UpdateVehicleAsync(GameVehicle gameEntity)
    {
        var prices = await vehiclePriceRepository.GetPurchasePricesForVehicleAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.Price);
        gameEntity.UpdatePurchasePrices(priceBounds);
    }

    protected override async Task InitializeAsyncCore(CancellationToken cancellationToken)
        => await resolver.DependsOn(this, commodityPriceRepository, itemPriceRepository, vehiclePriceRepository)
            .WaitUntilReadyAsync(cancellationToken);
}
