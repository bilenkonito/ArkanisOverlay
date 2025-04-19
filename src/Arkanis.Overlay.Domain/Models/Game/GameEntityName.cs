namespace Arkanis.Overlay.Domain.Models.Game;

using System.Collections;

public sealed record GameEntityName(IEnumerable<GameEntityName.Part> Parts) : IEnumerable<GameEntityName.Part>
{
    public GameEntityName(params Part[] parts) : this(parts.AsEnumerable())
    {
    }

    public IEnumerator<Part> GetEnumerator()
        => Parts.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public static Reference ReferenceTo(GameEntity entity)
        => Reference.Create(entity);

    public record Part;

    public abstract record Reference(GameEntity ReferencedEntity) : Part
    {
        public GameEntityName EntityName
            => ReferencedEntity.Name;

        public static Reference Create(GameEntity referencedEntity)
            => referencedEntity switch
            {
                GameItemCategory category => new ItemCategoryReference(category),
                GameCompany company => new CompanyReference(company),
                GameLocationEntity location => new LocationReference(location),
                _ => throw new ArgumentException($"Cannot create name reference for: {referencedEntity.GetType()}"),
            };
    }

    public sealed record ItemCategoryReference(GameItemCategory Category) : Reference(Category);

    public sealed record CompanyReference(GameCompany Company) : Reference(Company);

    public sealed record LocationReference(GameLocationEntity Location) : Reference(Location);

    public sealed record Separator(string Content) : Part;

    public record Name(string FullName) : Part;

    public sealed record NameWithCode(string FullName, string Code) : Name(FullName);

    public sealed record NameWithCodeAndShortVariant(string FullName, string Code, string ShortName) : Name(FullName);

    public sealed record NameWithShortVariant(string FullName, string ShortName) : Name(FullName);
}
