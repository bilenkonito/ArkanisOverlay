namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;

internal class UexMoonRepository(GameEntityLocalRepositoryDependencyResolver dependencyResolver, IUexGameApi gameApi, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<UniverseMoonDTO, GameMoon>(mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver
            .DependsOn<GamePlanet>()
            .AlsoDependencyOn<GameStarSystem>();

    protected override async Task<ICollection<UniverseMoonDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetMoonsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Result.Data?.Where(x => x.Is_available > 0).ToList() ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(UniverseMoonDTO source)
        => source.Id;
}
