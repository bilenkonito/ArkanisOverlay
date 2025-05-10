namespace Arkanis.Overlay.Domain.Abstractions.Services;

public interface IOverlayControls
{
    event EventHandler OverlayShown;
    event EventHandler OverlayHidden;

    event EventHandler OverlayFocused;
    event EventHandler OverlayBlurred;

    ValueTask ShowAsync();
    ValueTask HideAsync();

    void SetBlurEnabled(bool isEnabled);
}
