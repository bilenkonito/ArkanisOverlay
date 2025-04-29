namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models.Game;

public interface IGameCommodityPricingRepository : IGameEntityRepository<GameCommodityPricing>
{
    ValueTask<ICollection<GameCommodityPricing>> GetAllForCommodityAsync(IDomainId domainId, CancellationToken cancellationToken = default);
}
