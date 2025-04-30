namespace Arkanis.Overlay.Domain.Abstractions.Services;

public interface IOverlayControls
{
    event EventHandler OverlayShown;
    event EventHandler OverlayHidden;

    ValueTask ShowAsync();
    ValueTask HideAsync();

    void SetBlurEnabled(bool isEnabled);
}
