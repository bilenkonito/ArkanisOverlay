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

    public static string PickIconFor(GameEntityCategory value)
        => value switch
        {
            GameEntityCategory.Commodity => Icons.Material.Outlined.Diamond,
            GameEntityCategory.Vehicle => Icons.Material.Outlined.CarRental,
            GameEntityCategory.Item => Icons.Material.Outlined.Category,
            _ => DefaultIcon,
        };

    public static string PickIconFor<T>(T value)
        => value switch
        {
            GameEntityCategory x => PickIconFor(x),
            PriceType x => PickIconFor(x),
            _ => DefaultIcon,
        };
}
