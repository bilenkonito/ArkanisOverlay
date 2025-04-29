namespace Arkanis.Overlay.Infrastructure.Repositories.Local.Specialised;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models.Game;

internal class UexItemPricingRepositorySpecialisationDecorator(IGameEntityRepository<GameItemPurchasePricing> traitRepository)
    : UexPricingRepositorySpecialisationDecorator<GameItemPurchasePricing, GameItem>(traitRepository), IGameItemPurchasePricingRepository
{
    public async ValueTask<ICollection<GameItemPurchasePricing>> GetPurchasePricesForItemAsync(
        IDomainId domainId,
        CancellationToken cancellationToken = default
    )
        => await GetAllForOwnerAsync(domainId, cancellationToken);
}
