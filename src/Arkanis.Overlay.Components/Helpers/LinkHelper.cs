namespace Arkanis.Overlay.Components.Helpers;

public static class LinkHelper
{
    private static readonly string AssemblyName = typeof(LinkHelper).Assembly.GetName().Name ?? string.Empty;

    public static string GetPathToAsset(string relativeAssetPath)
        => $"_content/{AssemblyName}/assets/{relativeAssetPath}";
}
