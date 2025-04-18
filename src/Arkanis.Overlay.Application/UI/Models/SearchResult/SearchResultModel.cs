namespace Arkanis.Overlay.Application.UI.Models.SearchResult;

public class SearchResultModel
{
    public readonly decimal AvgPriceBuy = 0;
    public readonly decimal AvgPriceSell = 0;
    public readonly string Icon = Icons.Material.Outlined.QuestionMark;
    public readonly bool IsRent = false;

    public readonly string? Name;
    public string? Type { get; set; }
}
