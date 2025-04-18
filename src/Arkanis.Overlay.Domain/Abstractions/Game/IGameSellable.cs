namespace Arkanis.Overlay.Domain.Abstractions.Game;

using Enums;
using Models;
using Models.Trade;

public interface IGameSellable
{
    Bounds<PriceTag> LatestSellPrices { get; }
    GameTerminalType TerminalType { get; }
}
