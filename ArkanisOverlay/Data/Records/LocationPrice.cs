using ArkanisOverlay.Data.Entities.Overlay;
using ArkanisOverlay.Data.Enums;

namespace ArkanisOverlay.Data.Records;

public record LocationPrice(
    string Location,
    PriceType Type,
    decimal Price,
    DateTime LastUpdated);