namespace Arkanis.Overlay.Infrastructure.Repositories.Local;

using Domain.Models;
using Domain.Models.Game;
using External.UEX.Abstractions;
using External.UEX.Extensions;

internal sealed class UexGameDataStateProvider(IUexStaticApi staticApi)
{
    internal GameDataState CurrentDataState { get; set; } = MissingGameDataState.Instance;

    internal DateTimeOffset? CachedUntil { get; set; }

    public async Task<GameDataState> LoadCurrentDataState(CancellationToken cancellationToken)
    {
        if (CachedUntil > DateTimeOffset.Now)
        {
            return CurrentDataState;
        }

        var response = await staticApi.GetDataParametersAsync(cancellationToken);
        var gameVersion = StarCitizenVersion.Create(response.Result.Data?.Global?.Game_version ?? "unknown");
        var responseHeaders = response.CreateResponseHeaders();
        var requestTime = responseHeaders.GetRequestTime();

        CurrentDataState = new SyncedGameDataState(gameVersion, requestTime);
        CachedUntil = responseHeaders.GetCacheUntil();

        return CurrentDataState;
    }
}
