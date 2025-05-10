namespace Arkanis.Overlay.Domain.Abstractions.Services;

public interface IOverlayEventProvider
{
    event EventHandler OverlayShown;
    event EventHandler OverlayHidden;

    event EventHandler OverlayFocused;
    event EventHandler OverlayBlurred;
}
