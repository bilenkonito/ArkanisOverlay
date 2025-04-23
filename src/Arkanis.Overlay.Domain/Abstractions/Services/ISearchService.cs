namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Models.Search;

public interface ISearchService
{
    Task<Tuple<IEnumerable<SearchResult>, long>> SearchAsync(string query, bool includeDetailedPrices = false, CancellationToken cancellationToken = default);
}
