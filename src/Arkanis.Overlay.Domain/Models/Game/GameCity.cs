namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameCity : GameLocationEntity<GameLocationEntity>
{
    public GameCity(string fullName, string codeName, GameMoon moon) : this(fullName, codeName, (GameLocationEntity)moon)
    {
    }

    public GameCity(string fullName, string codeName, GamePlanet planet) : this(fullName, codeName, (GameLocationEntity)planet)
    {
    }

    private GameCity(string fullName, string codeName, GameLocationEntity location) : base(location)
    {
        SearchName = $"{codeName} {fullName}";
        Name = new GameEntityName(new GameEntityName.NameWithCode(fullName, codeName));
    }

    protected override string SearchName { get; }

    public override GameEntityName Name { get; }
}
