using Arkanis.Overlay.Application.Data.Enums;

namespace Arkanis.Overlay.Application.Data.Records;

public record LocationPrice(
    string Location,
    PriceType Type,
    decimal Price,
    DateTime LastUpdated);