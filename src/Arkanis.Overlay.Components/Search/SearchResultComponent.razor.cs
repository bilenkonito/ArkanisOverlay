namespace Arkanis.Overlay.Components.Search;

using Domain.Models.Search;
using Microsoft.AspNetCore.Components;

public partial class SearchResultComponent : ComponentBase
{
    private static string DEFAULT_LABEL = "<NO NAME>";

    [Parameter]
    public SearchResultModel SearchResult { get; set; }
}
