namespace Arkanis.Overlay.Domain.Models.Search;

public abstract record SearchMatch(SearchableAttribute TargetAttribute) : IComparable<SearchMatch>
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

public sealed record ScoredMatch(int Score, SearchableAttribute TargetAttribute) : SearchMatch(TargetAttribute)
{
    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ScoredMatch otherScoredMatch => Score.CompareTo(otherScoredMatch.Score),
            _ => 1,
        };
}

public sealed record ExactMatch(SearchableAttribute TargetAttribute) : SearchMatch(TargetAttribute)
{
    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ScoredMatch => -1,
            ExactMatch => 0,
            _ => 1,
        };
}

public sealed record SoftMatch(SearchableAttribute TargetAttribute) : SearchMatch(TargetAttribute)
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

public sealed record NoMatch(SearchableAttribute TargetAttribute) : SearchMatch(TargetAttribute)
{
    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ExcludeMatch => 1,
            NoMatch => 0,
            _ => -1,
        };
}

public sealed record ExcludeMatch(SearchableAttribute TargetAttribute) : SearchMatch(TargetAttribute)
{
    public override int CompareTo(SearchMatch? other)
        => other switch
        {
            ExcludeMatch => 0,
            _ => -1,
        };
}
