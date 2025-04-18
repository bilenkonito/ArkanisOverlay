namespace Arkanis.Overlay.Domain.Models.Search;

using Enums;

public class SearchResultModel
{
    public required GameEntityCategory EntityCategory { get; init; }
    public string? Name { get; init; }
    public decimal AvgPriceBuy { get; init; } = 0;
    public decimal AvgPriceSell { get; init; } = 0;
    public bool IsRent { get; init; } = false;
}
