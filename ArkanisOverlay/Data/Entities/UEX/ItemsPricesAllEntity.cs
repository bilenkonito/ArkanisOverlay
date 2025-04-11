using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArkanisOverlay.Data.Entities.UEX;

public class ItemsPricesAllEntity : BaseEntity
{
    [ForeignKey(nameof(Item))]
    [JsonPropertyName("id_item")] 
    public int IdItem { get; set; }
    
    [JsonPropertyName("id_parent")] public int IdParent { get; set; }
    [JsonPropertyName("id_category")] public int IdCategory { get; set; }
    [JsonPropertyName("id_vehicle")] public int IdVehicle { get; set; }
    [JsonPropertyName("id_star_system")] public int IdStarSystem { get; set; }
    [JsonPropertyName("id_planet")] public int IdPlanet { get; set; }
    [JsonPropertyName("id_orbit")] public int IdOrbit { get; set; }
    [JsonPropertyName("id_moon")] public int IdMoon { get; set; }
    [JsonPropertyName("id_city")] public int IdCity { get; set; }
    [JsonPropertyName("id_outpost")] public int IdOutpost { get; set; }
    [JsonPropertyName("id_poi")] public int IdPoi { get; set; }
    [JsonPropertyName("id_faction")] public int IdFaction { get; set; }
    [JsonPropertyName("id_terminal")] public int IdTerminal { get; set; }
    [JsonPropertyName("price_buy")] public decimal PriceBuy { get; set; } // last, per unit
    [JsonPropertyName("price_buy_min")] public decimal PriceBuyMin { get; set; }
    [JsonPropertyName("price_buy_min_week")] public decimal PriceBuyMinWeek { get; set; }
    [JsonPropertyName("price_buy_min_month")] public decimal PriceBuyMinMonth { get; set; }
    [JsonPropertyName("price_buy_max")] public decimal PriceBuyMax { get; set; }
    [JsonPropertyName("price_buy_max_week")] public decimal PriceBuyMaxWeek { get; set; }
    [JsonPropertyName("price_buy_max_month")] public decimal PriceBuyMaxMonth { get; set; }
    [JsonPropertyName("price_buy_avg")] public decimal PriceBuyAvg { get; set; }
    [JsonPropertyName("price_buy_avg_week")] public decimal PriceBuyAvgWeek { get; set; }
    [JsonPropertyName("price_buy_avg_month")] public decimal PriceBuyAvgMonth { get; set; }
    [JsonPropertyName("price_sell")] public decimal PriceSell { get; set; } // last, per unit
    [JsonPropertyName("price_sell_min")] public decimal PriceSellMin { get; set; }
    [JsonPropertyName("price_sell_min_week")] public decimal PriceSellMinWeek { get; set; }
    [JsonPropertyName("price_sell_min_month")] public decimal PriceSellMinMonth { get; set; }
    [JsonPropertyName("price_sell_max")] public decimal PriceSellMax { get; set; }
    [JsonPropertyName("price_sell_max_week")] public decimal PriceSellMaxWeek { get; set; }
    [JsonPropertyName("price_sell_max_month")] public decimal PriceSellMaxMonth { get; set; }
    [JsonPropertyName("price_sell_avg")] public decimal PriceSellAvg { get; set; }
    [JsonPropertyName("price_sell_avg_week")] public decimal PriceSellAvgWeek { get; set; }
    [JsonPropertyName("price_sell_avg_month")] public decimal PriceSellAvgMonth { get; set; }
    [JsonPropertyName("durability")] public decimal Durability { get; set; } // last (%)
    [JsonPropertyName("durability_min")] public decimal DurabilityMin { get; set; }
    [JsonPropertyName("durability_min_week")] public decimal DurabilityMinWeek { get; set; }
    [JsonPropertyName("durability_min_month")] public decimal DurabilityMinMonth { get; set; }
    [JsonPropertyName("durability_max")] public decimal DurabilityMax { get; set; }
    [JsonPropertyName("durability_max_week")] public decimal DurabilityMaxWeek { get; set; }
    [JsonPropertyName("durability_max_month")] public decimal DurabilityMaxMonth { get; set; }
    [JsonPropertyName("durability_avg")] public decimal DurabilityAvg { get; set; }
    [JsonPropertyName("durability_avg_week")] public decimal DurabilityAvgWeek { get; set; }
    [JsonPropertyName("durability_avg_month")] public decimal DurabilityAvgMonth { get; set; }
    [JsonPropertyName("faction_affinity")] public int FactionAffinity { get; set; } // datarunner's affinity average at a location (between -100 and 100)
    [JsonPropertyName("game_version")] public string? GameVersion { get; set; }
    [JsonPropertyName("item_name")] public string? ItemName { get; set; }
    [JsonPropertyName("star_system_name")] public string? StarSystemName { get; set; }
    [JsonPropertyName("planet_name")] public string? PlanetName { get; set; }
    [JsonPropertyName("orbit_name")] public string? OrbitName { get; set; }
    [JsonPropertyName("moon_name")] public string? MoonName { get; set; }
    [JsonPropertyName("space_station_name")] public string? SpaceStationName { get; set; }
    [JsonPropertyName("outpost_name")] public string? OutpostName { get; set; }
    [JsonPropertyName("city_name")] public string? CityName { get; set; }
    [JsonPropertyName("terminal_name")] public string? TerminalName { get; set; }
    [JsonPropertyName("terminal_code")] public string? TerminalCode { get; set; }
    [JsonPropertyName("terminal_is_player_owned")] public int TerminalIsPlayerOwned { get; set; }

    // Navigation property for Item
    [JsonIgnore]
    public virtual ItemEntity Item { get; set; } = null!;
}