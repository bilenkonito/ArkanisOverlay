namespace Arkanis.Overlay.Domain.Models.Game;

using System.Collections;
using System.Globalization;
using Abstractions.Game;
using Enums;
using Search;
using Trade;

public class GameItem(int id, string fullName, GameCompany manufacturer, GameProductCategory category)
    : GameEntity(UexApiGameEntityId.Create<GameItem>(id), GameEntityCategory.Item), IGameManufactured, IGamePurchasable
{
    public TraitCollection Traits { get; } = new();

    public GameCompany Manufacturer
        => manufacturer;

    public override IEnumerable<SearchableTrait> SearchableAttributes
    {
        get
        {
            yield return new SearchableName(fullName);
            yield return new SearchableManufacturer(manufacturer);
            foreach (var searchableAttribute in base.SearchableAttributes)
            {
                yield return searchableAttribute;
            }
        }
    }

    public override GameEntityName Name
        => new(
            GameEntityName.ReferenceTo(category),
            GameEntityName.ReferenceTo(manufacturer),
            new GameEntityName.Name(fullName),
            new GameEntityName.PropertyCollection(Traits.Select(trait => trait.ToNamePart()).ToArray())
        );

    public Bounds<PriceTag> LatestPurchasePrices { get; private set; } = new(PriceTag.Unknown, PriceTag.Unknown, PriceTag.Unknown);

    public GameTerminalType TerminalType
        => GameTerminalType.Item;

    public void UpdatePurchasePrices(Bounds<PriceTag> newPrices)
        => LatestPurchasePrices = newPrices;

    public class TraitCollection : IEnumerable<Trait>
    {
        private List<Trait> _traits = [];

        public IEnumerator<Trait> GetEnumerator()
            => _traits.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public void Update(IEnumerable<Trait> traits)
            => _traits = traits.ToList();
    }

    public abstract record Trait(string Name, string Content)
    {
        public GameEntityName.PropertyCollection.Item ToNamePart()
            => new(Name, Content);

        public static Trait? CreateFrom(string name, string value, string? unit = null)
            => name switch
            {
                "Size" => Size.Create(name, value),
                "Grade" => Grade.Create(name, value),
                "Class" or "Armor Class" => ClassTrait.Create(name, value),
                "Mass" when unit is not null => Mass.Create(name, value, unit),
                _ => null,
            };
    }

    public sealed record Size(string Name, int Value) : Trait(Name, Value.ToString(CultureInfo.InvariantCulture))
    {
        public static Trait? Create(string name, string value)
            => int.TryParse(value, out var size)
                ? new Size(name, size)
                : null;
    }

    public sealed record Grade(string Name, string Content) : Trait(Name, Content)
    {
        public static Trait? Create(string name, string value)
            => !string.IsNullOrWhiteSpace(value)
                ? new Grade(name, value)
                : null;
    }

    public sealed record ClassTrait(string Name, string Content) : Trait(Name, Content)
    {
        public static Trait? Create(string name, string value)
            => !string.IsNullOrWhiteSpace(value)
                ? new ClassTrait(name, value)
                : null;
    }

    public sealed record Mass(string Name, int Value, string Unit) : Trait(Name, Value.ToString(CultureInfo.InvariantCulture))
    {
        public static Trait? Create(string name, string value, string unit)
            => int.TryParse(value, out var size)
                ? new Mass(name, size, unit)
                : null;
    }
}
