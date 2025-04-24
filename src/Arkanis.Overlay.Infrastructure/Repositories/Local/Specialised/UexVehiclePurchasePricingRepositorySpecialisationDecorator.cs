namespace Arkanis.Overlay.Infrastructure.Repositories.Local.Specialised;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models.Game;

internal class UexVehiclePurchasePricingRepositorySpecialisationDecorator(IGameEntityRepository<GameVehiclePurchasePricing> traitRepository)
    : UexPricingRepositorySpecialisationDecorator<GameVehiclePurchasePricing, GameVehicle>(traitRepository), IGameVehiclePurchasePricingRepository
{
    public async ValueTask<ICollection<GameVehiclePurchasePricing>> GetPurchasePricesForVehicleAsync(
        IDomainId domainId,
        CancellationToken cancellationToken = default
    )
        => await GetAllForOwnerAsync(domainId, cancellationToken);
}
