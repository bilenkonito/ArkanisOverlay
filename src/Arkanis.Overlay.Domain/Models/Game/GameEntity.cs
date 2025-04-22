namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Search;

public abstract class GameEntity(UexApiGameEntityId id, GameEntityCategory entityCategory) : IGameEntity
{
    public GameEntityCategory EntityCategory { get; } = entityCategory;

    public virtual IEnumerable<SearchableTrait> SearchableAttributes { get; } = [new SearchableEntityCategory(entityCategory)];

    public abstract GameEntityName Name { get; }

    public UexApiGameEntityId Id { get; } = id;
}
