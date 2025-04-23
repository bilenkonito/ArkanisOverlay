namespace Arkanis.Overlay.Domain.Models.Game;

using Abstractions.Game;

public abstract record TypedGameEntityId<T>(T Identity) : IGameEntityId where T : notnull
{
    private static readonly string TypeName = $"{nameof(IGameEntityId)}+{typeof(T).Name}";

    public virtual bool Equals(IGameEntityId? other)
        => other is TypedGameEntityId<T> otherTyped && Identity.Equals(otherTyped.Identity);

    public override int GetHashCode()
        => HashCode.Combine(TypeName.GetHashCode(), Identity.GetHashCode());
}

public sealed record UexApiGameEntityId(int Identity) : TypedGameEntityId<int>(Identity)
{
    public bool Equals(double? uexApiId)
        => (int)(uexApiId ?? 0) == Identity;

    public static UexApiGameEntityId Create(int identity)
        => new(identity);
}
