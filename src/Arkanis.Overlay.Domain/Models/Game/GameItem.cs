namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Trade;

public class GameItem(Guid uuid, string fullName, GameCompany manufacturer)
    : GameEntity(GuidGameEntityId.Create(uuid), GameEntityCategory.Commodity), IGameManufactured, IGamePurchasable
{
    public GameItem(string uuid, string fullName, GameCompany manufacturer) : this(Guid.Parse(uuid), fullName, manufacturer)
    {
    }

    protected override string SearchName { get; } = fullName;

    public override GameEntityName Name { get; } = new(
        new GameEntityName.CompanyReference(manufacturer),
        new GameEntityName.Name(fullName)
    );

    public GameCompany Manufacturer
        => manufacturer;

    public required Bounds<PriceTag> LatestBuyPrices { get; set; }

    public GameTerminalType TerminalType
        => GameTerminalType.Item;
}
