namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions;
using Abstractions.Game;
using Enums;

public abstract class GameEntity(GameEntityCategory entityCategory) : IGameEntity, ISearchable
{
    protected abstract string SearchName { get; }
    public abstract GameEntityName Name { get; }

    string ISearchable.SearchName
        => SearchName;

    public GameEntityCategory EntityCategory { get; } = entityCategory;
}
