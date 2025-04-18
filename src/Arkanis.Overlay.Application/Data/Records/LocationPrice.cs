namespace Arkanis.Overlay.Application.Data.Records;

using Enums;

public record LocationPrice(
    string Location,
    PriceType Type,
    decimal Price,
    DateTime LastUpdated
);
