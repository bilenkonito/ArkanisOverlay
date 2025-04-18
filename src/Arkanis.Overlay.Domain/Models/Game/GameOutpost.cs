namespace Arkanis.Overlay.Domain.Models.Game;

public sealed class GameOutpost(string fullName, string shortName, GameLocationEntity location)
    : GameLocationEntity(StringGameEntityId.Create(shortName), location)
{
    protected override string SearchName { get; } = fullName;

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithShortVariant(fullName, shortName));
}
