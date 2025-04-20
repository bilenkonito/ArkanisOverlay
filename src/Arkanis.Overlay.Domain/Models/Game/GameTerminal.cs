namespace Arkanis.Overlay.Domain.Models.Game;

using Enums;
using Search;

public sealed class GameTerminal(
    int id,
    string fullName,
    string shortName,
    string codeName,
    GameLocationEntity location
) : GameLocationEntity(UexApiGameEntityId.Create(id), location)
{
    public override IEnumerable<SearchableAttribute> SearchableAttributes
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
        new GameEntityName.NameWithCodeAndShortVariant(fullName, codeName, shortName)
    );

    public required GameTerminalType Type { get; init; }
}
