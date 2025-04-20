namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;

internal class UexCommodityRepository(IUexCommoditiesApi commoditiesApi, UexGameDataStateProvider stateProvider, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<CommodityDTO, GameCommodity>(stateProvider, mapper)
{
    protected override async Task<UexApiResponse<ICollection<CommodityDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var response = await commoditiesApi.GetCommoditiesAsync(cancellationToken).ConfigureAwait(false);
        return CreateResponse(response, response.Result.Data?.Where(x => x.Is_available > 0).ToList());
    }

    protected override double? GetSourceApiId(CommodityDTO source)
        => source.Id;
}
