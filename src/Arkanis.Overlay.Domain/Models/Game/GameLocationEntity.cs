namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;

public abstract class GameLocationEntity(GameLocationEntity? parent) : GameEntity(GameEntityCategory.Location), IGameLocation
{
    public GameLocationEntity? Parent { get; } = parent;

    IGameLocation? IGameLocation.ParentLocation
        => Parent;
}

public abstract class GameLocationEntity<T>(T? parent) : GameLocationEntity(parent)
    where T : GameLocationEntity
{
    public new T? Parent { get; } = parent;
}
