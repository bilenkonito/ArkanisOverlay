namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;
using Search;

public sealed class GameCompany(int id, string fullName, string shortName)
    : GameEntity(UexApiGameEntityId.Create<GameCompany>(id), GameEntityCategory.Company)
{
    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithShortVariant(fullName, shortName));

    protected override IEnumerable<SearchableTrait> CollectSearchableTraits()
    {
        yield return new SearchableName(fullName);
        foreach (var searchableAttribute in base.CollectSearchableTraits())
        {
            yield return searchableAttribute;
        }
    }
}
