namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;

internal class UexItemCategoryRepository(IUexGameApi gameApi, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<CategoryDTO, GameProductCategory>(mapper)
{
    protected override async Task<ICollection<CategoryDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetCategoriesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Result.Data ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(CategoryDTO source)
        => source.Id;
}
