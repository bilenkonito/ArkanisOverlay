namespace Arkanis.Overlay.Domain.Options;

using System.Globalization;
using System.Text.Json.Serialization;
using Models.Keyboard;

public record UserPreferences
{
    [JsonIgnore]
    public CultureInfo ActiveCultureInfo
        => CustomRegionInfo is not null
            ? CultureInfo.GetCultureInfo($"{ActiveCulture.TwoLetterISOLanguageName}-{CustomRegionInfo.TwoLetterISORegionName}")
            : ActiveCulture;

    [JsonIgnore]
    private CultureInfo ActiveCulture
        => CustomCultureInfo ?? CultureInfo.CurrentCulture;

    public bool AutoStartWithBoot { get; set; }

    public bool TerminateOnGameExit { get; set; }

    public bool BlurBackground { get; set; }

    public CultureInfo? CustomCultureInfo { get; set; }

    public RegionInfo? CustomRegionInfo { get; set; }

    public KeyboardShortcut LaunchShortcut { get; set; } = KeyboardShortcut.None;
}
