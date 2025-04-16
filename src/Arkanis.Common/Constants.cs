namespace Arkanis.Common;

public static class Constants
{
    public const string InstanceId = "91df1d7c-b7fe-4a0f-91e3-22d94cd50ab5";

    public const string LocalAppDataDirectoryName = "ArkanisOverlay";

    public const string CurrencyName = "Alpha United Earth Credits";
    public const string CurrencyAbbr = "aUEC";

    public static readonly string LocalAppDataPath = Path.Join
    (
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        LocalAppDataDirectoryName
    );

    public static readonly DirectoryInfo LocalAppDataDir = Directory.CreateDirectory(LocalAppDataPath);
}
