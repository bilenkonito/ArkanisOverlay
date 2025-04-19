namespace Arkanis.Overlay.Infrastructure.Repositories.Local;

using Domain.Models;
using Domain.Models.Game;
using External.UEX.Abstractions;

internal sealed class UexGameDataStateProvider(IUexStaticApi staticApi)
{
    public async Task<GameDataState> LoadCurrentDataState(CancellationToken cancellationToken)
    {
        var parameters = await staticApi.GetDataParametersAsync(cancellationToken);
        var gameVersion = StarCitizenVersion.Create(parameters.Result.Data?.Global?.Game_version ?? "unknown");
        return new GameDataState(gameVersion, DateTimeOffset.Now);
    }
}
