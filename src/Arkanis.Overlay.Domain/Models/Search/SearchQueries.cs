namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions;
using Abstractions.Game;
using Enums;
using FuzzySharp;

/// <summary>
///     A prototype for any search query.
///     Matching queries against <see cref="ISearchable" /> subjects produces match results.
/// </summary>
public abstract record SearchQuery
{
    public virtual SearchMatchResult<T> Match<T>(T searchable) where T : ISearchable
        => SearchMatchResult.Create(searchable, Match(searchable.SearchableAttributes));

    public virtual SearchMatchResult Match(ISearchable searchable)
        => SearchMatchResult.Create(searchable, Match(searchable.SearchableAttributes));

    public virtual IEnumerable<SearchMatch> Match(IEnumerable<SearchableTrait> attributes, int depth = 0)
        => attributes.SelectMany(attribute => Match(attribute, depth));

    public abstract IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0);
}

public sealed record EmptySearch : SearchQuery
{
    private EmptySearch()
    {
    }

    public static SearchQuery Instance { get; } = new EmptySearch();

    public override IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0)
        => [];
}

public abstract record TextSearch(string Content) : SearchQuery
{
    protected string NormalizedContent { get; } = Content.ToLowerInvariant();

    public static FuzzyTextSearch Combine(IEnumerable<SearchQuery> queries)
        => new(string.Join(' ', queries.OfType<TextSearch>().Select(x => x.Content)));

    public static SearchQuery Fuzzy(string content)
        => new FuzzyTextSearch(content);
}

public sealed record FuzzyTextSearch(string Content) : TextSearch(Content)
{
    public override IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0)
        => trait switch
        {
            SearchableCode data =>
                //! Location Codes only for full match
                [
                    data.Code.Equals(NormalizedContent, StringComparison.OrdinalIgnoreCase)
                        ? new ScoredMatch(100, depth, trait, this)
                        : new NoMatch(trait, this),
                ],
            //! Category should be filtered through an operator not searched directly
            // SearchableEntityCategory data
            //     => [new ScoredMatch(Fuzz.Ratio(NormalizedContent, data.Category.ToString("G").ToLowerInvariant()), depth, trait, this)],
            // TODO: add negative weights to manufacturer and location (less important than search by name unless specifically targeted)
            // SearchableManufacturer data => Match(data.Manufacturer.SearchableAttributes.OfType<SearchableTextTrait>(), depth + 1),
            // SearchableLocation data => Match(data.Location.Parent?.SearchableAttributes ?? [], depth + 1),
            SearchableName data => MatchExact(data.Name, trait, depth),
            // SearchableName data => MatchExact(data.Name, trait, depth, () => Fuzz.WeightedRatio(Content, data.Name)),
            _ => [new NoMatch(trait, this)],
        };

    private IEnumerable<SearchMatch> MatchExact(string data, SearchableTrait trait, int depth, Func<int>? fallback = null)
    {
        var score = data.Equals(NormalizedContent, StringComparison.OrdinalIgnoreCase)
            ? 100
            : data.StartsWith(NormalizedContent, StringComparison.OrdinalIgnoreCase)
                ? 99
                : data.EndsWith(NormalizedContent, StringComparison.OrdinalIgnoreCase)
                    ? 98
                    : data.Contains(NormalizedContent, StringComparison.OrdinalIgnoreCase)
                        ? 97
                        : fallback?.Invoke() ?? 0;

        if (score == 0)
        {
            return [new NoMatch(trait, this)];
        }

        return [new ScoredMatch(score, depth, trait, this)];
    }

    public static SearchQuery Create(string content)
        => new FuzzyTextSearch(content);
}

public sealed record LocationSearch(IGameLocation Location) : SearchQuery
{
    public override IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0)
        => trait switch
        {
            SearchableLocation data => Location.IsOrContains(data.Location)
                ? [new SoftMatch(trait, this)]
                : [new ExcludeMatch(trait, this)],
            _ => [new NoMatch(trait, this)],
        };
}

public sealed record EntityCategorySearch(GameEntityCategory Category) : SearchQuery
{
    public override IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0)
        => trait switch
        {
            SearchableEntityCategory data => data.Category == Category
                ? [new SoftMatch(trait, this)]
                : [new ExcludeMatch(trait, this)],
            _ => [new NoMatch(trait, this)],
        };
}
