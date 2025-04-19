namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Game;

internal class GameEntityRepositoryMock<T>(IGameEntityExternalSyncRepository<T> repository) : IGameEntityRepository<T> where T : class, IGameEntity
{
    private readonly TaskCompletionSource _completionSource = new();

    internal List<T> Entities { get; set; } = [];

    public IAsyncEnumerable<T> GetAllAsync(CancellationToken cancellationToken = default)
        => repository.GetAllAsync(cancellationToken);

    public Task<T?> GetAsync(IGameEntityId id, CancellationToken cancellationToken = default)
        => repository.GetAsync(id, cancellationToken);

    public ValueTask<AppDataState> GetDataStateAsync(GameDataState gameDataState, CancellationToken cancellationToken = default)
        => Entities.Count == 0
            ? ValueTask.FromResult(AppDataMissing.Instance)
            : ValueTask.FromResult<AppDataState>(new AppDataUpToDate(gameDataState));

    public async Task UpdateAllAsync(IAsyncEnumerable<T> entities, GameDataState gameDataState, CancellationToken cancellationToken = default)
    {
        Entities = await entities.ToListAsync(cancellationToken);
        _completionSource.TrySetResult();
    }

    public Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => _completionSource.Task.WaitAsync(cancellationToken);
}
