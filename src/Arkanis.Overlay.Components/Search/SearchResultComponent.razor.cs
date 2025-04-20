namespace Arkanis.Overlay.Components.Search;

using Domain.Abstractions.Game;
using Domain.Models.Search;
using Microsoft.AspNetCore.Components;

public partial class SearchResultComponent : ComponentBase
{
    [Parameter]
    public required SearchMatchResult<IGameEntity> SearchResult { get; set; }

    private IGameEntity GameEntity
        => SearchResult.TypedSubject;
}
