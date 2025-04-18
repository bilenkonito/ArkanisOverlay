namespace Arkanis.Overlay.Domain.Abstractions.Game;

public interface IGameLocation : IGameEntity, ISearchableRecursively
{
    IGameLocation? ParentLocation { get; }

    ISearchableRecursively? ISearchableRecursively.Parent
        => ParentLocation;
}
