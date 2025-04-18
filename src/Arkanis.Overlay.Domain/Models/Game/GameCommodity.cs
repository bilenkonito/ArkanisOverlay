namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;
using Enums;
using Trade;

public class GameCommodity(string fullName, string code) : GameEntity(StringGameEntityId.Create(code), GameEntityCategory.Commodity), IGameTradable
{
    protected override string SearchName { get; } = $"{code} {fullName}";

    public override GameEntityName Name { get; } = new(new GameEntityName.NameWithCode(fullName, code));

    public required Bounds<PriceTag> LatestBuyPrices { get; set; }

    public required Bounds<PriceTag> LatestSellPrices { get; set; }

    public GameTerminalType TerminalType
        => GameTerminalType.Commodity;
}
