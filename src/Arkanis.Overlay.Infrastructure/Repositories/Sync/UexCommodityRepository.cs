namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;

internal class UexCommodityRepository(IUexCommoditiesApi commoditiesApi, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<CommodityDTO, GameCommodity>(mapper)
{
    protected override async Task<ICollection<CommodityDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await commoditiesApi.GetCommoditiesAsync(cancellationToken).ConfigureAwait(false);
        return response.Result.Data?.Where(x => x.Is_available > 0).ToList() ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(CommodityDTO source)
        => source.Id;
}
