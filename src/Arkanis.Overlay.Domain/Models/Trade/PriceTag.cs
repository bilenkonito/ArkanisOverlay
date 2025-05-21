namespace Arkanis.Overlay.Domain.Models.Trade;

using Abstractions.Game;
using Game;

public abstract record PriceTag : IComparable<PriceTag>
{
    public static readonly PriceTag Unknown = UnknownPriceTag.Instance;

    public int CompareTo(PriceTag? other)
        => this is KnownPriceTag userPriceTag && other is KnownPriceTag otherUserPriceTag
            ? userPriceTag.CompareTo(otherUserPriceTag)
            : -1;

    public static bool operator <(PriceTag left, PriceTag right)
        => ReferenceEquals(left, null)
            ? !ReferenceEquals(right, null)
            : left.CompareTo(right) < 0;

    public static bool operator <=(PriceTag left, PriceTag right)
        => ReferenceEquals(left, null) || left.CompareTo(right) <= 0;

    public static bool operator >(PriceTag left, PriceTag right)
        => !ReferenceEquals(left, null) && left.CompareTo(right) > 0;

    public static bool operator >=(PriceTag left, PriceTag right)
        => ReferenceEquals(left, null)
            ? ReferenceEquals(right, null)
            : left.CompareTo(right) >= 0;

    public static PriceTag MissingFor(IGameLocation location)
        => new MissingPriceTag(location);
}

public sealed record UnknownPriceTag : PriceTag
{
    private UnknownPriceTag()
    {
    }

    public static UnknownPriceTag Instance { get; } = new();
}

public sealed record MissingPriceTag(IGameLocation Location) : PriceTag;

public sealed record AggregatePriceTag(GameCurrency Price) : PriceTag, IComparable<AggregatePriceTag>
{
    public int CompareTo(AggregatePriceTag? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is not null
            ? Price.CompareTo(other.Price)
            : 1;
    }
}

public record KnownPriceTag(GameCurrency Price, DateTimeOffset UpdatedAt) : PriceTag, IComparable<KnownPriceTag>
{
    public int CompareTo(KnownPriceTag? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is not null
            ? Price.CompareTo(other.Price)
            : 1;
    }

    public static bool operator <(KnownPriceTag left, KnownPriceTag right)
        => ReferenceEquals(left, null)
            ? !ReferenceEquals(right, null)
            : left.CompareTo(right) < 0;

    public static bool operator <=(KnownPriceTag left, KnownPriceTag right)
        => ReferenceEquals(left, null) || left.CompareTo(right) <= 0;

    public static bool operator >(KnownPriceTag left, KnownPriceTag right)
        => !ReferenceEquals(left, null) && left.CompareTo(right) > 0;

    public static bool operator >=(KnownPriceTag left, KnownPriceTag right)
        => ReferenceEquals(left, null)
            ? ReferenceEquals(right, null)
            : left.CompareTo(right) >= 0;
}

public record KnownPriceTagWithLocation(GameCurrency Price, IGameLocation Location, DateTimeOffset UpdatedAt)
    : KnownPriceTag(Price, UpdatedAt), IGameLocatedAt;
