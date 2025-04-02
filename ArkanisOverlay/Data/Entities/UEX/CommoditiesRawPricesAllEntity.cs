using System.Text.Json.Serialization;

namespace ArkanisOverlay.Data.Entities.UEX;

public class CommoditiesRawPricesAllEntity: BaseEntity
{
    [JsonPropertyName("id_commodity")] public int IdCommodity { get; set; }
    [JsonPropertyName("id_terminal")] public int IdTerminal { get; set; }
    [JsonPropertyName("price_sell")] public decimal PriceSell { get; set; }
    [JsonPropertyName("price_sell_avg")] public decimal PriceSellAvg { get; set; }
    [JsonPropertyName("commodity_name")] public string? CommodityName { get; set; }
    [JsonPropertyName("commodity_code")] public string? CommodityCode { get; set; }
    [JsonPropertyName("commodity_slug")] public string? CommoditySlug { get; set; }
    [JsonPropertyName("terminal_name")] public string? TerminalName { get; set; }
    [JsonPropertyName("terminal_code")] public string? TerminalCode { get; set; }
    [JsonPropertyName("terminal_slug")] public string? TerminalSlug { get; set; }
}