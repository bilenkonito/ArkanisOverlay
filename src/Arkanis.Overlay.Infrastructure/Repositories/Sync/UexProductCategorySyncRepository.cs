namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions.Services;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;
using Microsoft.Extensions.Logging;

internal class UexProductCategorySyncRepository(
    IUexGameApi gameApi,
    UexServiceStateProvider stateProvider,
    IExternalSyncCacheProvider<UexProductCategorySyncRepository> cacheProvider,
    UexApiDtoMapper mapper,
    ILogger<UexProductCategorySyncRepository> logger
) : UexGameEntitySyncRepositoryBase<CategoryDTO, GameProductCategory>(stateProvider, cacheProvider, mapper, logger)
{
    protected override async Task<UexApiResponse<ICollection<CategoryDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetCategoriesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return CreateResponse(response, response.Result.Data);
    }

    protected override UexApiGameEntityId? GetSourceApiId(CategoryDTO source)
        => source.Id is not null
            ? Mapper.CreateGameEntityId(source, x => x.Id)
            : null;
}
