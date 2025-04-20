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

    public virtual IEnumerable<SearchMatch> Match(IEnumerable<SearchableAttribute> attributes)
        => attributes.SelectMany(Match);

    public abstract IEnumerable<SearchMatch> Match(SearchableAttribute attribute);
}

public sealed record FuzzyStringSearch(string Content) : SearchQuery
{
    private readonly string _normalizedContent = Content.ToLowerInvariant();

    public override IEnumerable<SearchMatch> Match(SearchableAttribute attribute)
        => attribute switch
        {
            SearchableCode attr => [new ScoredMatch(Fuzz.Ratio(Content, attr.Code), attribute)],
            SearchableEntityCategory attr => [new ScoredMatch(Fuzz.Ratio(Content, attr.Category.ToString()), attribute)],
            SearchableLocation attr => Match(attr.Location.SearchableAttributes),
            SearchableName attr => [new ScoredMatch(Fuzz.TokenSortRatio(_normalizedContent, attr.NormalizedName), attribute)],
            _ => [new NoMatch(attribute)],
        };
}

public sealed record LocationSearch(IGameLocation Location) : SearchQuery
{
    public override IEnumerable<SearchMatch> Match(SearchableAttribute attribute)
        => attribute switch
        {
            SearchableLocation attr => attr.Location == Location || attr.Location.Contains(Location)
                ? [new SoftMatch(attribute)]
                : [new ExcludeMatch(attribute)],
            _ => [new NoMatch(attribute)],
        };
}

public sealed record EntityCategorySearch(GameEntityCategory Category) : SearchQuery
{
    public override IEnumerable<SearchMatch> Match(SearchableAttribute attribute)
        => attribute switch
        {
            SearchableEntityCategory attr => attr.Category == Category
                ? [new SoftMatch(attribute)]
                : [new ExcludeMatch(attribute)],
            _ => [new NoMatch(attribute)],
        };
}
