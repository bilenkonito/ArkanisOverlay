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

public record GuidGameEntityId(Guid Identity) : TypedGameEntityId<Guid>(Identity)
{
    public static GuidGameEntityId Create(Guid identity)
        => new(identity);
}

public record StringGameEntityId(string Identity) : TypedGameEntityId<string>(Identity)
{
    public static StringGameEntityId Create(string identity)
        => new(identity);
}

public record IntegerGameEntityId(int Identity) : TypedGameEntityId<int>(Identity)
{
    public static IntegerGameEntityId Create(int identity)
        => new(identity);
}
