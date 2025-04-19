namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;

internal class UexStarSystemRepository(IUexGameApi gameApi, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<UniverseStarSystemDTO, GameStarSystem>(mapper)
{
    protected override async Task<ICollection<UniverseStarSystemDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetStarSystemsAsync(cancellationToken).ConfigureAwait(false);
        return response.Result.Data?.Where(x => x.Is_available > 0).ToList() ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(UniverseStarSystemDTO source)
        => source.Id;
}
