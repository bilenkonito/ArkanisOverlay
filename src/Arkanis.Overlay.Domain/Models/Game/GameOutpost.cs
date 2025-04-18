namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameOutpost : GameLocationEntity<GameLocationEntity>
{
    public GameOutpost(string fullName, string shortName, GameMoon moon) : this(fullName, shortName, (GameLocationEntity)moon)
    {
    }

    public GameOutpost(string fullName, string shortName, GamePlanet planet) : this(fullName, shortName, (GameLocationEntity)planet)
    {
    }

    private GameOutpost(string fullName, string shortName, GameLocationEntity location) : base(location)
    {
        SearchName = fullName;
        Name = new GameEntityName(new GameEntityName.NameWithShortVariant(fullName, shortName));
    }

    protected override string SearchName { get; }

    public override GameEntityName Name { get; }
}
