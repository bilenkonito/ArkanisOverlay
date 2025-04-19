namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Trade;

public class GameCommodity(int id, string fullName, string codeName)
    : GameEntity(UexApiGameEntityId.Create(id), GameEntityCategory.Commodity), IGameTradable
{
    protected override string SearchName { get; } = $"{codeName} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, codeName));

    public required Bounds<PriceTag> LatestBuyPrices { get; set; }

    public required Bounds<PriceTag> LatestSellPrices { get; set; }

    public GameTerminalType TerminalType
        => GameTerminalType.Commodity;
}
