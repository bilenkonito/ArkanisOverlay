namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions;
using Enums;

public class SearchResult : ISearchable
{
    public EntityType EntityType { get; set; }
    public required string Name { get; set; }
    public required Dictionary<PriceType, decimal> AveragePrices { get; set; }
    public IEnumerable<LocationPrice> LocationPrices { get; set; }
}
