namespace Arkanis.Overlay.Application.Data.Entities.UEX;

public class CommodityEntity : BaseEntity
{
    [JsonPropertyName("id_parent")]
    public int IdParent { get; init; } // int(11)

    [JsonPropertyName("name")]
    public string? Name { get; init; } // string(255)

    [JsonPropertyName("code")]
    public string? Code { get; init; } // string(5) // UEX commodity code

    [JsonPropertyName("slug")]
    public string? Slug { get; init; } // string(255) // UEX URL slug

    [JsonPropertyName("kind")]
    public string? Kind { get; init; } // string(255)

    [JsonPropertyName("weight_scu")]
    public decimal WeightScu { get; init; } // ! WRONG ! // float, e.g. 1.2 // int(11) // tons per SCU

    [JsonPropertyName("price_buy")]
    public decimal PriceBuy { get; init; } // float // average market price per SCU

    [JsonPropertyName("price_sell")]
    public decimal PriceSell { get; init; } // float // average market price per SCU

    [JsonPropertyName("is_available")]
    public bool IsAvailable { get; init; } // int(1) // UEX website

    [JsonPropertyName("is_available_live")]
    public bool IsAvailableLive { get; init; } // int(1) // Star Citizen (LIVE servers)

    [JsonPropertyName("is_visible")]
    public bool IsVisible { get; init; } // int(1) // UEX website (visible to everyone)

    [JsonPropertyName("is_raw")]
    public bool IsRaw { get; init; } // int(1)

    [JsonPropertyName("is_refined")]
    public bool IsRefined { get; init; } // int(1)

    [JsonPropertyName("is_mineral")]
    public bool IsMineral { get; init; } // int(1)

    [JsonPropertyName("is_harvestable")]
    public bool IsHarvestable { get; init; } // int(1)

    [JsonPropertyName("is_buyable")]
    public bool IsBuyable { get; init; } // int(1)

    [JsonPropertyName("is_sellable")]
    public bool IsSellable { get; init; } // int(1)

    [JsonPropertyName("is_temporary")]
    public bool IsTemporary { get; init; } // int(1)

    [JsonPropertyName("is_illegal")]
    public bool IsIllegal { get; init; } // int(1) // if the commodity is restricted in one or more jurisdictions

    [JsonPropertyName("is_fuel")]
    public bool IsFuel { get; init; } // int(1)

    [JsonPropertyName("wiki")]
    public string? Wiki { get; init; } // string(255)

    // Navigation properties for prices
    [JsonIgnore]
    public virtual ICollection<CommoditiesPricesAllEntity> CommoditiesPricesAll { get; set; } =
        new List<CommoditiesPricesAllEntity>();

    [JsonIgnore]
    public virtual ICollection<CommoditiesRawPricesAllEntity> CommoditiesRawPricesAll { get; set; } =
        new List<CommoditiesRawPricesAllEntity>();
}
