namespace Arkanis.Overlay.Infrastructure.Services;

using System.Diagnostics;
using Domain.Abstractions.Services;
using Domain.Models.Search;
using FuzzySharp;
using Microsoft.Extensions.Logging;

public class InMemorySearchService(
    IGameEntityAggregateRepository aggregateRepository,
    ILogger<InMemorySearchService> logger
) : ISearchService
{
    public async Task<Tuple<IEnumerable<SearchResult>, long>> SearchAsync(
        string query,
        bool includeDetailedPrices = false,
        CancellationToken cancellationToken = default
    )
    {
        var stopwatch = Stopwatch.StartNew();

        query = query.ToLowerInvariant();
        logger.LogDebug("Searching all entities for matches with {SearchQuery}", query);

        var matches = await aggregateRepository.GetAllAsync(cancellationToken)
            .Select(entity => (Entity: entity, MatchRatio: Fuzz.PartialRatio(query, entity.SearchName.ToLowerInvariant())))
            .Where(match => match.MatchRatio > 75)
            .OrderBy(match => match.MatchRatio)
            .ToListAsync(cancellationToken);

        var searchElapsed = stopwatch.Elapsed;
        logger.LogDebug("Search yielded {SearchMatches} results in {SearchLengthMs}ms", matches.Count, searchElapsed.TotalMilliseconds);

        var searchMatches = matches
            .Select(match => match.Entity)
            .Select(entity => new SearchResult
                {
                    SearchName = entity.SearchName,
                    EntityCategory = entity.EntityCategory,
                    AveragePrices = [],
                }
            );

        return Tuple.Create(searchMatches, (long)searchElapsed.TotalMilliseconds);
    }
}
