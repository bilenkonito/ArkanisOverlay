namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders.UEX;

using Common;
using Domain.Models;
using Domain.Models.Game;
using Domain.Models.Trade;
using NodaMoney;

public abstract class UexPriceProviderBase : SelfInitializableServiceBase
{
    protected static Bounds<PriceTag> CreateBoundsFrom<T>(ICollection<T> prices, Func<T, Money> priceSelector, PriceTag? fallback = null)
        where T : GameEntityPricing
    {
        var minDto = prices.Where(dto => priceSelector(dto).Amount > 0).MinBy(priceSelector);
        var maxDto = prices.Where(dto => priceSelector(dto).Amount > 0).MaxBy(priceSelector);

        decimal? avgValue = maxDto is not null
            ? prices.Select(priceSelector)
                .Where(money => money.Amount > 0)
                .Average(money => money.Amount)
            : null;

        fallback ??= PriceTag.Unknown;
        return new Bounds<PriceTag>(
            minDto is not null
                ? new KnownPriceTag(priceSelector(minDto), minDto.Terminal, minDto.UpdatedAt)
                : fallback,
            maxDto is not null
                ? new KnownPriceTag(priceSelector(maxDto), maxDto.Terminal, maxDto.UpdatedAt)
                : fallback,
            avgValue is not null
                ? new AggregatePriceTag(new Money(avgValue.Value, ApplicationConstants.GameCurrency))
                : fallback
        );
    }
}
