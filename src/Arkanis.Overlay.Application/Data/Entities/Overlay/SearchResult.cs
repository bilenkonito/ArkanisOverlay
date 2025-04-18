namespace Arkanis.Overlay.Application.Data.Entities.Overlay;

using Enums;
using Interfaces;
using Records;

public class SearchResult : ISearchable
{
    public EntityType EntityType { get; set; }
    public required string Name { get; set; }
    public required Dictionary<PriceType, decimal> AveragePrices { get; set; }
    public IEnumerable<LocationPrice> LocationPrices { get; set; }
}
