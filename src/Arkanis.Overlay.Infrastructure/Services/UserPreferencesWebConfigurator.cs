namespace Arkanis.Overlay.Infrastructure.Services;

using System.Globalization;
using Domain.Abstractions.Services;
using Domain.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class UserPreferencesWebConfigurator(
    IUserPreferencesProvider preferencesProvider,
    IOverlayControls overlayControls,
    ILogger<UserPreferencesWebConfigurator> logger
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        preferencesProvider.PreferencesChanged += ApplyUserPreferencesAsync;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        preferencesProvider.PreferencesChanged -= ApplyUserPreferencesAsync;
        return Task.CompletedTask;
    }

    /// <summary>
    ///     Applies the provided configuration in the web environment.
    /// </summary>
    /// <param name="sender">The event sender</param>
    /// <param name="userPreferences">User configuration to apply</param>
    private void ApplyUserPreferencesAsync(object? sender, UserPreferences userPreferences)
    {
        logger.LogDebug("Applying new user preferences");
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = userPreferences.ActiveCultureInfo;
        overlayControls.SetBlurEnabled(userPreferences.BlurBackground);
    }
}
