namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Services;

internal class UexTerminalRepository(
    GameEntityRepositoryDependencyResolver dependencyResolver,
    IUexGameApi gameApi,
    IUexStaticApi staticApi,
    UexApiDtoMapper mapper
) : UexGameEntityRepositoryBase<UniverseTerminalDTO, GameTerminal>(staticApi, mapper)
{
    protected override IDependable GetDependencies()
        => dependencyResolver
            .DependsOn<GameCity>()
            .AlsoDependencyOn<GameOutpost>()
            .AlsoDependencyOn<GameSpaceStation>();

    protected override async Task<ICollection<UniverseTerminalDTO>> GetAllInternalAsync(CancellationToken cancellationToken)
    {
        var response = await gameApi.GetTerminalsAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        return response.Result.Data?.Where(x => x.Is_available > 0).ToList() ?? ThrowCouldNotParseResponse();
    }

    protected override double? GetSourceApiId(UniverseTerminalDTO source)
        => source.Id;
}
