namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;
using Search;

public class GameItemTrait(int id, int itemId, string fullName)
    : GameEntity(UexApiGameEntityId.Create<GameItemTrait>(id), GameEntityCategory.ItemTrait)
{
    public UexId<GameItem> ItemId { get; } = UexApiGameEntityId.Create<GameItem>(itemId);

    public required string Content { get; init; }

    public GameItemTraitType TraitType { get; init; } = fullName switch
    {
        "Grade" => GameItemTraitType.Grade,
        "Class" or "Armor Class" => GameItemTraitType.Class,
        _ => GameItemTraitType.Unspecified,
    };

    public override GameEntityName Name
        => new(new GameEntityName.Name(fullName));

    protected override IEnumerable<SearchableTrait> CollectSearchableTraits()
    {
        yield return new SearchableName($"{Name} {Content}");
        foreach (var searchableAttribute in base.CollectSearchableTraits())
        {
            yield return searchableAttribute;
        }
    }

    public GameEntityName.PropertyItem ToNamePart()
        => new(fullName, Content);
}
