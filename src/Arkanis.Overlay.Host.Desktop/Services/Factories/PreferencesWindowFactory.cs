namespace Arkanis.Overlay.Host.Desktop.Services.Factories;

using Microsoft.Extensions.DependencyInjection;
using UI.Windows;

public class PreferencesWindowFactory(IServiceProvider serviceProvider)
{
    public PreferencesWindow CreateWindow()
        => ActivatorUtilities.CreateInstance<PreferencesWindow>(serviceProvider);
}
