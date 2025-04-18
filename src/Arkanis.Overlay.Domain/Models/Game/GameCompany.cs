namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;

public sealed class GameCompany(string fullName, string shortName) : GameEntity(GameEntityCategory.Company)
{
    protected override string SearchName { get; } = fullName;

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithShortVariant(fullName, shortName));
}
