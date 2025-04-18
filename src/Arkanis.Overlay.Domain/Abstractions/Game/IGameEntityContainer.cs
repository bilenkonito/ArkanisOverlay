namespace Arkanis.Overlay.Domain.Abstractions.Game;

public interface IGameEntityContainer : IGameEntity
{
    IEnumerable<IGameEntity> Parts { get; }
}

public interface IGameEntityContainer<out T> : IGameEntityContainer
    where T : IGameEntity
{
    new IEnumerable<T> Parts { get; }

    IEnumerable<IGameEntity> IGameEntityContainer.Parts
        => Parts.OfType<IGameEntity>();
}
