namespace Arkanis.Overlay.Application.Data.Entities.UEX;

public class CommoditiesPricesAllEntity : BaseEntity
{
    [ForeignKey(nameof(Commodity))]
    [JsonPropertyName("id_commodity")]
    public int IdCommodity { get; set; }

    // Navigation property
    [JsonIgnore]
    public virtual CommodityEntity Commodity { get; set; } = null!;

    [JsonPropertyName("id_terminal")]
    public int IdTerminal { get; set; }

    [JsonPropertyName("price_buy")]
    public decimal PriceBuy { get; set; } // last

    [JsonPropertyName("price_buy_avg")]
    public decimal PriceBuyAvg { get; set; }

    [JsonPropertyName("price_sell")]
    public decimal PriceSell { get; set; } // last

    [JsonPropertyName("price_sell_avg")]
    public decimal PriceSellAvg { get; set; }

    [JsonPropertyName("scu_buy")]
    public decimal ScuBuy { get; set; } // last

    [JsonPropertyName("scu_buy_avg")]
    public decimal ScuBuyAvg { get; set; }

    [JsonPropertyName("scu_sell_stock")]
    public decimal ScuSellStock { get; set; } // last

    [JsonPropertyName("scu_sell_stock_avg")]
    public decimal ScuSellStockAvg { get; set; } // average reported

    [JsonPropertyName("scu_sell")]
    public decimal ScuSell { get; set; }

    [JsonPropertyName("scu_sell_avg")]
    public decimal ScuSellAvg { get; set; }

    [JsonPropertyName("status_buy")]
    public int? StatusBuy { get; set; }

    [JsonPropertyName("status_sell")]
    public int? StatusSell { get; set; }

    [JsonPropertyName("commodity_name")]
    public string? CommodityName { get; set; }

    [JsonPropertyName("commodity_code")]
    public string? CommodityCode { get; set; }

    [JsonPropertyName("commodity_slug")]
    public string? CommoditySlug { get; set; }

    [JsonPropertyName("terminal_name")]
    public string? TerminalName { get; set; }

    [JsonPropertyName("terminal_code")]
    public string? TerminalCode { get; set; }

    [JsonPropertyName("terminal_slug")]
    public string? TerminalSlug { get; set; }
}
