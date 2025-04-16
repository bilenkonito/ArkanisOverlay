namespace Arkanis.Overlay.Domain.Models;

using Enums;

public record LocationPrice(
    string Location,
    PriceType Type,
    decimal Price,
    DateTime LastUpdated
);
