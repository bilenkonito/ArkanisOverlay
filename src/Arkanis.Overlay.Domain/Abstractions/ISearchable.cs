namespace Arkanis.Overlay.Domain.Abstractions;

using Enums;
using Models;

public interface ISearchable
{
    string Name { get; }
    EntityType EntityType { get; }
    Dictionary<PriceType, decimal> AveragePrices { get; }
    IEnumerable<LocationPrice> LocationPrices { get; }
}
