namespace Arkanis.Overlay.Domain.Models.Game;

using Search;

public sealed class GameStarSystem(int id, string fullName, string codeName) : GameLocationEntity(UexApiGameEntityId.Create(id), null)
{
    public override IEnumerable<SearchableTrait> SearchableAttributes
    {
        get
        {
            yield return new SearchableName(fullName);
            yield return new SearchableCode(codeName);
            foreach (var searchableAttribute in base.SearchableAttributes)
            {
                yield return searchableAttribute;
            }
        }
    }

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, codeName));
}
