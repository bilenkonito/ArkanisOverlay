namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Services;

internal class UexMoonRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IUexGameApi gameApi,
    IUexStaticApi staticApi,
    UexApiDtoMapper mapper
) : UexGameEntityRepositoryBase<UniverseMoonDTO, GameMoon>(staticApi, mapper)
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
