namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models.Game;

public interface IGameEntityExternalSyncRepository<T> where T : class, IGameEntity
{
    ValueTask<GameDataState> LoadCurrentDataState(CancellationToken cancellationToken = default);

    ValueTask<GameEntitySyncData<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<T?> GetAsync(IGameEntityId id, CancellationToken cancellationToken = default);
}
