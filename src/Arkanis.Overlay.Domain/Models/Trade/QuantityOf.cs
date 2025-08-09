namespace Arkanis.Overlay.Domain.Models.Trade;

using Game;
using Inventory;

public record QuantityOf(OwnableEntityReference Reference, int Amount, Quantity.UnitType Unit) : Quantity(Amount, Unit), IFormattable
{
    public const string FormatWithReferenceName = nameof(FormatWithReferenceName);

    public const string FormatWithReferenceCode = nameof(FormatWithReferenceCode);

    public QuantityOf(OwnableEntityReference reference, Quantity quantity) : this(reference, quantity.Amount, quantity.Unit)
    {
    }

    public OwnableEntityReference Reference { get; set; } = Reference;

    private string ReferenceFullName
        => Reference.Entity.Name.MainContent.FullName;

    private string ReferenceCode
        => Reference.Entity.Name.MainContent is GameEntityName.IHasCode codeVariant
            ? codeVariant.Code
            : ReferenceFullName;

    public string ToString(string? format, IFormatProvider? formatProvider)
        => format switch
        {
            FormatWithReferenceName => Unit switch
            {
                UnitType.Count => $"{base.ToString()} {ReferenceFullName}",
                _ => $"{base.ToString()} of {ReferenceFullName}",
            },
            FormatWithReferenceCode => Unit switch
            {
                UnitType.Count => $"{base.ToString()} {ReferenceCode}",
                _ => $"{base.ToString()} of {ReferenceCode}",
            },
            _ => base.ToString(),
        };

    public override string ToString()
        => ToString(null, null);

    public static QuantityOf Create(GameItem item, Quantity quantity)
        => new(new OwnableEntityReference.Item(item), quantity);

    public static QuantityOf Create(GameCommodity commodity, Quantity quantity)
        => new(new OwnableEntityReference.Commodity(commodity), quantity);

    public static QuantityOf Create(GameVehicle vehicle, Quantity quantity)
        => new(new OwnableEntityReference.Vehicle(vehicle), quantity);

    public static IEnumerable<QuantityOf> Aggregate(IEnumerable<QuantityOf> quantities)
        => quantities
            .GroupBy(quantity => quantity.Reference.EntityId)
            .Select(quantitiesByEntity =>
                {
                    var firstQuantity = quantitiesByEntity.First();
                    return new QuantityOf(
                        firstQuantity.Reference,
                        Quantity.Aggregate(quantitiesByEntity).Single()
                    );
                }
            );
}
