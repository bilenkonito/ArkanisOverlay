namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameCity(int id, string fullName, string codeName, GameLocationEntity location)
    : GameLocationEntity(UexApiGameEntityId.Create(id), location)
{
    protected override string SearchName { get; } = $"{codeName} {fullName}";

    public override GameEntityName Name { get; } = new(
        GameEntityName.ReferenceTo(location),
        new GameEntityName.NameWithCode(fullName, codeName)
    );
}
