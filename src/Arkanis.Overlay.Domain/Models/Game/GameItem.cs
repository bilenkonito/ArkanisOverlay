namespace Arkanis.Overlay.Domain.Models.Game;

using System.Collections;
using Abstractions.Game;
using Enums;
using Search;
using Trade;

public class GameItem(int id, string fullName, GameCompany manufacturer, GameProductCategory category)
    : GameEntity(UexApiGameEntityId.Create<GameItem>(id), GameEntityCategory.Item), IGameManufactured, IGamePurchasable
{
    public TraitCollection Traits { get; private set; } = new();

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

    public override GameEntityName Name { get; } = new(
        GameEntityName.ReferenceTo(category),
        GameEntityName.ReferenceTo(manufacturer),
        new GameEntityName.Name(fullName)
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

    public abstract record Trait(string Name)
    {
        public static Trait? CreateFrom(string name, string value, string? unit = null)
            => name switch
            {
                "Size" => Size.Create(value, value),
                "Grade" => Grade.Create(value, value),
                "Class" or "Armor Class" => ClassTrait.Create(value, value),
                "Mass" when unit is not null => Mass.Create(value, value, unit),
                _ => null,
            };
    }

    public sealed record Size(string Name, int Value) : Trait(Name)
    {
        public static Trait? Create(string name, string value)
            => int.TryParse(value, out var size)
                ? new Size(name, size)
                : null;
    }

    public sealed record Grade(string Name, string Value) : Trait(Name)
    {
        public static Trait Create(string name, string value)
            => new Grade(name, value);
    }

    public sealed record ClassTrait(string Name, string Value) : Trait(Name)
    {
        public static Trait Create(string name, string value)
            => new ClassTrait(name, value);
    }

    public sealed record Mass(string Name, int Value, string Unit) : Trait(Name)
    {
        public static Trait? Create(string name, string value, string unit)
            => int.TryParse(value, out var size)
                ? new Mass(name, size, unit)
                : null;
    }
}
