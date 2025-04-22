namespace Arkanis.Overlay.Domain.Models.Trade;

using Abstractions.Game;
using Common;
using NodaMoney;

public abstract record PriceTag : IComparable<PriceTag>
{
    public static readonly PriceTag Unknown = new UnknownPriceTag();

    public int CompareTo(PriceTag? other)
        => this is UserPriceTag userPriceTag && other is UserPriceTag otherUserPriceTag
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

public sealed record UnknownPriceTag : PriceTag;

public sealed record MissingPriceTag(IGameLocation Location) : PriceTag;

public sealed record UserPriceTag(Money Price, IGameLocation Location, DateTimeOffset UpdatedAt) : PriceTag, IComparable<UserPriceTag>
{
    public int CompareTo(UserPriceTag? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is not null
            ? Price.CompareTo(other.Price)
            : 1;
    }

    public static UserPriceTag Create(long price, IGameLocation location, DateTimeOffset updatedAt)
        => new(new Money(price, ApplicationConstants.GameCurrency), location, updatedAt);

    public static bool operator <(UserPriceTag left, UserPriceTag right)
        => ReferenceEquals(left, null)
            ? !ReferenceEquals(right, null)
            : left.CompareTo(right) < 0;

    public static bool operator <=(UserPriceTag left, UserPriceTag right)
        => ReferenceEquals(left, null) || left.CompareTo(right) <= 0;

    public static bool operator >(UserPriceTag left, UserPriceTag right)
        => !ReferenceEquals(left, null) && left.CompareTo(right) > 0;

    public static bool operator >=(UserPriceTag left, UserPriceTag right)
        => ReferenceEquals(left, null)
            ? ReferenceEquals(right, null)
            : left.CompareTo(right) >= 0;
}
