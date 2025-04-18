namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;

public sealed class GameItemCategory(string fullName, string section) : GameEntity(StringGameEntityId.Create($"{section} / {fullName}"), GameEntityCategory.Company)
{
    protected override string SearchName { get; } = $"{section} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.Name($"{section} / {fullName}"));
}
