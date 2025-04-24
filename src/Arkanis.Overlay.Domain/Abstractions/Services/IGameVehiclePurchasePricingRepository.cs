namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models.Game;

public interface IGameVehiclePurchasePricingRepository : IGameEntityRepository<GameVehiclePurchasePricing>
{
    ValueTask<ICollection<GameVehiclePurchasePricing>> GetPurchasePricesForVehicleAsync(IDomainId domainId, CancellationToken cancellationToken = default);
}
