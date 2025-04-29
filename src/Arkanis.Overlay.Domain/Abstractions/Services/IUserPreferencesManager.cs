namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Options;

public interface IUserPreferencesManager : IUserPreferencesProvider
{
    UserPreferences CurrentPreferences { get; }

    Task LoadUserPreferencesAsync();

    Task SaveAndApplyUserPreferencesAsync(UserPreferences userPreferences);
}
