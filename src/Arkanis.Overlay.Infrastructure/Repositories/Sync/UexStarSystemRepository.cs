namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;

internal class UexStarSystemRepository(IUexGameApi gameApi, UexGameDataStateProvider stateProvider, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<UniverseStarSystemDTO, GameStarSystem>(stateProvider, mapper)
{
    protected override async Task<UexApiResponse<ICollection<UniverseStarSystemDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetStarSystemsAsync(cancellationToken).ConfigureAwait(false);
        return CreateResponse(response, response.Result.Data?.Where(x => x.Is_available > 0).ToList());
    }

    protected override double? GetSourceApiId(UniverseStarSystemDTO source)
        => source.Id;
}
