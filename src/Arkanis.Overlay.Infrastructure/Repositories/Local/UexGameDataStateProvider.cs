namespace Arkanis.Overlay.Infrastructure.Repositories.Local;

using Domain.Models;
using Domain.Models.Game;
using External.UEX.Abstractions;
using External.UEX.Extensions;

internal sealed class UexGameDataStateProvider(IUexStaticApi staticApi)
{
    public async Task<GameDataState> LoadCurrentDataState(CancellationToken cancellationToken)
    {
        var response = await staticApi.GetDataParametersAsync(cancellationToken);
        var gameVersion = StarCitizenVersion.Create(response.Result.Data?.Global?.Game_version ?? "unknown");
        var requestTime = response.CreateResponseHeaders().GetRequestTime();
        return new SyncedGameDataState(gameVersion, requestTime);
    }
}
