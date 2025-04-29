namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models.Game;

public interface IGameVehicleRentalPricingRepository : IGameEntityRepository<GameVehicleRentalPricing>
{
    ValueTask<ICollection<GameVehicleRentalPricing>> GetRentalPricesForVehicleAsync(IDomainId domainId, CancellationToken cancellationToken = default);
}
