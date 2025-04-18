namespace Arkanis.Overlay.Domain.Models.Game;

public sealed record GameEntityName(IEnumerable<GameEntityName.Part> Parts)
{
    public GameEntityName(params Part[] parts) : this(parts.AsEnumerable())
    {
    }

    public record Part;

    public abstract record Reference(GameEntity ReferencedEntity) : Part
    {
        public GameEntityName EntityName
            => ReferencedEntity.Name;

        public static Reference Create(GameEntity referencedEntity)
            => referencedEntity switch
            {
                GameCompany company => new CompanyReference(company),
                GameLocationEntity location => new LocationReference(location),
                _ => throw new ArgumentException($"Cannot create name reference for: {referencedEntity.GetType()}"),
            };
    }

    public sealed record CompanyReference(GameCompany Company) : Reference(Company);

    public sealed record LocationReference(GameLocationEntity Location) : Reference(Location);

    public sealed record Separator(string Content) : Part;

    public record Name(string FullName) : Part;

    public sealed record NameWithCode(string FullName, string Code) : Name(FullName);

    public sealed record NameWithCodeAndShortVariant(string FullName, string Code, string ShortName) : Name(FullName);

    public sealed record NameWithShortVariant(string FullName, string ShortName) : Name(FullName);
}
