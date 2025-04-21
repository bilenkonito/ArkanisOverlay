namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Search;

public abstract class GameLocationEntity(IGameEntityId id, GameLocationEntity? parent)
    : GameEntity(id, GameEntityCategory.Location), IGameLocation
{
    public GameLocationEntity? Parent { get; } = parent;

    public HashSet<IGameEntityId> ParentIds { get; } = parent is not null
        ?
        [
            parent.Id,
            ..parent.ParentIds,
        ]
        : [];

    IGameLocation? IGameLocation.ParentLocation
        => Parent;

    public override IEnumerable<SearchableTrait> SearchableAttributes
    {
        get
        {
            if (Parent is not null)
            {
                yield return new SearchableLocation(Parent);
            }

            foreach (var searchableAttribute in base.SearchableAttributes)
            {
                yield return searchableAttribute;
            }
        }
    }

    public IEnumerable<GameLocationEntity> CreatePathToRoot()
    {
        foreach (var parent in Parent?.CreatePathToRoot() ?? [])
        {
            yield return parent;
        }

        yield return this;
    }
}
