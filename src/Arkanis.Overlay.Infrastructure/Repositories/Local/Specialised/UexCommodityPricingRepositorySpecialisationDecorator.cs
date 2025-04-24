namespace Arkanis.Overlay.Infrastructure.Repositories.Local.Specialised;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models.Game;

internal class UexCommodityPricingRepositorySpecialisationDecorator(IGameEntityRepository<GameCommodityPricing> traitRepository)
    : UexPricingRepositorySpecialisationDecorator<GameCommodityPricing, GameCommodity>(traitRepository), IGameCommodityPricingRepository
{
    public async ValueTask<ICollection<GameCommodityPricing>> GetAllForCommodityAsync(IDomainId domainId, CancellationToken cancellationToken = default)
        => await GetAllForOwnerAsync(domainId, cancellationToken);
}
