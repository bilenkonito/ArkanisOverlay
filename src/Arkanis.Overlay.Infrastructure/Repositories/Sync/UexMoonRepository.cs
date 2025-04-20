namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;
using Services;

internal class UexMoonRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IUexGameApi gameApi,
    UexGameDataStateProvider stateProvider,
    UexApiDtoMapper mapper
) : UexGameEntityRepositoryBase<UniverseMoonDTO, GameMoon>(stateProvider, mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver
            .DependsOn<GamePlanet>()
            .AlsoDependencyOn<GameStarSystem>();

    protected override async Task<UexApiResponse<ICollection<UniverseMoonDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetMoonsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return CreateResponse(response, response.Result.Data?.Where(x => x.Is_available > 0).ToList());
    }

    protected override double? GetSourceApiId(UniverseMoonDTO source)
        => source.Id;
}
