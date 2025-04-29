namespace Arkanis.Overlay.Application.Services;

using System.Reflection;
using Domain.Abstractions.Services;
using Domain.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

public class WindowsAutoStartManager(
    IUserPreferencesProvider userPreferencesProvider,
    ILogger<WindowsAutoStartManager> logger
) : IHostedService
{
    /// <summary>
    ///     Registry key path for the application to set itself to run at startup for the current user.
    /// </summary>
    public const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Run";

    /// <summary>
    ///     The key for the application in the registry.
    /// </summary>
    public const string RegistryKeyName = Constants.ApplicationName;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        userPreferencesProvider.ApplyPreferences += OnUserApplyPreferences;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        userPreferencesProvider.ApplyPreferences -= OnUserApplyPreferences;
        return Task.CompletedTask;
    }

    private static void EnableAutoStart()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true);
        if (key == null)
        {
            throw new InvalidOperationException("Failed to open registry key for auto-start.");
        }

        var exePath = Assembly.GetExecutingAssembly().Location;
        key.SetValue(RegistryKeyName, $"\"{exePath}\"");
    }

    private static void DisableAutoStart()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath, true);

        if (key?.GetValue(RegistryKeyName) != null)
        {
            key.DeleteValue(RegistryKeyName);
        }
    }

    private void OnUserApplyPreferences(object? sender, UserPreferences userPreferences)
    {
        try
        {
            if (userPreferences.AutoStartWithBoot)
            {
                logger.LogInformation($"Enabling {Constants.ApplicationName} auto-start");
                EnableAutoStart();
            }
            else
            {
                logger.LogInformation($"Disabling {Constants.ApplicationName} auto-start");
                DisableAutoStart();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to register program auto-start");
        }
    }
}
