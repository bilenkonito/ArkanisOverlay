namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;

internal class GameEntityLocalRepositoryMock<T>(IGameEntityExternalSyncRepository<T> repository) : IGameEntityLocalRepository<T> where T : class, IGameEntity
{
    private readonly TaskCompletionSource _completionSource = new();

    internal List<T> Entities { get; set; } = [];

    public IAsyncEnumerable<T> GetAllAsync(CancellationToken cancellationToken = default)
        => repository.GetAllAsync(cancellationToken);

    public Task<T?> GetAsync(IGameEntityId id, CancellationToken cancellationToken = default)
        => repository.GetAsync(id, cancellationToken);

    public async Task UpdateAllAsync(IAsyncEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        Entities = await entities.ToListAsync(cancellationToken);
        _completionSource.TrySetResult();
    }

    public Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => _completionSource.Task.WaitAsync(cancellationToken);
}
