namespace Arkanis.Overlay.Domain.Models.Game;

using System.Collections;

public sealed record GameEntityName(IEnumerable<GameEntityName.Part> Parts) : IEnumerable<GameEntityName.Part>
{
    private readonly Part[] _parts = Parts.ToArray();

    public GameEntityName(params Part[] parts) : this(parts.AsEnumerable())
    {
    }

    public Name MainContent
        => _parts.OfType<Name>().First();

    public CompanyReference? Company
        => _parts.OfType<CompanyReference>().FirstOrDefault();

    public LocationReference? Location
        => _parts.OfType<LocationReference>().FirstOrDefault();

    public PropertyCollection? Properties
        => _parts.OfType<PropertyCollection>().FirstOrDefault();

    public IEnumerator<Part> GetEnumerator()
        => Parts.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public static Reference ReferenceTo(GameEntity entity)
        => Reference.Create(entity);

    public abstract record Part;

    public abstract record Reference(GameEntity ReferencedEntity) : Part
    {
        public GameEntityName EntityName
            => ReferencedEntity.Name;

        public static Reference Create(GameEntity referencedEntity)
            => referencedEntity switch
            {
                GameProductCategory category => new ItemCategoryReference(category),
                GameCompany company => new CompanyReference(company),
                GameLocationEntity location => new LocationReference(location),
                _ => throw new ArgumentException($"Cannot create name reference for: {referencedEntity.GetType()}"),
            };
    }

    public interface IHasCode
    {
        string Code { get; }
    }

    public interface IHasShortName
    {
        string ShortName { get; }
    }

    public sealed record ItemCategoryReference(GameProductCategory Category) : Reference(Category);

    public sealed record CompanyReference(GameCompany Company) : Reference(Company);

    public sealed record LocationReference(GameLocationEntity Location) : Reference(Location);

    public record Name(string FullName) : Part;

    public record NameWithCode(string FullName, string Code) : Name(FullName), IHasCode;

    public sealed record NameWithCodeAndShortVariant(string FullName, string Code, string ShortName) : NameWithCode(FullName, Code), IHasShortName;

    public sealed record NameWithShortVariant(string FullName, string ShortName) : Name(FullName), IHasShortName;

    public sealed record PropertyCollection(params PropertyCollection.Item[] Items) : Part, IEnumerable<PropertyCollection.Item>
    {
        public IEnumerator<Item> GetEnumerator()
            => Items.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public sealed record Item(string Key, string Value);
    }
}
