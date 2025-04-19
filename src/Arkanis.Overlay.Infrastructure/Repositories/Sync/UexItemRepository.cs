namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using System.Globalization;
using Data.Mappers;
using Domain.Abstractions;
using Domain.Abstractions.Services;
using Domain.Enums;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Services;

internal class UexItemRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IGameEntityRepository<GameProductCategory> itemCategoryRepository,
    IUexItemsApi itemsApi,
    IUexStaticApi staticApi,
    UexApiDtoMapper mapper
) : UexGameEntityRepositoryBase<ItemDTO, GameItem>(staticApi, mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver.DependsOn<GameProductCategory>();

    protected override async Task<ICollection<ItemDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var categories = await itemCategoryRepository.GetAllAsync(cancellationToken).ToListAsync(cancellationToken).ConfigureAwait(false);
        var items = new List<ItemDTO>();

        foreach (var category in categories.Where(x => x.CategoryType == GameItemCategoryType.Item))
        {
            var categoryEntityId = category.Id as UexApiGameEntityId;
            var categoryId = (categoryEntityId?.Identity ?? 0).ToString(CultureInfo.InvariantCulture);
            var response = await itemsApi.GetItemsByCategoryAsync(categoryId, cancellationToken).ConfigureAwait(false);
            items.AddRange(response.Result.Data ?? ThrowCouldNotParseResponse());
        }

        return items;
    }

    protected override double? GetSourceApiId(ItemDTO source)
        => source.Id;
}
