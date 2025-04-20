namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;
using Services;

internal class UexCityRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IUexGameApi gameApi,
    UexGameDataStateProvider stateProvider,
    UexApiDtoMapper mapper
) : UexGameEntityRepositoryBase<UniverseCityDTO, GameCity>(stateProvider, mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver
            .DependsOn<GamePlanet>()
            .AlsoDependencyOn<GameMoon>();

    protected override async Task<UexApiResponse<ICollection<UniverseCityDTO>>> GetInternalResponseAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetCitiesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return CreateResponse(response, response.Result.Data?.Where(x => x.Is_available > 0).ToList());
    }

    protected override double? GetSourceApiId(UniverseCityDTO source)
        => source.Id;
}
