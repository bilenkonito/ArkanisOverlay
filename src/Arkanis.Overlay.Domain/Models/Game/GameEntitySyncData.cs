namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;

public sealed record GameEntitySyncData<T>(IAsyncEnumerable<T> GameEntities, GameDataState DataState, DateTimeOffset CacheUntil)
    where T : class, IGameEntity;
