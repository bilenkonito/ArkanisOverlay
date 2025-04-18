namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;

public abstract class GameLocationEntity(IGameEntityId id, GameLocationEntity? parent) : GameEntity(id, GameEntityCategory.Location), IGameLocation
{
    public GameLocationEntity? Parent { get; } = parent;

    IGameLocation? IGameLocation.ParentLocation
        => Parent;
}
