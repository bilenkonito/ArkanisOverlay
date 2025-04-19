namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models;
using Models.Game;

public interface IGameEntityRepository<T> : IGameEntitySimpleReadOnlyRepository<T>, IDependable
    where T : class, IGameEntity
{
    ValueTask<AppDataState> GetDataStateAsync(GameDataState gameDataState, CancellationToken cancellationToken = default);

    Task UpdateAllAsync(IAsyncEnumerable<T> entities, GameDataState gameDataState, CancellationToken cancellationToken = default);
}
