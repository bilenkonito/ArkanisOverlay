namespace Arkanis.Overlay.Infrastructure.Services;

using System.Globalization;
using System.Text.Json;
using Common;
using Common.Converters.Json;
using Domain.Abstractions.Services;
using Domain.Models.Keyboard;
using Domain.Options;
using Microsoft.Extensions.Logging;

public class UserPreferencesJsonFileManager(ILogger<UserPreferencesJsonFileManager> logger) : IUserPreferencesManager
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters =
        {
            new KeyboardShortcut.JsonConverter(),
            new RegionInfoJsonConverter(),
            new CultureInfoJsonConverter(),
        },
    };

    private static FileInfo PreferencesFileInfo
        => new(Path.Combine(ApplicationConstants.LocalAppDataPath, "userPreferences.json"));

    public UserPreferences CurrentPreferences { get; private set; } = new();

    public async Task LoadUserPreferencesAsync()
    {
        if (!PreferencesFileInfo.Exists)
        {
            await SaveAndApplyUserPreferencesAsync(CurrentPreferences).ConfigureAwait(false);
            return;
        }

        UserPreferences? userPreferences = null;
        logger.LogDebug("Loading existing user preferences from {FilePath}", PreferencesFileInfo.FullName);

        try
        {
            await using var fileStream = PreferencesFileInfo.OpenRead();
            userPreferences = await JsonSerializer.DeserializeAsync<UserPreferences>(fileStream, _options).ConfigureAwait(false);
        }
        catch (JsonException exception)
        {
            logger.LogError(exception, "Failed to properly load user preferences from {FilePath}", PreferencesFileInfo.FullName);
        }

        userPreferences ??= CurrentPreferences;
        await ApplyUserPreferencesAsync(userPreferences).ConfigureAwait(false);
    }

    public async Task SaveAndApplyUserPreferencesAsync(UserPreferences userPreferences)
    {
        await ApplyUserPreferencesAsync(userPreferences).ConfigureAwait(false);

        try
        {
            logger.LogDebug("Saving new user preferences to {FilePath}", PreferencesFileInfo.FullName);
            await using var fileStream = PreferencesFileInfo.Open(FileMode.Create, FileAccess.Write, FileShare.None);
            await JsonSerializer.SerializeAsync(fileStream, CurrentPreferences, _options).ConfigureAwait(false);
        }
        catch (JsonException exception)
        {
            logger.LogError(exception, "Failed to properly save user preferences to {FilePath}", PreferencesFileInfo.FullName);
        }
    }

    /// <summary>
    ///     Applies the provided configuration.
    ///     Also stores the provided configuration instance to <see cref="CurrentPreferences" />.
    /// </summary>
    /// <param name="userPreferences">User configuration to apply</param>
    private Task ApplyUserPreferencesAsync(UserPreferences userPreferences)
    {
        logger.LogDebug("Applying new user preferences");
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = userPreferences.ActiveCultureInfo;

        // TODO: Apply configured user preferences

        CurrentPreferences = userPreferences;
        return Task.CompletedTask;
    }
}
