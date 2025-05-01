namespace Arkanis.Overlay.Components.Services;

using Domain.Abstractions.Services;
using Microsoft.Extensions.Logging;

public class GlobalOverlayKeyboardProxy : KeyboardProxy
{
    private readonly IOverlayControls _overlay;

    public GlobalOverlayKeyboardProxy(IOverlayControls overlay, ILogger<GlobalOverlayKeyboardProxy> logger) : base(logger)
    {
        _overlay = overlay;
        _overlay.OverlayShown += OnOverlayVisibilityChanged;
        _overlay.OverlayHidden += OnOverlayVisibilityChanged;
    }

    private void OnOverlayVisibilityChanged(object? sender, EventArgs e)
        => ShortcutBuilder.Clear();

    public override void Dispose()
    {
        _overlay.OverlayShown -= OnOverlayVisibilityChanged;
        _overlay.OverlayHidden -= OnOverlayVisibilityChanged;

        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
