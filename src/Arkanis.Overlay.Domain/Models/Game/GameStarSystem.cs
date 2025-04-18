namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameStarSystem(string fullName, string codeName) : GameLocationEntity(StringGameEntityId.Create(codeName), null)
{
    protected override string SearchName { get; } = $"{codeName} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, codeName));
}
