namespace Arkanis.Overlay.Domain.Options;

using System.Globalization;
using Common.Abstractions;
using Models.Keyboard;

public class UserOptions : ISelfBindableOptions
{
    public CultureInfo ActiveCultureInfo
        => CustomRegionInfo is not null
            ? CultureInfo.GetCultureInfo($"{ActiveCulture.TwoLetterISOLanguageName}-{CustomRegionInfo.TwoLetterISORegionName}")
            : ActiveCulture;

    private CultureInfo ActiveCulture
        => CustomCultureInfo ?? CultureInfo.CurrentCulture;

    public bool AutoStartWithBoot { get; set; }

    public bool TerminateOnGameExit { get; set; }

    public CultureInfo? CustomCultureInfo { get; set; }

    public RegionInfo? CustomRegionInfo { get; set; }

    public KeyboardShortcut LaunchShortcut { get; set; } = KeyboardShortcut.None;

    public string SectionPath
        => "User";
}
