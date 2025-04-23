namespace Arkanis.Overlay.Domain.Abstractions.Game;

using Models.Game;

public interface IGameEntity : ISearchable
{
    IGameEntityId Id { get; }

    GameEntityName Name { get; }
}
