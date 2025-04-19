namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Services;

internal class UexPlanetRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IUexGameApi gameApi,
    IUexStaticApi staticApi,
    UexApiDtoMapper mapper
) : UexGameEntityRepositoryBase<UniversePlanetDTO, GamePlanet>(staticApi, mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver.DependsOn<GameStarSystem>();

    protected override async Task<ICollection<UniversePlanetDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetPlanetsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Result.Data?.Where(x => x.Is_available > 0).ToList() ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(UniversePlanetDTO source)
        => source.Id;
}
