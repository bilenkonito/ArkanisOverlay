namespace Arkanis.Overlay.Domain.Abstractions.Services;

public interface IOverlayControls
{
    ValueTask ShowAsync();
    ValueTask HideAsync();

    void SetBlurEnabled(bool isEnabled);

    void Shutdown();
}
