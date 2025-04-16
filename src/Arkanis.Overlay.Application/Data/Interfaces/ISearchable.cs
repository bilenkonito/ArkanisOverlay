using Arkanis.Overlay.Application.Data.Enums;
using Arkanis.Overlay.Application.Data.Records;

namespace Arkanis.Overlay.Application.Data.Interfaces;

public interface ISearchable
{
    string Name { get; }
    EntityType EntityType { get; }
    Dictionary<PriceType, decimal> AveragePrices { get; }
    IEnumerable<LocationPrice> LocationPrices { get; }
}