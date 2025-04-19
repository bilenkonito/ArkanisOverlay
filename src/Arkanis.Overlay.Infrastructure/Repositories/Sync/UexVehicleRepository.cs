namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;

internal class UexVehicleRepository(IUexGameApi gameApi, IUexStaticApi staticApi, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<VehicleDTO, GameVehicle>(staticApi, mapper)
{
    protected override async Task<ICollection<VehicleDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetVehiclesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Result.Data ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(VehicleDTO source)
        => source.Id;
}
