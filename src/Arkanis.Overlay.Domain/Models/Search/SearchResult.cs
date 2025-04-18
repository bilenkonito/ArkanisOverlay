namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions;
using Enums;

public class SearchResult : ISearchable
{
    public required Dictionary<PriceType, decimal> AveragePrices { get; set; }
    public GameEntityCategory EntityCategory { get; set; }
    public required string SearchName { get; set; }
}
