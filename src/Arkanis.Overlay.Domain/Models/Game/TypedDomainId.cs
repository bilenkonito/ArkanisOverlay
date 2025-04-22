namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;

public abstract record TypedDomainId<T>(T Identity) : IDomainId where T : notnull
{
    private static readonly string TypeName = $"{nameof(IDomainId)}+{typeof(T).Name}";

    public virtual bool Equals(IDomainId? other)
        => other is TypedDomainId<T> otherTyped && Identity.Equals(otherTyped.Identity);

    public override int GetHashCode()
        => HashCode.Combine(TypeName.GetHashCode(), Identity.GetHashCode());
}
