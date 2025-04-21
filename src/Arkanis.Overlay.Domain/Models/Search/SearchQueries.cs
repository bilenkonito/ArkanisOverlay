namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions;
using Abstractions.Game;
using Enums;
using FuzzySharp;

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

public sealed record FuzzyStringSearch(string Content) : SearchQuery
{
    private readonly string _normalizedContent = Content.ToLowerInvariant();

    public override IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0)
        => trait switch
        {
            SearchableCode data => [new ScoredMatch(Fuzz.Ratio(Content, data.Code), depth, trait)],
            SearchableEntityCategory data => [new ScoredMatch(Fuzz.Ratio(_normalizedContent, data.Category.ToString("G").ToLowerInvariant()), depth, trait)],
            SearchableManufacturer data => Match(data.Manufacturer.SearchableAttributes.OfType<SearchableTextTrait>(), depth + 1),
            SearchableLocation data => Match(data.Location.Parent?.SearchableAttributes.OfType<SearchableTextTrait>() ?? [], depth + 1),
            SearchableName data => [new ScoredMatch(Fuzz.TokenSortRatio(Content, data.Name), depth, trait)],
            _ => [new NoMatch(trait)],
        };

    public static SearchQuery Create(string content)
        => new FuzzyStringSearch(content);
}

public sealed record LocationSearch(IGameLocation Location) : SearchQuery
{
    public override IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0)
        => trait switch
        {
            SearchableLocation data => Location.IsOrContains(data.Location)
                ? [new SoftMatch(trait)]
                : [new ExcludeMatch(trait)],
            _ => [new NoMatch(trait)],
        };
}

public sealed record EntityCategorySearch(GameEntityCategory Category) : SearchQuery
{
    public override IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0)
        => trait switch
        {
            SearchableEntityCategory data => data.Category == Category
                ? [new SoftMatch(trait)]
                : [new ExcludeMatch(trait)],
            _ => [new NoMatch(trait)],
        };
}
