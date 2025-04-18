namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Enums;
using Models;
using Models.Search;

public interface ISearchService
{
    Task<Tuple<IEnumerable<SearchResult>, long>> SearchAsync(string query, bool includeDetailedPrices = false);

    Task<IEnumerable<LocationPrice>> GetDetailedPricesAsync(
        GameEntityCategory entityCategory,
        string name,
        PriceType? priceType = null,
        string? location = null
    );
}
