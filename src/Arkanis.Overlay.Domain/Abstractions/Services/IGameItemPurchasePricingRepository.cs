namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models.Game;

public interface IGameItemPurchasePricingRepository : IGameEntityRepository<GameItemPurchasePricing>
{
    ValueTask<ICollection<GameItemPurchasePricing>> GetPurchasePricesForItemAsync(IDomainId domainId, CancellationToken cancellationToken = default);
}
