namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders.UEX;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Trade;

public class UexSalePriceProvider(
    ServiceDependencyResolver resolver,
    IGameCommodityPricingRepository commodityPricingRepository,
    IGameItemPurchasePricingRepository itemPriceRepository
) : UexPriceProviderBase, ISalePriceProvider
{
    public async ValueTask UpdatePriceTagAsync(IGameSellable gameEntity)
    {
        if (gameEntity.EntityCategory is GameEntityCategory.Commodity)
        {
            await UpdateCommodity(gameEntity);
        }
        else if (gameEntity.EntityCategory is GameEntityCategory.Item)
        {
            await UpdateItemAsync(gameEntity);
        }
    }

    public async ValueTask<Bounds<PriceTag>> GetPriceTagAtAsync(IGameSellable gameEntity, IGameLocation gameLocation)
        => gameEntity.EntityCategory switch
        {
            GameEntityCategory.Commodity => await GetCommodityPriceTagAsync(gameEntity, gameLocation),
            GameEntityCategory.Item => await GetItemPriceTagAsync(gameEntity, gameLocation),
            _ => Bounds.All(PriceTag.Unknown),
        };

    private async ValueTask<Bounds<PriceTag>> GetCommodityPriceTagAsync(IGameSellable gameEntity, IGameLocation gameLocation)
    {
        var prices = await commodityPricingRepository.GetAllForCommodityAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.SalePrice, PriceTag.MissingFor(gameLocation));
    }

    private async ValueTask<Bounds<PriceTag>> GetItemPriceTagAsync(IGameSellable gameEntity, IGameLocation gameLocation)
    {
        var prices = await itemPriceRepository.GetPurchasePricesForItemAsync(gameEntity.Id);
        var pricesAtLocation = prices.Where(x => gameLocation.IsOrContains(x.Terminal)).ToList();
        return CreateBoundsFrom(pricesAtLocation, price => price.SalePrice, PriceTag.MissingFor(gameLocation));
    }

    private async ValueTask UpdateCommodity(IGameSellable gameEntity)
    {
        var prices = await commodityPricingRepository.GetAllForCommodityAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.SalePrice);
        gameEntity.UpdateSalePrices(priceBounds);
    }

    private async ValueTask UpdateItemAsync(IGameSellable gameEntity)
    {
        var prices = await itemPriceRepository.GetPurchasePricesForItemAsync(gameEntity.Id);
        var priceBounds = CreateBoundsFrom(prices, price => price.SalePrice);
        gameEntity.UpdateSalePrices(priceBounds);
    }

    protected override async Task InitializeAsyncCore(CancellationToken cancellationToken)
        => await resolver.DependsOn(this, commodityPricingRepository, itemPriceRepository)
            .WaitUntilReadyAsync(cancellationToken);
}
