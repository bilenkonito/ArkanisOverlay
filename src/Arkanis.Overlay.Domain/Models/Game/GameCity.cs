namespace Arkanis.Overlay.Domain.Models.Game;

using Search;

public sealed class GameCity(int id, string fullName, string codeName, GameLocationEntity location)
    : GameLocationEntity(UexApiGameEntityId.Create<GameCity>(id), location)
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

    public override GameEntityName Name { get; } = new(
        GameEntityName.ReferenceTo(location),
        new GameEntityName.NameWithCode(fullName, codeName)
    );
}
