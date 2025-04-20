namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;

internal class UexItemCategoryRepository(IUexGameApi gameApi, UexGameDataStateProvider stateProvider, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<CategoryDTO, GameProductCategory>(stateProvider, mapper)
{
    protected override async Task<UexApiResponse<ICollection<CategoryDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetCategoriesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return CreateResponse(response, response.Result.Data);
    }

    protected override double? GetSourceApiId(CategoryDTO source)
        => source.Id;
}
