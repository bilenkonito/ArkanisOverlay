namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions;

public record SearchMatchResult(ISearchable Subject, List<SearchMatch> Matches) : IComparable<SearchMatchResult>
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

    public T Merge<T>(T other) where T : SearchMatchResult
    {
        if (Subject != other.Subject)
        {
            throw new InvalidOperationException("Cannot merge match result of two different subjects.");
        }

        return other with
        {
            Matches = other.Matches.Concat(Matches)
                .GroupBy(match => (match.GetType(), match.TargetTrait))
                .Select(group => group.Max()!)
                .OrderDescending()
                .ToList(),
        };
    }

    public static SearchMatchResult Create(ISearchable subject, IEnumerable<SearchMatch> matches)
        => new(subject, PreprocessMatches(matches));

    public static SearchMatchResult<T> Create<T>(T subject, IEnumerable<SearchMatch> matches) where T : ISearchable
        => new(subject, PreprocessMatches(matches));

    private static List<SearchMatch> PreprocessMatches(IEnumerable<SearchMatch> matches)
        => matches
            .Where(match => match is not NoMatch)
            .Where(match => match is not ScoredMatch
                {
                    Score: 0,
                }
            )
            .OrderDescending()
            .ToList();
}

public record SearchMatchResult<T>(T TypedSubject, List<SearchMatch> Matches)
    : SearchMatchResult(TypedSubject, Matches) where T : ISearchable;
