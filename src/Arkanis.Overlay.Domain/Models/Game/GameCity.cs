namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameCity(string fullName, string codeName, GameLocationEntity location)
    : GameLocationEntity(StringGameEntityId.Create(codeName), location)
{
    protected override string SearchName { get; } = $"{codeName} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, codeName));
}
