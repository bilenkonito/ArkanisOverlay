namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;
using Search;

public sealed class GameProductCategory(int id, string fullName, string section)
    : GameEntity(UexApiGameEntityId.Create<GameProductCategory>(id), GameEntityCategory.ProductCategory)
{
    public override GameEntityName Name { get; } = new(new GameEntityName.Name($"{section} / {fullName}"));

    public required GameItemCategoryType CategoryType { get; set; }

    protected override IEnumerable<SearchableTrait> CollectSearchableTraits()
    {
        yield return new SearchableName(fullName);
        foreach (var searchableAttribute in base.CollectSearchableTraits())
        {
            yield return searchableAttribute;
        }
    }
}
