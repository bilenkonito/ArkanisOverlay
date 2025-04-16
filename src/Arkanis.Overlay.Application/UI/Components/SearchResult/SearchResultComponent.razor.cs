using Arkanis.Overlay.Application.UI.Models.SearchResult;
using Microsoft.AspNetCore.Components;

namespace Arkanis.Overlay.Application.UI.Components.SearchResult;

public partial class SearchResultComponent : ComponentBase
{
    [Parameter] public SearchResultModel SearchResult { get; set; }
    
    private static string DEFAULT_LABEL = "<NO NAME>";
}