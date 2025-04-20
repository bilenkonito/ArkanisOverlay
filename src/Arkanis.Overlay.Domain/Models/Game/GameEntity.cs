namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Search;

public abstract class GameEntity(IGameEntityId id, GameEntityCategory entityCategory) : IGameEntity
{
    public GameEntityCategory EntityCategory { get; } = entityCategory;

    public virtual IEnumerable<SearchableAttribute> SearchableAttributes { get; } = [new SearchableEntityCategory(entityCategory)];

    public abstract GameEntityName Name { get; }

    public IGameEntityId Id { get; } = id;
}
