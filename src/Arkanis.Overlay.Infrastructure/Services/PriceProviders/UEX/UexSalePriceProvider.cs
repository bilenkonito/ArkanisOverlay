namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders.UEX;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Game;
using Domain.Models.Trade;

public class UexSalePriceProvider(
    ServiceDependencyResolver resolver,
    IGameCommodityPricingRepository commodityPricingRepository,
    IGameItemPurchasePricingRepository itemPriceRepository
) : UexPriceProviderBase, ISalePriceProvider
{
    public async ValueTask UpdatePriceTagAsync(IGameSellable gameEntity)
        => await (gameEntity switch
        {
            GameCommodity commodity => UpdateCommodityAsync(commodity),
            GameItem item => UpdateItemAsync(item),
            _ => ValueTask.CompletedTask,
        });

    public async ValueTask<Bounds<PriceTag>> GetPriceTagAtAsync(IGameSellable gameEntity, IGameLocation gameLocation)
        => gameEntity switch
        {
            GameCommodity commodity => await GetCommodityPriceTagAsync(commodity, gameLocation),
            GameItem item => await GetItemPriceTagAsync(item, gameLocation),
            _ => Bounds.All(PriceTag.Unknown),
        };

    private async ValueTask<Bounds<PriceTag>> GetCommodityPriceTagAsync(GameCommodity gameEntity, IGameLocation gameLocation)
    {
        var prices = await commodityPricingRepository.GetAllForCommodityAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.SalePrice, PriceTag.MissingFor(gameLocation));
    }

    private async ValueTask<Bounds<PriceTag>> GetItemPriceTagAsync(GameItem gameEntity, IGameLocation gameLocation)
    {
        var prices = await itemPriceRepository.GetPurchasePricesForItemAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.SalePrice, PriceTag.MissingFor(gameLocation));
    }

    private async ValueTask UpdateCommodityAsync(GameCommodity gameEntity)
    {
        var prices = await commodityPricingRepository.GetAllForCommodityAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.SalePrice);
        gameEntity.UpdateSalePrices(priceBounds);
    }

    private async ValueTask UpdateItemAsync(GameItem gameEntity)
    {
        var prices = await itemPriceRepository.GetPurchasePricesForItemAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.SalePrice);
        gameEntity.UpdateSalePrices(priceBounds);
    }

    protected override async Task InitializeAsyncCore(CancellationToken cancellationToken)
        => await resolver.DependsOn(this, commodityPricingRepository, itemPriceRepository)
            .WaitUntilReadyAsync(cancellationToken);
}
