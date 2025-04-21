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

public sealed record FuzzyStringSearch(string Content) : SearchQuery
{
    private readonly string _normalizedContent = Content.ToLowerInvariant();

    public override IEnumerable<SearchMatch> Match(SearchableTrait trait, int depth = 0)
        => trait switch
        {
            SearchableCode attr => [new ScoredMatch(Fuzz.Ratio(Content, attr.Code), depth, trait)],
            SearchableEntityCategory attr => [new ScoredMatch(Fuzz.Ratio(_normalizedContent, attr.Category.ToString("G").ToLowerInvariant()), depth, trait)],
            SearchableLocation attr => Match(attr.Location.SearchableAttributes, depth + 1),
            SearchableName attr => [new ScoredMatch(Fuzz.TokenSortRatio(Content, attr.Name), depth, trait)],
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
            SearchableLocation attr => attr.Location == Location || attr.Location.Contains(Location)
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
            SearchableEntityCategory attr => attr.Category == Category
                ? [new SoftMatch(trait)]
                : [new ExcludeMatch(trait)],
            _ => [new NoMatch(trait)],
        };
}
