namespace Arkanis.Overlay.Application.UI.Components.SearchResult;

using Models.SearchResult;

public partial class SearchResultComponent : ComponentBase
{
    private static string DEFAULT_LABEL = "<NO NAME>";

    [Parameter]
    public SearchResultModel SearchResult { get; set; }
}
