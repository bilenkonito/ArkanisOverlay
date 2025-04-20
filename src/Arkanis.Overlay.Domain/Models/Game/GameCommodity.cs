namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Search;
using Trade;

public class GameCommodity(int id, string fullName, string codeName)
    : GameEntity(UexApiGameEntityId.Create(id), GameEntityCategory.Commodity), IGameTradable
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

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, codeName));

    public required Bounds<PriceTag> LatestBuyPrices { get; set; }

    public required Bounds<PriceTag> LatestSellPrices { get; set; }

    public GameTerminalType TerminalType
        => GameTerminalType.Commodity;
}
