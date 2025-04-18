namespace Arkanis.Overlay.Domain.Models.Trade;

using Abstractions.Game;
using Common;
using NodaMoney;

public sealed record PriceTag(Money Price, IGameLocation Location, DateTimeOffset UpdatedAt) : IComparable<PriceTag>
{
    public int CompareTo(PriceTag? other)
    {
        if (ReferenceEquals(this, other))
        {
            return 0;
        }

        return other is not null
            ? Price.CompareTo(other.Price)
            : 1;
    }

    public static PriceTag Create(long price, IGameLocation location, DateTimeOffset updatedAt)
        => new(new Money(price, ApplicationConstants.GameCurrency), location, updatedAt);

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
}
