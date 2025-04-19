namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;

public sealed class GameTerminal(
    int id,
    string fullName,
    string shortName,
    string codeName,
    GameLocationEntity location
) : GameLocationEntity(UexApiGameEntityId.Create(id), location)
{
    protected override string SearchName { get; } = fullName;

    public override GameEntityName Name { get; } = new(
        GameEntityName.ReferenceTo(location),
        new GameEntityName.NameWithCodeAndShortVariant(fullName, codeName, shortName)
    );

    public required GameTerminalType Type { get; init; }
}
