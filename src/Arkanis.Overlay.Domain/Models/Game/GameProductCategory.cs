namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;
using Search;

public sealed class GameProductCategory(int id, string fullName, string section)
    : GameEntity(UexApiGameEntityId.Create<GameProductCategory>(id), GameEntityCategory.ItemCategory)
{
    public override IEnumerable<SearchableTrait> SearchableAttributes
    {
        get
        {
            yield return new SearchableName(fullName);
            foreach (var searchableAttribute in base.SearchableAttributes)
            {
                yield return searchableAttribute;
            }
        }
    }

    public override GameEntityName Name { get; } = new(new GameEntityName.Name($"{section} / {fullName}"));

    public required GameItemCategoryType CategoryType { get; set; }
}
