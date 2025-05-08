namespace Arkanis.Overlay.Host.Desktop.Services;

using Domain.Abstractions.Services;
using UI.Windows;

public class WindowsOverlayControls : IOverlayControls
{
    public event EventHandler? OverlayShown;
    public event EventHandler? OverlayHidden;

    public ValueTask ShowAsync()
    {
        OverlayWindow.Instance?.Show();
        OverlayShown?.Invoke(this, EventArgs.Empty);
        return ValueTask.CompletedTask;
    }

    public ValueTask HideAsync()
    {
        OverlayWindow.Instance?.Hide();
        OverlayHidden?.Invoke(this, EventArgs.Empty);
        return ValueTask.CompletedTask;
    }

    public void SetBlurEnabled(bool isEnabled)
    {
    }
}
