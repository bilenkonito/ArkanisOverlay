namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameMoon(string fullName, string codeName, GamePlanet planet) : GameLocationEntity<GamePlanet>(StringGameEntityId.Create(codeName), planet)
{
    protected override string SearchName { get; } = $"{codeName} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, codeName));
}
