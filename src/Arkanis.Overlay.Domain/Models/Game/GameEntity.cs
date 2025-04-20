namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions;
using Abstractions.Game;
using Enums;

public abstract class GameEntity(IGameEntityId id, GameEntityCategory entityCategory) : IGameEntity
{
    protected abstract string SearchName { get; }
    public abstract GameEntityName Name { get; }

    public IGameEntityId Id { get; } = id;

    string ISearchable.SearchName
        => SearchName;

    public GameEntityCategory EntityCategory { get; } = entityCategory;
}
