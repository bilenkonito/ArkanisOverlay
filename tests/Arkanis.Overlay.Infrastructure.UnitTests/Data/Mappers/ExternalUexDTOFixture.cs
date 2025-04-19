namespace Arkanis.Overlay.Infrastructure.UnitTests.Data.Mappers;

using External.UEX.Abstractions;

public static class ExternalUexDTOFixture
{
    public static CompanyDTO ItemCompany { get; } = new()
    {
        Id = 55,
        Is_item_manufacturer = 1,
        Is_vehicle_manufacturer = 0,
        Name = "Code Blue Apparel",
        Nickname = "Code Blue",
    };

    public static CategoryDTO ItemHatsCategory { get; } = new()
    {
        Id = 10,
        Type = "item",
        Section = "Clothing",
        Name = "Hats",
        Is_game_related = 1,
        Is_mining = 0,
    };

    public static ItemDTO Item { get; } = new()
    {
        Id = 998,
        Uuid = "6cb8150c-6e2d-4d93-9f11-3ba743227215",
        Id_category = ItemHatsCategory.Id,
        Id_company = ItemCompany.Id,
        Name = "Col Head Cover Maroon",
        Section = "Clothing",
        Category = "Hats",
    };

    public static UniverseStarSystemDTO StarSystem { get; } = new()
    {
        Id = 68,
        Name = "Stanton",
        Code = "ST",
    };

    public static UniversePlanetDTO Planet { get; } = new()
    {
        Id = 59,
        Id_star_system = StarSystem.Id,
        Name = "Crusader",
        Code = "CRU",
    };

    public static UniverseMoonDTO Moon { get; } = new()
    {
        Id = 25,
        Id_star_system = StarSystem.Id,
        Id_planet = Planet.Id,
        Name = "Daymar",
        Code = "DAY",
    };

    public static UniverseCityDTO City { get; } = new()
    {
        Id = 5,
        Id_star_system = StarSystem.Id,
        Id_planet = Planet.Id,
        Name = "Orison",
        Code = "ORIS",
    };

    public static UniverseSpaceStationDTO SpaceStation { get; } = new()
    {
        Id = 27,
        Id_star_system = StarSystem.Id,
        Id_planet = Planet.Id,
        Id_city = City.Id,
        Name = "Seraphim Station",
        Nickname = "Seraphim",
    };

    public static UniverseOutpostDTO Outpost { get; } = new()
    {
        Id = 48,
        Id_star_system = StarSystem.Id,
        Id_moon = Moon.Id,
        Name = "Shubin Mining Facility SCD-1",
        Nickname = "Shubin SCD-1",
    };

    public static UniverseTerminalDTO OutpostTerminal { get; } = new()
    {
        Id = 79,
        Id_star_system = StarSystem.Id,
        Id_moon = Moon.Id,
        Id_outpost = Outpost.Id,
        Name = "Shubin Mining Facility SCD-1",
        Nickname = "Shubin SCD-1",
        Code = "SSCD1",
        Type = "commodity",
    };
}
