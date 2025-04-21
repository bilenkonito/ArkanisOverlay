namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Search;
using Trade;

public class GameVehicle(int id, string fullName, string shortName, GameCompany manufacturer)
    : GameEntity(UexApiGameEntityId.Create(id), GameEntityCategory.Vehicle), IGameManufactured, IGamePurchasable
{
    public override IEnumerable<SearchableTrait> SearchableAttributes
    {
        get
        {
            yield return new SearchableName(fullName);
            yield return new SearchableManufacturer(manufacturer);
            foreach (var searchableAttribute in base.SearchableAttributes)
            {
                yield return searchableAttribute;
            }
        }
    }

    public override GameEntityName Name { get; } = new(
        new GameEntityName.CompanyReference(manufacturer),
        new GameEntityName.NameWithShortVariant(fullName, shortName)
    );

    public GameCompany Manufacturer
        => manufacturer;

    public required Bounds<PriceTag> LatestBuyPrices { get; set; }

    public GameTerminalType TerminalType
        => GameTerminalType.Item;
}
