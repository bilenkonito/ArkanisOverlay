namespace Arkanis.Overlay.Components.Helpers;

using Domain.Abstractions.Services;
using Domain.Enums;
using MudBlazor;

public class IconPicker : IIconPicker
{
    private const string DefaultIcon = Icons.Material.Outlined.Square;

    public static IconPicker Instance { get; } = new();

    string IIconPicker.PickIconFor<T>(T value)
        => PickIconFor(value);

    public static string PickIconFor(PriceType value)
        => value switch
        {
            PriceType.Buy => Icons.Material.Outlined.AddShoppingCart,
            PriceType.Sell => Icons.Material.Outlined.RemoveShoppingCart,
            PriceType.Rent => Icons.Material.Outlined.CarRental,
            _ => DefaultIcon,
        };

    public static string PickIconFor(EntityType value)
        => value switch
        {
            EntityType.Commodity => Icons.Material.Outlined.Diamond,
            EntityType.Vehicle => Icons.Material.Outlined.AirportShuttle,
            EntityType.Item => Icons.Material.Outlined.Category,
            EntityType.SpaceShip => Icons.Material.Outlined.Rocket,
            _ => DefaultIcon,
        };

    public static string PickIconFor<T>(T value)
        => value switch
        {
            EntityType x => PickIconFor(x),
            PriceType x => PickIconFor(x),
            _ => DefaultIcon,
        };
}
