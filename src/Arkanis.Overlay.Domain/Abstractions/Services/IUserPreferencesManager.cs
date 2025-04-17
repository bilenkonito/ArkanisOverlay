namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Options;

public interface IUserPreferencesManager : IUserPreferencesProvider
{
    Task LoadUserPreferencesAsync();

    Task SaveAndApplyUserPreferencesAsync(UserPreferences userPreferences);
}
