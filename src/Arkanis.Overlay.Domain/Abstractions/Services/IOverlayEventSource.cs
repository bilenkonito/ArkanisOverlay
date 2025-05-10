namespace Arkanis.Overlay.Domain.Abstractions.Services;

public interface IOverlayEventSource
{
    void OnFocusGained();
    void OnFocusLost();
}
