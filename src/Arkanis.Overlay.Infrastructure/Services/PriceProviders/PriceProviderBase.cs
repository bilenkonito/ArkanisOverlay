namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders;

using Domain.Abstractions.Game;
using Domain.Models;
using Domain.Models.Game;
using Domain.Models.Trade;

public abstract class PriceProviderBase : SelfInitializableServiceBase
{
    protected static Bounds<PriceTag> CreateBoundsFrom<T>(ICollection<T> prices, Func<T, GameCurrency> priceSelector, PriceTag? fallback = null)
        where T : IGameEntityPrice
    {
        var minDto = prices.Where(dto => priceSelector(dto).Amount > 0).MinBy(priceSelector);
        var maxDto = prices.Where(dto => priceSelector(dto).Amount > 0).MaxBy(priceSelector);

        int? avgValue = maxDto is not null
            ? (int)prices.Select(priceSelector)
                .Where(money => money.Amount > 0)
                .Average(money => money.Amount)
            : null;

        fallback ??= PriceTag.Unknown;
        return new Bounds<PriceTag>(
            CreatePriceTag(minDto),
            CreatePriceTag(maxDto),
            avgValue is not null
                ? new AggregatePriceTag(new GameCurrency(avgValue.Value))
                : fallback
        );

        PriceTag CreatePriceTag(T? gameEntityPrice)
        {
            if (gameEntityPrice is null)
            {
                return fallback;
            }

            var price = priceSelector(gameEntityPrice);
            return gameEntityPrice switch
            {
                GameEntityTerminalPrice context => new KnownPriceTagWithLocation(price, context.Terminal, context.UpdatedAt),
                GameEntityPrice context => new KnownPriceTag(price, context.UpdatedAt),
                not null => new KnownPriceTag(price, DateTimeOffset.UnixEpoch),
                _ => fallback,
            };
        }
    }
}
