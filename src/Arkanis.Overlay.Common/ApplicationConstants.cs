namespace Arkanis.Overlay.Common;

public static class ApplicationConstants
{
    public const string GoogleAnalyticsTrackingId = "G-ND6WBR51VP";

    public const string ApplicationName = "Arkanis Overlay";

    public const string LocalAppDataDirectoryName = "ArkanisOverlay";
    public const string GitHubOwner = "ArkanisCorporation";
    public const string GitHubRepository = "ArkanisOverlay";
    public const string GitHubRepositoryUrl = $"https://github.com/{GitHubOwner}/{GitHubRepository}";

    public const string CurrencyName = "Alpha United Earth Credits";
    public const string CurrencyAbbr = "aUEC";
    public const string CurrencySymbol = "\u00A4";

    public static readonly TimeSpan GameTimeOffset = TimeSpan.FromTicks(TimeSpan.TicksPerDay * (long)(365.25 * 930));

    public static readonly string LocalAppDataPath = Path.Join(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        LocalAppDataDirectoryName
    );

    public static readonly DirectoryInfo LocalAppDataDir = Directory.CreateDirectory(LocalAppDataPath);
}
