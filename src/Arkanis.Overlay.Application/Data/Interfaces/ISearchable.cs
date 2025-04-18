namespace Arkanis.Overlay.Application.Data.Interfaces;

using Enums;
using Records;

public interface ISearchable
{
    string Name { get; }
    EntityType EntityType { get; }
    Dictionary<PriceType, decimal> AveragePrices { get; }
    IEnumerable<LocationPrice> LocationPrices { get; }
}
