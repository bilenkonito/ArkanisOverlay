namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions;

public abstract record SearchMatchResult(List<SearchMatch> Matches) : IComparable<SearchMatchResult>
{
    public IEnumerable<ScoredMatch> ScoredMatches
        => Matches.OfType<ScoredMatch>();

    public double ScoreTotal
        => ScoredMatches.Select((match, index) => match.NormalizedScore / double.Pow(2, index)).Sum();

    public bool IsExplicitlyExcluded
        => Matches.OfType<ExcludeMatch>().Any();

    public bool ShouldBeExcluded
        => IsExplicitlyExcluded
           || (ScoreTotal == 0 && !HasMatched);

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
        => new(subject, PreprocessMatches(matches));

    public static SearchMatchResult<T> CreateEmpty<T>(T subject) where T : ISearchable
        => new(subject, []);

    private static List<SearchMatch> PreprocessMatches(IEnumerable<SearchMatch> matches)
        => matches
            .Where(match => match is not NoMatch)
            .Where(match => match is not ScoredMatch { Score: 0 })
            .OrderDescending()
            .ToList();

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
}

public record SearchMatchResult<T>(T Subject, List<SearchMatch> Matches)
    : SearchMatchResult(Matches) where T : ISearchable
{
    public SearchMatchResult<T> Merge(SearchMatchResult other)
        => this with
        {
            Matches = other.Matches.Concat(Matches)
                .GroupBy(match => (match.GetType(), match.TargetTrait))
                .Select(group => group.Max()!)
                .OrderDescending()
                .ToList(),
        };
}
