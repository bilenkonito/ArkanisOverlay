namespace Arkanis.Overlay.Application.Services;

using Domain.Abstractions.Services;
using Helpers;
using UI.Windows;

public class WindowsOverlayControls(BlurHelper blurHelper) : IOverlayControls
{
    public ValueTask ShowAsync()
    {
        OverlayWindow.Instance?.Show();
        return ValueTask.CompletedTask;
    }

    public ValueTask HideAsync()
    {
        OverlayWindow.Instance?.Hide();
        return ValueTask.CompletedTask;
    }

    public void SetBlurEnabled(bool isEnabled)
    {
    }
}
