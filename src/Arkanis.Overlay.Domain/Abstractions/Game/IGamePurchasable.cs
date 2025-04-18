namespace Arkanis.Overlay.Domain.Abstractions.Game;

using Enums;
using Models;
using Models.Trade;

public interface IGamePurchasable
{
    Bounds<PriceTag> LatestBuyPrices { get; }
    GameTerminalType TerminalType { get; }
}
