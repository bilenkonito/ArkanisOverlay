namespace Arkanis.Overlay.Infrastructure.Repositories.Local.Specialised;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models.Game;

internal class UexVehicleRentalPricingRepositorySpecialisationDecorator(IGameEntityRepository<GameVehicleRentalPricing> traitRepository)
    : UexPricingRepositorySpecialisationDecorator<GameVehicleRentalPricing, GameVehicle>(traitRepository), IGameVehicleRentalPricingRepository
{
    public async ValueTask<ICollection<GameVehicleRentalPricing>> GetRentalPricesForVehicleAsync(
        IDomainId domainId,
        CancellationToken cancellationToken = default
    )
        => await GetAllForOwnerAsync(domainId, cancellationToken);
}
