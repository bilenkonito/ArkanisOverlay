namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Options;

public interface IUserPreferencesProvider
{
    UserPreferences CurrentPreferences { get; }

    event EventHandler<UserPreferences> PreferencesChanged;
}
