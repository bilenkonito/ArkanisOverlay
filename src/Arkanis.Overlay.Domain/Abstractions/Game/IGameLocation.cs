namespace Arkanis.Overlay.Domain.Abstractions.Game;

public interface IGameLocation : IGameEntity, ISearchableRecursively
{
    HashSet<IGameEntityId> ParentIds { get; }

    IGameLocation? ParentLocation { get; }

    ISearchableRecursively? ISearchableRecursively.Parent
        => ParentLocation;

    bool Contains(IGameLocation location)
        => location.ParentIds.Contains(Id) || location.ParentIds.Overlaps(ParentIds);
}
