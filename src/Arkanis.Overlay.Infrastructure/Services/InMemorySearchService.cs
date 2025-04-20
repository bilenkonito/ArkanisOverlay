namespace Arkanis.Overlay.Infrastructure.Services;

using System.Diagnostics;
using Domain.Abstractions.Services;
using Domain.Models.Search;
using Microsoft.Extensions.Logging;

public class InMemorySearchService(
    IGameEntityAggregateRepository aggregateRepository,
    ILogger<InMemorySearchService> logger
) : ISearchService
{
    public async Task<GameEntitySearchResults> SearchAsync(IEnumerable<SearchQuery> queries, CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        logger.LogDebug("Searching all entities for matches with {@SearchQuery}", queries);

        var matches = await aggregateRepository.GetAllAsync(cancellationToken)
            .Select(entity => queries
                .Select(query => query.Match(entity))
                .Aggregate((result1, result2) => result1.Merge(result2))
            )
            .Where(result => result.IsExcluded == false)
            .ToListAsync(cancellationToken);

        matches.Sort((a, b) => b.CompareTo(a));

        var searchElapsed = stopwatch.Elapsed;
        logger.LogDebug("Search yielded {SearchMatches} results in {SearchLengthMs}ms", matches.Count, searchElapsed.TotalMilliseconds);

        return new GameEntitySearchResults(matches, searchElapsed);
    }
}
