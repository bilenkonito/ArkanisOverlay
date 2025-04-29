namespace Arkanis.Overlay.Infrastructure.Services;

using Abstractions;
using Domain.Abstractions.Services;
using Domain.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
///     Provides a mechanism to add up-to-date value for app auto-start state configuration property.
/// </summary>
public class AutoStartUserPreferencesUpdater(
    IUserPreferencesManager userPreferencesManager,
    ISystemAutoStartStateProvider autoStartStateProvider,
    ILogger<AutoStartUserPreferencesUpdater> logger
) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogDebug("Registering {ServiceName} to modify user preferences", nameof(AutoStartUserPreferencesUpdater));
        userPreferencesManager.UpdatePreferences += OnUpdatePreferences;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        userPreferencesManager.UpdatePreferences -= OnUpdatePreferences;
        return Task.CompletedTask;
    }

    private void OnUpdatePreferences(object? sender, UserPreferences currentPreferences)
    {
        var newValue = autoStartStateProvider.IsAutoStartEnabled();
        logger.LogDebug(
            "Updating value for {PreferenceKey}: {CurrentValue} -> {NewValue}",
            nameof(currentPreferences.AutoStartWithBoot),
            currentPreferences.AutoStartWithBoot,
            newValue
        );
        currentPreferences.AutoStartWithBoot = newValue;
    }
}
