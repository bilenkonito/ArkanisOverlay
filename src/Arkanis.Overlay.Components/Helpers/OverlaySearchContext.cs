namespace Arkanis.Overlay.Components.Helpers;

using Domain.Abstractions.Game;
using Microsoft.AspNetCore.Components;

public class OverlaySearchContext
{
    public IGameLocation? CurrentLocation { get; set; }

    public EventCallback<OverlaySearchContext> ConfigChanged { get; init; }

    public async Task SetFilterAsync(IGameLocation location)
    {
        CurrentLocation = location;
        await ConfigChanged.InvokeAsync(this);
    }

    public async Task ClearLocationFilterAsync()
    {
        CurrentLocation = null;
        await ConfigChanged.InvokeAsync(this);
    }
}
