namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;

internal class UexSpaceStationRepository(GameEntityLocalRepositoryDependencyResolver dependencyResolver, IUexGameApi gameApi, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<UniverseSpaceStationDTO, GameSpaceStation>(mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver
            .DependsOn<GameCity>()
            .AlsoDependencyOn<GamePlanet>()
            .AlsoDependencyOn<GameMoon>();

    protected override async Task<ICollection<UniverseSpaceStationDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetSpaceStationsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Result.Data?.Where(x => x.Is_available > 0).ToList() ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(UniverseSpaceStationDTO source)
        => source.Id;
}
