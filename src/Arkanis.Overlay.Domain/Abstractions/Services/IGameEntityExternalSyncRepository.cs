namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;
using Models.Game;

public interface IGameEntityExternalSyncRepository<T> : IGameEntitySimpleReadOnlyRepository<T> where T : class, IGameEntity
{
    ValueTask<GameDataState> LoadCurrentDataState(CancellationToken cancellationToken = default);
}
