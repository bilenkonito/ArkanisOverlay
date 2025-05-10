namespace Arkanis.Overlay.Common.Abstractions;

using NuGet.Versioning;

public interface IAppVersionProvider
{
    SemanticVersion CurrentVersion { get; }

    string CurrentChannel { get; }
}
