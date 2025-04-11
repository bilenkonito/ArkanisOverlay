using ArkanisOverlay.Data.Entities.Overlay;
using ArkanisOverlay.Data.Enums;
using ArkanisOverlay.Data.Records;

namespace ArkanisOverlay.Data.Interfaces;

public interface ISearchable
{
    string Name { get; }
    EntityType EntityType { get; }
    Dictionary<PriceType, decimal> AveragePrices { get; }
    IEnumerable<LocationPrice> LocationPrices { get; }
}