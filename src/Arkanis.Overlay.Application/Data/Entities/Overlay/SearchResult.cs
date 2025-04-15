using Arkanis.Overlay.Application.Data.Enums;
using Arkanis.Overlay.Application.Data.Interfaces;
using Arkanis.Overlay.Application.Data.Records;

namespace Arkanis.Overlay.Application.Data.Entities.Overlay;

public class SearchResult : ISearchable
{
    public EntityType EntityType { get; set; }
    public required string Name { get; set; }
    public required Dictionary<PriceType, decimal> AveragePrices { get; set; }
    public IEnumerable<LocationPrice> LocationPrices { get; set; }
}