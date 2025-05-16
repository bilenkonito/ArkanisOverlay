namespace Arkanis.Overlay.Common.Models;

public record UpdateChannel(string Name, string InternalId, string? VelopackChannelId)
{
    public static readonly UpdateChannel Default = new("Default", "default", null);
    public static readonly UpdateChannel Stable = new("Stable", "stable", "stable");

    public static readonly UpdateChannel ReleaseCandidate = new("Release Candidate", "rc", "rc")
    {
        IsUnstable = true,
    };

    public static readonly UpdateChannel Nightly = new("Nightly", "nightly", "nightly")
    {
        IsUnstable = true,
    };

    public bool IsUnstable { get; init; }
    public string? Description { get; init; }

    public static IEnumerable<UpdateChannel> All
        =>
        [
            Default,
            Stable,
            ReleaseCandidate,
            Nightly,
        ];

    public static UpdateChannel ById(string? id)
        => All.SingleOrDefault(x => x.InternalId == id, Default);
}
