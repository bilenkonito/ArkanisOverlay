namespace Arkanis.Overlay.Infrastructure.Services.PriceProviders.UEX;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Trade;
using Repositories.Local.Specialised;

public class UexRentPriceProvider(
    ServiceDependencyResolver resolver,
    GameRentalPricingRepositoryAggregate pricingRepositoryAggregate
) : UexPriceProviderBase, IRentPriceProvider
{
    public async ValueTask UpdatePriceTagAsync(IGameRentable gameEntity)
    {
        var bounds = await GetBoundsAsync(gameEntity, null);
        gameEntity.UpdateRentPrices(bounds);
    }

    public async ValueTask<Bounds<PriceTag>> GetPriceTagAtAsync(IGameRentable gameEntity, IGameLocation gameLocation)
        => await GetBoundsAsync(gameEntity, gameLocation);

    private async ValueTask<Bounds<PriceTag>> GetBoundsAsync(IGameRentable gameEntity, IGameLocation? gameLocation)
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
