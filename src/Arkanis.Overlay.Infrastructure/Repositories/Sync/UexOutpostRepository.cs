namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Services;

internal class UexOutpostRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IUexGameApi gameApi,
    IUexStaticApi staticApi,
    UexApiDtoMapper mapper
) : UexGameEntityRepositoryBase<UniverseOutpostDTO, GameOutpost>(staticApi, mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver
            .DependsOn<GamePlanet>()
            .AlsoDependencyOn<GameMoon>();

    protected override async Task<ICollection<UniverseOutpostDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetOutpostsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Result.Data?.Where(x => x.Is_available > 0).ToList() ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(UniverseOutpostDTO source)
        => source.Id;
}
