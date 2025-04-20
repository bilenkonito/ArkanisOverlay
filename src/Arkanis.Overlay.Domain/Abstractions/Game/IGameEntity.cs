namespace Arkanis.Overlay.Domain.Abstractions.Game;

using Enums;
using Models.Game;

public interface IGameEntity : ISearchable
{
    IGameEntityId Id { get; }

    GameEntityName Name { get; }

    GameEntityCategory EntityCategory { get; }
}
