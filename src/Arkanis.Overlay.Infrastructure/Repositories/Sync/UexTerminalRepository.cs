namespace Arkanis.Overlay.Infrastructure.Repositories.Sync;

using Data.Mappers;
using Domain.Abstractions;
using Domain.Models.Game;
using External.UEX.Abstractions;
using Local;

internal class UexTerminalRepository(GameEntityLocalRepositoryDependencyResolver dependencyResolver, IUexGameApi gameApi, UexApiDtoMapper mapper)
    : UexGameEntityRepositoryBase<UniverseTerminalDTO, GameTerminal>(mapper)
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
