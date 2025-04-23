namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models.Game;

/// <summary>
///     Represents an external source of game entity data.
///     These services will be used to populate the fully-fledged repository (potentially allowing improved filtering
///     capabilities).
/// </summary>
/// <typeparam name="T">The entity model provided</typeparam>
public interface IGameEntityExternalSyncRepository<T> where T : class, IGameEntity
{
    ValueTask<GameDataState> LoadCurrentDataState(CancellationToken cancellationToken = default);

    ValueTask<GameEntitySyncData<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetAsync(IDomainId id, CancellationToken cancellationToken = default);
}
