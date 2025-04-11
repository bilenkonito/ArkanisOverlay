using ArkanisOverlay.Data.Enums;
using ArkanisOverlay.Data.Interfaces;
using ArkanisOverlay.Data.Records;

namespace ArkanisOverlay.Data.Entities.Overlay;

public class SearchResult : ISearchable
{
    public EntityType EntityType { get; set; }
    public required string Name { get; set; }
    public required Dictionary<PriceType, decimal> AveragePrices { get; set; }
    public IEnumerable<LocationPrice> LocationPrices { get; set; }
}