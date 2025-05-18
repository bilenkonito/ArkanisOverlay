namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models;
using Models.Trade;

public interface IMarketPriceProvider : IDependable
{
    ValueTask<Bounds<PriceTag>> GetPriceTagAsync(IGameEntity gameEntity);
}
