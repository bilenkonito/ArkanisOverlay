namespace Arkanis.Overlay.Domain.Abstractions.Services;

public interface IOverlayEventControls
{
    void OnFocusGained();
    void OnFocusLost();
}
