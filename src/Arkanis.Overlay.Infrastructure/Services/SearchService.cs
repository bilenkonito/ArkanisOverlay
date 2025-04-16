namespace Arkanis.Overlay.Infrastructure.Services;

using System.Diagnostics;
using System.Text.RegularExpressions;
using Data;
using Domain.Abstractions.Services;
using Domain.Enums;
using Domain.Models;
using Domain.Models.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

public partial class SearchService(IMemoryCache cache, UEXContext dbContext, ILogger<SearchService> logger) : ISearchService
{
    private const int CACHE_DURATION_MINUTES = 15;

    public async Task<Tuple<IEnumerable<SearchResult>, long>> SearchAsync(
        string searchText,
        bool includeDetailedPrices = false
    )
    {
        var stopwatch = Stopwatch.StartNew();
        var (query, options) = ProcessSearchTokens(searchText);

        var cacheKey = $"search_{searchText.ToLower()}_{includeDetailedPrices}";

        var result = await cache.GetOrCreateAsync
            (
                cacheKey,
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CACHE_DURATION_MINUTES);

                    var results = new List<SearchResult>();

                    // Create list of search tasks based on options
                    var tasks = new List<Task<IEnumerable<SearchResult>>>();

                    if (options.Items)
                    {
                        tasks.Add(SearchItemsAsync(query));
                    }

                    if (options.Ships || options.Vehicles)
                    {
                        tasks.Add(SearchVehiclesAsync(query, options.Ships, options.Vehicles));
                    }

                    if (options.Commodities)
                    {
                        tasks.Add(SearchCommoditiesAsync(query));
                    }

                    if (tasks.Count <= 0)
                    {
                        return results;
                    }

                    await Task.WhenAll(tasks).ConfigureAwait(false);
                    results.AddRange(tasks.SelectMany(t => t.Result));

                    return results.OrderBy(r => r.EntityType).ThenBy(r => r.Name).ToList();
                }
            )
            .ConfigureAwait(false);

        stopwatch.Stop();
        logger.LogDebug
        (
            "Search for '{Query}' completed in {Duration}ms with {ResultCount} results",
            searchText,
            stopwatch.ElapsedMilliseconds,
            result?.Count() ?? 0
        );

        return new Tuple<IEnumerable<SearchResult>, long>
        (
            result
            ??
            [
            ],
            stopwatch.ElapsedMilliseconds
        );
    }

    public Task<IEnumerable<LocationPrice>> GetDetailedPricesAsync(
        EntityType entityType,
        string name,
        PriceType? priceType = null,
        string? location = null
    )
        => throw new NotImplementedException();

    private Tuple<string, SearchOptions> ProcessSearchTokens(string searchText)
    {
        var textTokens = new List<string>();
        var searchOptions = new SearchOptions();
        var hasFilterTokens = false;

        foreach (var token in searchText.Split(" "))
        {
            if (!MyRegex().IsMatch(token))
            {
                textTokens.Add(token);
                continue;
            }

            hasFilterTokens = true;
            switch (token[1])
            {
                case 'c':
                    searchOptions.Commodities = true;
                    break;
                case 's':
                    searchOptions.Ships = true;
                    break;
                case 'v':
                    searchOptions.Vehicles = true;
                    break;
                case 'i':
                    searchOptions.Items = true;
                    break;
                case 'l':
                    searchOptions.Locations = true;
                    break;
            }
        }

        // if no tokens were specified, we include all
        if (!hasFilterTokens)
        {
            searchOptions = new SearchOptions
            {
                Commodities = true,
                Ships = true,
                Vehicles = true,
                Items = true,
                Locations = true,
            };
        }

        return new Tuple<string, SearchOptions>(string.Join(' ', textTokens), searchOptions);
    }

    private async Task<IEnumerable<SearchResult>> SearchItemsAsync(string query)
    {
        var results = await dbContext.Items
            .Where(i => EF.Functions.Like(i.Name, $"%{query}%"))
            .Select
            (
                i => new
                {
                    i.Id,
                    i.Name,
                    AverageBuyPrice = i.ItemsPricesAll.Any() ? i.ItemsPricesAll.Average(p => (double)p.PriceBuy) : 0.0,
                    AverageSellPrice = i.ItemsPricesAll.Any() ? i.ItemsPricesAll.Average(p => (double)p.PriceSell) : 0.0,
                }
            )
            .ToListAsync()
            .ConfigureAwait(false);

        return results.Select
        (
            i =>
                new SearchResult
                {
                    EntityType = EntityType.Item,
                    Name = i.Name ?? $"Unnamed Item #{i.Id}",
                    AveragePrices = new Dictionary<PriceType, decimal>
                    {
                        [PriceType.Buy] = (decimal)i.AverageBuyPrice,
                        [PriceType.Sell] = (decimal)i.AverageSellPrice,
                    },
                }
        );
    }

    // Similar implementations for SearchVehiclesAsync and SearchCommoditiesAsync

    private async Task<IEnumerable<SearchResult>> SearchVehiclesAsync(
        string query,
        bool includeShips,
        bool includeVehicles
    )
    {
        var both = !(includeShips ^ includeVehicles);

        var result = await dbContext.Vehicles
            .Where
            (
                vehicle =>
                    (vehicle.IsSpaceship == (both || includeShips)
                     || vehicle.IsGroundVehicle == (both || includeVehicles))
                    && EF.Functions.Like(vehicle.NameFull, $"%{query}%")
            )
            .Select
            (
                i => new
                {
                    i.Id,
                    i.NameFull,
                    i.IsSpaceship,
                    AverageBuyPrice =
                        i.PurchasePricesAll.Any() ? i.PurchasePricesAll.Average(p => (double)p.PriceBuy) : 0.0,
                    AverageRentPrice = i.RentalPricesAll.Any() ? i.RentalPricesAll.Average(p => (double)p.PriceRent) : 0.0,
                }
            )
            .ToListAsync()
            .ConfigureAwait(false);

        return result.Select
        (
            i =>
                new SearchResult
                {
                    EntityType = i.IsSpaceship ? EntityType.SpaceShip : EntityType.Vehicle,
                    Name = i.NameFull ?? $"Unnamed Vehicle #{i.Id}",
                    AveragePrices = new Dictionary<PriceType, decimal>
                    {
                        [PriceType.Buy] = (decimal)i.AverageBuyPrice,
                        [PriceType.Rent] = (decimal)i.AverageRentPrice,
                    },
                }
        );
    }

    private async Task<IEnumerable<SearchResult>> SearchCommoditiesAsync(string query)
    {
        var result = await dbContext.Commodities
            .Where(i => EF.Functions.Like(i.Name, $"%{query}%"))
            .Select
            (
                i => new
                {
                    i.Id,
                    i.Name,
                    AverageBuyPrice = i.CommoditiesPricesAll.Any()
                        ? i.CommoditiesPricesAll.Average(p => (double)p.PriceBuy)
                        : 0.0,
                    AverageSellPrice = i.CommoditiesPricesAll.Any()
                        ? i.CommoditiesPricesAll.Average(p => (double)p.PriceSell)
                        : 0.0,
                }
            )
            .ToListAsync()
            .ConfigureAwait(false);

        return result.Select
        (
            i =>
                new SearchResult
                {
                    EntityType = EntityType.Commodity,
                    Name = i.Name ?? $"Unnamed Commodity #{i.Id}",
                    AveragePrices = new Dictionary<PriceType, decimal>
                    {
                        [PriceType.Buy] = (decimal)i.AverageBuyPrice,
                        [PriceType.Sell] = (decimal)i.AverageSellPrice,
                    },
                }
        );
    }

    [GeneratedRegex(@":\w")]
    private static partial Regex MyRegex();
}
