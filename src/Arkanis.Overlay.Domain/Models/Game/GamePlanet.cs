namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GamePlanet(string fullName, string codeName, GameStarSystem starSystem) : GameLocationEntity<GameStarSystem>(starSystem)
{
    protected override string SearchName { get; } = $"{codeName} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, codeName));
}
