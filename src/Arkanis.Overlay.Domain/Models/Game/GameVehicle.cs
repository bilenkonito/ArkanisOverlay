namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Trade;

public class GameVehicle(int id, string fullName, string shortName, GameCompany manufacturer)
    : GameEntity(UexApiGameEntityId.Create(id), GameEntityCategory.Vehicle), IGameManufactured, IGamePurchasable
{
    protected override string SearchName { get; } = fullName;

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
