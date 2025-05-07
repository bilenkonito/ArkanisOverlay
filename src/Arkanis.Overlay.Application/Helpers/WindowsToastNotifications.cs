namespace Arkanis.Overlay.Application.Helpers;

using Domain.Options;
using Microsoft.Toolkit.Uwp.Notifications;

internal static class WindowsToastNotifications
{
    public static void ShowWelcomeToast(UserPreferences userPreferences)
    {
        var launchShortcut = userPreferences.LaunchShortcut.Description;
        new ToastContentBuilder()
            .SetToastDuration(ToastDuration.Long)
            .SetToastScenario(ToastScenario.Default)
            .AddText("Welcome to the Arkanis Overlay!", AdaptiveTextStyle.Header)
            .AddText("You can find the app in the system tray. By right-clicking on it, you can open the preferences dialog or exit.", AdaptiveTextStyle.Body)
            .AddText($"The overlay can be launched by pressing {launchShortcut} while in the game.", AdaptiveTextStyle.Body)
            .AddAttributionText("Built with ❤️ by Arkanis Corporation\n(FatalMerlin, TheKronnY, and contributors)")
            .Show();
    }
}
