namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Search;
using Trade;

public abstract class GameVehicle(
    int id,
    string fullName,
    string shortName,
    GameCompany manufacturer,
    GameEntityCategory vehicleCategory
) : GameEntity(UexApiGameEntityId.Create(id), vehicleCategory), IGameManufactured, IGamePurchasable
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

public class GameSpaceShip(int id, string fullName, string shortName, GameCompany manufacturer) : GameVehicle(
    id,
    fullName,
    shortName,
    manufacturer,
    GameEntityCategory.SpaceShip
);

public class GameGroundVehicle(int id, string fullName, string shortName, GameCompany manufacturer) : GameVehicle(
    id,
    fullName,
    shortName,
    manufacturer,
    GameEntityCategory.GroundVehicle
);
