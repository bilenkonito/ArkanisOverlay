namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;
using Services;

internal class UexSpaceStationRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IUexGameApi gameApi,
    UexGameDataStateProvider stateProvider,
    UexApiDtoMapper mapper
) : UexGameEntityRepositoryBase<UniverseSpaceStationDTO, GameSpaceStation>(stateProvider, mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver
            .DependsOn<GameCity>()
            .AlsoDependencyOn<GamePlanet>()
            .AlsoDependencyOn<GameMoon>();

    protected override async Task<UexApiResponse<ICollection<UniverseSpaceStationDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetSpaceStationsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return CreateResponse(response, response.Result.Data?.Where(x => x.Is_available > 0).ToList());
    }

    protected override double? GetSourceApiId(UniverseSpaceStationDTO source)
        => source.Id;
}
