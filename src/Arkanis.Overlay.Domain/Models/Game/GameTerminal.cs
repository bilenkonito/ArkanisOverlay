namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;

public sealed class GameTerminal(string fullName, string shortName, string codeName, GameLocationEntity location)
    : GameLocationEntity(StringGameEntityId.Create(codeName), location)
{
    protected override string SearchName { get; } = fullName;

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCodeAndShortVariant(fullName, codeName, shortName));

    public required GameTerminalType Type { get; init; }
}
