namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;

public sealed class GameProductCategory(int id, string fullName, string section)
    : GameEntity(UexApiGameEntityId.Create(id), GameEntityCategory.ItemCategory)
{
    protected override string SearchName { get; } = $"{section} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.Name($"{section} / {fullName}"));

    public required GameItemCategoryType CategoryType { get; set; }
}
