using System.Text.Json.Serialization;

namespace ArkanisOverlay.Data.UEX.DTO;
public class CommodityDto : BaseDto
{
    [JsonPropertyName("id_parent")]
    public int IdParent { get; set; } // int(11)
    [JsonPropertyName("name")]
    public string? Name { get; set; } // string(255)
    [JsonPropertyName("code")]
    public string? Code { get; set; } // string(5) // UEX commodity code
    [JsonPropertyName("slug")]
    public string? Slug { get; set; } // string(255) // UEX URL slug
    [JsonPropertyName("kind")]
    public string? Kind { get; set; } // string(255)
    [JsonPropertyName("weight_scu")]
    public float WeightScu { get; set; } // ! WRONG ! // float, e.g. 1.2 // int(11) // tons per SCU
    [JsonPropertyName("price_buy")]
    public float PriceBuy { get; set; } // float // average market price per SCU
    [JsonPropertyName("price_sell")]
    public float PriceSell { get; set; } // float // average market price per SCU
    [JsonPropertyName("is_available")]
    public bool IsAvailable { get; set; } // int(1) // UEX website
    [JsonPropertyName("is_available_live")]
    public bool IsAvailableLive { get; set; } // int(1) // Star Citizen (LIVE servers)
    [JsonPropertyName("is_visible")]
    public bool IsVisible { get; set; } // int(1) // UEX website (visible to everyone)
    [JsonPropertyName("is_raw")]
    public bool IsRaw { get; set; } // int(1)
    [JsonPropertyName("is_refined")]
    public bool IsRefined { get; set; } // int(1)
    [JsonPropertyName("is_mineral")]
    public bool IsMineral { get; set; } // int(1)
    [JsonPropertyName("is_harvestable")]
    public bool IsHarvestable { get; set; } // int(1)
    [JsonPropertyName("is_buyable")]
    public bool IsBuyable { get; set; } // int(1)
    [JsonPropertyName("is_sellable")]
    public bool IsSellable { get; set; } // int(1)
    [JsonPropertyName("is_temporary")]
    public bool IsTemporary { get; set; } // int(1)
    [JsonPropertyName("is_illegal")]
    public bool IsIllegal { get; set; } // int(1) // if the commodity is restricted in one or more jurisdictions
    [JsonPropertyName("is_fuel")]
    public bool IsFuel { get; set; } // int(1)
    [JsonPropertyName("wiki")]
    public string? Wiki { get; set; } // string(255)
}