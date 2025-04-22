namespace Arkanis.Overlay.Domain.Abstractions.Game;

using Models.Game;

public interface IGameLocation : IGameEntity, ISearchableRecursively
{
    HashSet<UexApiGameEntityId> ParentIds { get; }

    IGameLocation? ParentLocation { get; }

    ISearchableRecursively? ISearchableRecursively.Parent
        => ParentLocation;

    bool IsOrContains(IGameLocation location)
        => this == location || Contains(location);

    bool Contains(IGameLocation location)
        => location.ParentIds.Contains(Id);
}
