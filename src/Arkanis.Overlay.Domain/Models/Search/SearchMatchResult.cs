namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions;

public abstract record SearchMatchResult(List<SearchMatch> Matches) : IComparable<SearchMatchResult>
{
    public IEnumerable<ScoredMatch> ScoredMatches
        => Matches.OfType<ScoredMatch>();

    public IEnumerable<SearchQuery> UnmatchedQueries
        => Matches.GroupBy(match => match.Source)
            .Where(group => group.All(match => match is NoMatch))
            .Select(x => x.Key);

    public double ScoreTotal
        => ScoredMatches.Select((match, index) => match.NormalizedScore / double.Pow(2, index)).Sum();

    public bool ShouldBeExcluded
        => IsExplicitlyExcluded
           || (ScoreTotal == 0 && !HasMatched);

    private bool IsExplicitlyExcluded
        => Matches.OfType<ExcludeMatch>().Any();

    private bool HasMatched
        => Matches.OfType<SoftMatch>().Any()
           || Matches.OfType<ExactMatch>().Any();

    public int CompareTo(SearchMatchResult? other)
    {
        if (IsExplicitlyExcluded)
        {
            return -1;
        }

        return ScoreTotal.CompareTo(other?.ScoreTotal ?? 0);
    }

    public static SearchMatchResult<T> Create<T>(T subject, IEnumerable<SearchMatch> matches) where T : ISearchable
        => new(subject, GroupAndPickBest(matches));

    public static SearchMatchResult<T> CreateEmpty<T>(T subject) where T : ISearchable
        => new(subject, []);

    public static bool operator <(SearchMatchResult left, SearchMatchResult right)
        => ReferenceEquals(left, null)
            ? !ReferenceEquals(right, null)
            : left.CompareTo(right) < 0;

    public static bool operator <=(SearchMatchResult left, SearchMatchResult right)
        => ReferenceEquals(left, null) || left.CompareTo(right) <= 0;

    public static bool operator >(SearchMatchResult left, SearchMatchResult right)
        => !ReferenceEquals(left, null) && left.CompareTo(right) > 0;

    public static bool operator >=(SearchMatchResult left, SearchMatchResult right)
        => ReferenceEquals(left, null)
            ? ReferenceEquals(right, null)
            : left.CompareTo(right) >= 0;

    protected static List<SearchMatch> GroupAndPickBest(IEnumerable<SearchMatch> matches)
        => matches
            .GroupBy(match => (match.GetType(), match.TargetTrait))
            .Select(group => group.Max()!)
            .OrderDescending()
            .ToList();
}

public record SearchMatchResult<TSubject>(TSubject Subject, List<SearchMatch> Matches)
    : SearchMatchResult(Matches) where TSubject : ISearchable
{
    public SearchMatchResult<TSubject> Merge(SearchMatchResult other)
        => this with
        {
            Matches = GroupAndPickBest(other.Matches.Concat(Matches)),
        };

    public bool ContainsUnmatched<TSource>(Func<UnmatchedSearch<TSource>, bool> predicate) where TSource : SearchQuery
        => UnmatchedQueries.OfType<TSource>()
            .Select(query => new UnmatchedSearch<TSource>(Subject, query))
            .Any(predicate);

    public record UnmatchedSearch<TSource>(TSubject Subject, TSource Query) where TSource : SearchQuery;
}
