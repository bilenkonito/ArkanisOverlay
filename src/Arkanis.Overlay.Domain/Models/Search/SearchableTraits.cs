namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions.Game;
using Enums;
using Game;

public abstract record SearchableTrait;

public sealed record UnknownTrait : SearchableTrait
{
    private UnknownTrait()
    {
    }

    public static UnknownTrait Instance { get; } = new();
}

public abstract record SearchableTextTrait(string Content) : SearchableTrait
{
    public string NormalizedContent { get; } = Content.ToLowerInvariant();
}

public sealed record SearchableName(string Name) : SearchableTextTrait(Name);

public sealed record SearchableCode(string Code) : SearchableTextTrait(Code);

public sealed record SearchableLocation(IGameLocation Location) : SearchableTrait;

public sealed record SearchableManufacturer(GameCompany Manufacturer) : SearchableTrait;

public sealed record SearchableEntityCategory(GameEntityCategory Category) : SearchableTrait;
