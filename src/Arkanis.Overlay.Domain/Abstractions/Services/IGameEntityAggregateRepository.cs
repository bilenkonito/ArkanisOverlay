namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;

public interface IGameEntityAggregateRepository : IDependable
{
    IAsyncEnumerable<IGameEntity> GetAllAsync(CancellationToken cancellationToken = default);
}
