namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameSpaceStation(int id, string fullName, string shortName, GameLocationEntity location)
    : GameLocationEntity(UexApiGameEntityId.Create(id), location)
{
    protected override string SearchName { get; } = fullName;

    public override GameEntityName Name { get; } = new(
        GameEntityName.ReferenceTo(location),
        new GameEntityName.NameWithShortVariant(fullName, shortName)
    );
}
