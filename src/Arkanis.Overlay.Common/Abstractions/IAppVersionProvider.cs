namespace Arkanis.Overlay.Common.Abstractions;

using Models;
using NuGet.Versioning;

public interface IAppVersionProvider
{
    SemanticVersion CurrentVersion { get; }

    UpdateChannel CurrentUpdateChannel
        => UpdateChannel.ByVelopackChannelId(CurrentVelopackChannelId);

    string CurrentVelopackChannelId { get; }

    DateTimeOffset? AutoUpdateCheckAt { get; }
}
