namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Game;

public interface IGameEntityExternalSyncRepository<T> : IGameEntityReadOnlyRepository<T> where T : class, IGameEntity;
