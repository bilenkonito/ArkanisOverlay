namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models.Trade;

public interface IPurchasePriceProvider
{
    ValueTask UpdatePriceTagAsync(IGamePurchasable gameEntity);

    ValueTask<PriceTag> GetPriceTagAtAsync(IGamePurchasable gameEntity, IGameLocation gameLocation);
}
