using MudBlazor;

namespace ArkanisOverlay.UI.Models.SearchResult;

public class SearchResultModel
{
    public string? Type { get; set; }

    public readonly string? Name;
    public readonly string Icon = Icons.Material.Outlined.QuestionMark;
    public readonly decimal AvgPriceBuy = 0;
    public readonly decimal AvgPriceSell = 0;
    public readonly bool IsRent = false;
}