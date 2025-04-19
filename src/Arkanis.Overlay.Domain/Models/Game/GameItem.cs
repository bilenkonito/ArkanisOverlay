namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Trade;

public class GameItem(int id, string fullName, GameCompany manufacturer, GameProductCategory category)
    : GameEntity(UexApiGameEntityId.Create(id), GameEntityCategory.Item), IGameManufactured, IGamePurchasable
{
    protected override string SearchName { get; } = fullName;

    public override GameEntityName Name { get; } = new(
        GameEntityName.ReferenceTo(category),
        GameEntityName.ReferenceTo(manufacturer),
        new GameEntityName.Name(fullName)
    );

    public GameCompany Manufacturer
        => manufacturer;

    public required Bounds<PriceTag> LatestBuyPrices { get; set; }

    public GameTerminalType TerminalType
        => GameTerminalType.Item;
}
