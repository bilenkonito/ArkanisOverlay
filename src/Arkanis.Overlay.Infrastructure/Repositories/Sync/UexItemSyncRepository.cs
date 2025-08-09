namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using System.Collections.Concurrent;
using System.Globalization;
using Data.Mappers;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Domain.Enums;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;
using Microsoft.Extensions.Logging;
using MoreAsyncLINQ;
using Services;

internal class UexItemSyncRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IExternalSyncCacheProvider<UexItemSyncRepository> cacheProvider,
    IGameEntityRepository<GameProductCategory> itemCategoryRepository,
    IUexItemsApi itemsApi,
    UexServiceStateProvider stateProvider,
    UexApiDtoMapper mapper,
    ILogger<UexItemSyncRepository> logger
) : UexGameEntitySyncRepositoryBase<ItemDTO, GameItem>(stateProvider, cacheProvider, mapper, logger)
{
    private const int BatchSize = 4;

    protected override IDependable GetDependencies()
        => dependencyResolver.DependsOn<GameProductCategory>(this)
            .AlsoDependsOn<GameCompany>();

    protected override async Task<UexApiResponse<ICollection<ItemDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var categories = itemCategoryRepository.GetAllAsync(cancellationToken)
            .Where(x => x.CategoryType == GameItemCategoryType.Item)
            .Where(category => category.Id.Identity > 0);

        // this must be a thread-safe collection due to the batching that follows
        var items = new ConcurrentBag<ItemDTO>();
        var responseDetectedAsNull = false;
        UexApiResponse<GetItemsOkResponse>? response = null;

        await foreach (var categoryBatch in categories.Batch(BatchSize).WithCancellation(cancellationToken))
        {
            await Task.WhenAll(categoryBatch.Select(LoadForCategoryAsync));
        }

        if (items.IsEmpty && responseDetectedAsNull)
        {
            // temporary workaround, some responses return Result.Data=null
            ThrowCouldNotParseResponse();
        }

        return CreateResponse(response, items.ToArray());

        async Task LoadForCategoryAsync(GameProductCategory category)
        {
            var categoryEntityId = category.Id;
            var categoryId = categoryEntityId.Identity.ToString(CultureInfo.InvariantCulture);
            response = await itemsApi.GetItemsByCategoryAsync(categoryId, cancellationToken).ConfigureAwait(false);
            responseDetectedAsNull |= response.Result.Data is null;
            foreach (var dto in response.Result.Data ?? [])
            {
                items.Add(dto);
            }
        }
    }

    protected override UexApiGameEntityId? GetSourceApiId(ItemDTO source)
        => source.Id is not null
            ? Mapper.CreateGameEntityId(source, x => x.Id)
            : null;
}
