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

public abstract record UexApiGameEntityId(int Identity) : TypedDomainId<int>(Identity)
{
    public override bool Equals(IDomainId? other)
        => other is UexApiGameEntityId && base.Equals(other);

    public static UexId<T> Create<T>(int identity) where T : IGameEntity
        => new(identity);
}

public sealed record UexId<T>(int Identity) : UexApiGameEntityId(Identity) where T : IGameEntity
{
    public override bool Equals(IDomainId? other)
        => other is UexId<T> && base.Equals(other);
}
