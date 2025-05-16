namespace Arkanis.Overlay.Host.Desktop.Services;

public class WindowsPreferencesControls
{
    public event EventHandler? PreferencesClosed;

    public ValueTask CloseAsync()
    {
        PreferencesClosed?.Invoke(this, EventArgs.Empty);
        return ValueTask.CompletedTask;
    }
}
