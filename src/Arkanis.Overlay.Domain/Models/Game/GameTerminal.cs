namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;

public sealed class GameTerminal : GameLocationEntity<GameLocationEntity>
{
    public GameTerminal(string fullName, string shortName, string codeName, GameCity city)
        : this(fullName, shortName, codeName, (GameLocationEntity)city)
    {
    }

    public GameTerminal(string fullName, string shortName, string codeName, GameOutpost outpost)
        : this(fullName, shortName, codeName, (GameLocationEntity)outpost)
    {
    }

    private GameTerminal(string fullName, string shortName, string codeName, GameLocationEntity location)
        : base(StringGameEntityId.Create(codeName), location)
    {
        SearchName = fullName;
        Name = new GameEntityName(new GameEntityName.NameWithCodeAndShortVariant(fullName, codeName, shortName));
    }

    protected override string SearchName { get; }

    public override GameEntityName Name { get; }

    public required GameTerminalType Type { get; init; }
}
