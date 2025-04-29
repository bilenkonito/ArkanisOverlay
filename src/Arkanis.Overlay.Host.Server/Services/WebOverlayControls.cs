namespace Arkanis.Overlay.Host.Server.Services;

using System.Globalization;
using Domain.Abstractions.Services;
using Domain.Options;
using MudBlazor;

public sealed class WebOverlayControls : IOverlayControls, IDisposable
{
    private readonly IUserPreferencesProvider _preferencesProvider;
    private readonly ISnackbar _snackbar;

    public WebOverlayControls(ISnackbar snackbar, IUserPreferencesProvider preferencesProvider)
    {
        _snackbar = snackbar;
        _preferencesProvider = preferencesProvider;
        preferencesProvider.PreferencesChanged += ApplyUserPreferencesAsync;
    }

    public void Dispose()
        => _preferencesProvider.PreferencesChanged -= ApplyUserPreferencesAsync;

    public ValueTask ShowAsync()
    {
        _snackbar.Add(
            "This would open the overlay",
            configure: options =>
            {
                options.ShowCloseIcon = false;
            }
        );
        return ValueTask.CompletedTask;
    }

    public ValueTask HideAsync()
    {
        _snackbar.Add(
            "This would close the overlay",
            configure: options =>
            {
                options.ShowCloseIcon = false;
            }
        );
        return ValueTask.CompletedTask;
    }

    public void SetBlurEnabled(bool isEnabled)
    {
    }

    private void ApplyUserPreferencesAsync(object? sender, UserPreferences userPreferences)
    {
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = userPreferences.ActiveCultureInfo;
        SetBlurEnabled(userPreferences.BlurBackground);
    }
}
