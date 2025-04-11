using ArkanisOverlay.Data.Contexts;
using ArkanisOverlay.UI.Models.SearchResult;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace ArkanisOverlay.UI.Components.SearchResult;

public partial class SearchResultComponent : ComponentBase
{
    [Parameter] public SearchResultModel SearchResult { get; set; }
    
    private static string DEFAULT_LABEL = "<NO NAME>";
}