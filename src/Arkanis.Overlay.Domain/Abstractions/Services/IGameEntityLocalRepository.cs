namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;

public interface IGameEntityLocalRepository<T> : IGameEntityReadOnlyRepository<T>, IDependable
    where T : class, IGameEntity
{
    Task UpdateAllAsync(IAsyncEnumerable<T> entities, CancellationToken cancellationToken = default);
}
