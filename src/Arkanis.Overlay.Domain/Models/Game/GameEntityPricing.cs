namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using NodaMoney;

public abstract class GameEntityPricing(UexApiGameEntityId id, UexApiGameEntityId ownerId, GameTerminal terminal)
    : GameEntity(id, GameEntityCategory.Price), IGameLocatedAt
{
    public UexApiGameEntityId OwnerId { get; } = ownerId;

    public GameTerminal Terminal
        => terminal;

    public required DateTimeOffset UpdatedAt { get; init; }

    IGameLocation IGameLocatedAt.Location
        => terminal;
}

public abstract class GameEntityPricing<T>(UexApiGameEntityId id, UexId<T> ownerId, GameTerminal terminal)
    : GameEntityPricing(id, ownerId, terminal) where T : IGameEntity;

public sealed class GameCommodityPricing(
    int id,
    int ownerId,
    Money purchasePrice,
    Money salePrice,
    GameTerminal terminal
) : GameEntityPricing<GameCommodity>(
    UexApiGameEntityId.Create<GameCommodityPricing>(id),
    UexApiGameEntityId.Create<GameCommodity>(ownerId),
    terminal
)
{
    public override GameEntityName Name { get; } = new(new GameEntityName.Name("Commodity Price"));

    public Money PurchasePrice { get; } = purchasePrice;
    public Money SalePrice { get; } = salePrice;
}

public sealed class GameItemPurchasePricing(
    int id,
    int ownerId,
    Money purchasePrice,
    Money salePrice,
    GameTerminal terminal
) : GameEntityPricing<GameItem>(
    UexApiGameEntityId.Create<GameItemPurchasePricing>(id),
    UexApiGameEntityId.Create<GameItem>(ownerId),
    terminal
)
{
    public override GameEntityName Name { get; } = new(new GameEntityName.Name("Item Price"));

    public Money PurchasePrice { get; } = purchasePrice;
    public Money SalePrice { get; } = salePrice;
}

public sealed class GameVehiclePurchasePricing(int id, int ownerId, Money price, GameTerminal terminal)
    : GameEntityPricing<GameVehicle>(
        UexApiGameEntityId.Create<GameCommodityPricing>(id),
        UexApiGameEntityId.Create<GameVehicle>(ownerId),
        terminal
    )
{
    public override GameEntityName Name { get; } = new(new GameEntityName.Name("Vehicle Purchase Price"));

    public Money Price { get; } = price;
}

public sealed class GameVehicleRentalPricing(int id, int ownerId, Money price, GameTerminal terminal)
    : GameEntityPricing<GameVehicle>(
        UexApiGameEntityId.Create<GameCommodityPricing>(id),
        UexApiGameEntityId.Create<GameVehicle>(ownerId),
        terminal
    )
{
    public override GameEntityName Name { get; } = new(new GameEntityName.Name("Vehicle Rent Price"));

    public Money Price { get; } = price;
}
