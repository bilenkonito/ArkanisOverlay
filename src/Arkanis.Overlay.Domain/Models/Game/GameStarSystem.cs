namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameStarSystem(int id, string fullName, string codeName) : GameLocationEntity(UexApiGameEntityId.Create(id), null)
{
    protected override string SearchName { get; } = $"{codeName} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, codeName));
}
