namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Trade;
using Repositories.Local.Specialised;

public class SalePriceProvider(
    ServiceDependencyResolver resolver,
    GameSalePricingRepositoryAggregate pricingRepositoryAggregate
) : PriceProviderBase, ISalePriceProvider
{
    public async ValueTask UpdatePriceTagAsync(IGameSellable gameEntity)
    {
        var bounds = await GetBoundsAsync(gameEntity, null);
        gameEntity.UpdateSalePrices(bounds);
    }

    public async ValueTask<Bounds<PriceTag>> GetPriceTagAtAsync(IGameSellable gameEntity, IGameLocation gameLocation)
        => await GetBoundsAsync(gameEntity, gameLocation);

    private async ValueTask<Bounds<PriceTag>> GetBoundsAsync(IGameSellable gameEntity, IGameLocation? gameLocation)
    {
        var fallback = gameLocation switch
        {
            not null => PriceTag.MissingFor(gameLocation),
            _ => PriceTag.Unknown,
        };

        var prices = await pricingRepositoryAggregate.GetAllForAsync(gameEntity.Id);
        var filtered = gameLocation switch
        {
            not null => prices.Where(price => price is IGameLocatedAt locatedAt && gameLocation.IsOrContains(locatedAt.Location)).ToList(),
            _ => prices,
        };

        return CreateBoundsFrom(filtered, price => price.Price, fallback);
    }

    protected override Task InitializeAsyncCore(CancellationToken cancellationToken)
        => resolver.DependsOn(this, pricingRepositoryAggregate)
            .WaitUntilReadyAsync(cancellationToken);
}
