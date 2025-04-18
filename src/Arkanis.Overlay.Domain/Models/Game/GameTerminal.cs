namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;

public sealed class GameTerminal : GameLocationEntity<GameLocationEntity>
{
    public GameTerminal(
        string fullName,
        string shortName,
        string codeName,
        GameTerminalType type,
        GameCity city
    )
        : this(fullName, shortName, codeName, type, (GameLocationEntity)city)
    {
    }

    public GameTerminal(
        string fullName,
        string shortName,
        string codeName,
        GameTerminalType type,
        GameOutpost outpost
    )
        : this(fullName, shortName, codeName, type, (GameLocationEntity)outpost)
    {
    }

    private GameTerminal(
        string fullName,
        string shortName,
        string codeName,
        GameTerminalType type,
        GameLocationEntity location
    ) : base(location)
    {
        SearchName = fullName;
        Type = type;
        Name = new GameEntityName(new GameEntityName.NameWithCodeAndShortVariant(fullName, codeName, shortName));
    }

    protected override string SearchName { get; }
    public GameTerminalType Type { get; }

    public override GameEntityName Name { get; }
}
