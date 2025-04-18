namespace Arkanis.Overlay.Domain.Abstractions.Services;

public interface IOverlayControls
{
    ValueTask ShowAsync();
    ValueTask HideAsync();
}
