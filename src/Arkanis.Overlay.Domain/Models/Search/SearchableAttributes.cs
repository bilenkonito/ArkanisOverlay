namespace Arkanis.Overlay.Domain.Models.Search;

using Abstractions.Game;
using Enums;
using Game;

public abstract record SearchableAttribute;

public sealed record SearchableName(string Name) : SearchableAttribute
{
    public string NormalizedName { get; } = Name.ToLowerInvariant();
}

public sealed record SearchableCode(string Code) : SearchableAttribute;

public sealed record SearchableLocation(IGameLocation Location) : SearchableAttribute;

public sealed record SearchableManufacturer(GameCompany Manufacturer) : SearchableAttribute;

public sealed record SearchableEntityCategory(GameEntityCategory Category) : SearchableAttribute;
