namespace Arkanis.Overlay.Domain.Models.Search;

public abstract record SearchMatch(SearchableTrait TargetTrait, SearchQuery Source) : IComparable<SearchMatch>
{
    public abstract int CompareTo(SearchMatch? other);

    public static bool operator <(SearchMatch left, SearchMatch right)
        => ReferenceEquals(left, null)
            ? !ReferenceEquals(right, null)
            : left.CompareTo(right) < 0;

    public static bool operator <=(SearchMatch left, SearchMatch right)
        => ReferenceEquals(left, null) || left.CompareTo(right) <= 0;

    public static bool operator >(SearchMatch left, SearchMatch right)
        => !ReferenceEquals(left, null) && left.CompareTo(right) > 0;

    public static bool operator >=(SearchMatch left, SearchMatch right)
        => ReferenceEquals(left, null)
            ? ReferenceEquals(right, null)
            : left.CompareTo(right) >= 0;
}

public sealed record ExactMatch(SearchableTrait TargetTrait, SearchQuery Source) : SearchMatch(TargetTrait, Source)
{
    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ExactMatch => 0,
            _ => 1,
        };
}

public sealed record ScoredMatch(int Score, int Depth, SearchableTrait TargetTrait, SearchQuery Source) : SearchMatch(TargetTrait, Source)
{
    public double NormalizedScore { get; init; } = Score / Math.Pow(2, Depth);

    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ExactMatch => -1,
            ScoredMatch otherScoredMatch => NormalizedScore.CompareTo(otherScoredMatch.NormalizedScore),
            _ => 1,
        };
}

public sealed record SoftMatch(SearchableTrait TargetTrait, SearchQuery Source) : SearchMatch(TargetTrait, Source)
{
    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ScoredMatch => -1,
            ExactMatch => -1,
            SoftMatch => 0,
            _ => 1,
        };
}

public sealed record NoMatch(SearchableTrait TargetTrait, SearchQuery Source) : SearchMatch(TargetTrait, Source)
{
    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ExcludeMatch => 1,
            NoMatch => 0,
            _ => -1,
        };
}

public sealed record ExcludeMatch(SearchableTrait TargetTrait, SearchQuery Source) : SearchMatch(TargetTrait, Source)
{
    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ExcludeMatch => 0,
            _ => -1,
        };
}
