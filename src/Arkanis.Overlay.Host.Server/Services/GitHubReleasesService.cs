namespace Arkanis.Overlay.Host.Server.Services;

using Common;
using Microsoft.Extensions.Caching.Memory;
using Octokit;
using Octokit.Internal;

internal class GitHubReleasesService(IMemoryCache memoryCache)
{
    private readonly GitHubClient _client = new(
        new ProductHeaderValue(ServerHostModule.Namespace),
        new InMemoryCredentialStore(new Credentials(ApplicationConstants.GitHubReleaseToken))
    );

    public async Task<Downloads> GetLatestStableDownloads()
    {
        var releases = await GetReleases();
        if (releases.FirstOrDefault(x => x.Prerelease == false) is not { } latestRelease)
        {
            latestRelease = releases.FirstOrDefault();
            if (latestRelease is null)
            {
                return DownloadsMissing.Instance;
            }
        }

        if (await GetPortableDownloadAsset(latestRelease) is not { } portableAsset
            || await GetInstallerDownloadAsset(latestRelease) is not { } installerAsset)
        {
            return DownloadsMissing.Instance;
        }

        return new ReleaseDownloads
        {
            Release = latestRelease,
            PortableAsset = portableAsset,
            InstallerAsset = installerAsset,
        };
    }

    private async Task<ReleaseAsset?> GetPortableDownloadAsset(Release release)
    {
        var assets = await GetReleaseAssets(release);
        return assets.FirstOrDefault(asset => asset.Name.EndsWith("-Portable.zip", StringComparison.Ordinal));
    }

    private async Task<ReleaseAsset?> GetInstallerDownloadAsset(Release release)
    {
        var assets = await GetReleaseAssets(release);
        return assets.FirstOrDefault(asset => asset.Name.EndsWith("-Setup.exe", StringComparison.Ordinal));
    }

    private async Task<IReadOnlyList<ReleaseAsset>> GetReleaseAssets(Release release)
        => await memoryCache.GetOrCreateAsync(
               $"{nameof(GitHubReleasesService)}-{nameof(GetReleaseAssets)}",
               async entry =>
               {
                   entry.SetSlidingExpiration(TimeSpan.FromHours(1));
                   return await _client.Repository.Release.GetAllAssets(ApplicationConstants.GitHubOwner, ApplicationConstants.GitHubRepository, release.Id);
               }
           )
           ?? [];

    private async Task<IReadOnlyList<Release>> GetReleases()
        => await memoryCache.GetOrCreateAsync(
               $"{nameof(GitHubReleasesService)}-{nameof(GetLatestStableDownloads)}",
               async entry =>
               {
                   entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
                   return await _client.Repository.Release.GetAll(ApplicationConstants.GitHubOwner, ApplicationConstants.GitHubRepository);
               }
           )
           ?? [];
}

public class Downloads;

public class DownloadsMissing : Downloads
{
    private DownloadsMissing()
    {
    }

    public static Downloads Instance { get; } = new DownloadsMissing();
}

public class ReleaseDownloads : Downloads
{
    public required Release Release { get; init; }
    public required ReleaseAsset PortableAsset { get; init; }
    public required ReleaseAsset InstallerAsset { get; init; }
}
