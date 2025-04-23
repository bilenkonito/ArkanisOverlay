namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models;
using Models.Game;

public interface IGameEntityRepository : IGameEntityReadOnlyRepository<IGameEntity>, IDependable;

public interface IGameEntityRepository<T> : IGameEntityReadOnlyRepository<T>, IDependable
    where T : class, IGameEntity
{
    ValueTask<AppDataState> GetDataStateAsync(GameDataState gameDataState, CancellationToken cancellationToken = default);

    Task UpdateAllAsync(GameEntitySyncData<T> syncData, CancellationToken cancellationToken = default);
}
