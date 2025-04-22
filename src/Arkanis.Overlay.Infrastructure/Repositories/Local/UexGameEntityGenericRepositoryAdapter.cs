namespace Arkanis.Overlay.Infrastructure.Repositories.Local;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;

public class UexGameEntityGenericRepositoryAdapter<T>(IGameEntityRepository<T> repository) : IGameEntityRepository
    where T : class, IGameEntity
{
    public Task WaitUntilReadyAsync(CancellationToken cancellationToken = default)
        => repository.WaitUntilReadyAsync(cancellationToken);

    public IAsyncEnumerable<IGameEntity> GetAllAsync(CancellationToken cancellationToken = default)
        => repository.GetAllAsync(cancellationToken);

    public async Task<IGameEntity?> GetAsync(IDomainId id, CancellationToken cancellationToken = default)
        => await repository.GetAsync(id, cancellationToken);
}
