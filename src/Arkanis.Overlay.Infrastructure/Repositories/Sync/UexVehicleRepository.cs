namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;

internal class UexVehicleRepository(IUexGameApi gameApi, UexGameDataStateProvider stateProvider, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<VehicleDTO, GameVehicle>(stateProvider, mapper)
{
    protected override async Task<UexApiResponse<ICollection<VehicleDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetVehiclesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return CreateResponse(response, response.Result.Data);
    }

    protected override double? GetSourceApiId(VehicleDTO source)
        => source.Id;
}
