namespace Arkanis.Overlay.Infrastructure.Services;

using System.Globalization;

internal class EndpointConfig(
    string apiPath,
    string cacheTtl,
    Func<string, IEnumerable<string>>? mapper = null
)
{
    public readonly List<Type> Dependents =
    [
    ];

    public IEnumerable<string> ApiPaths
        => mapper == null
            ?
            [
                apiPath,
            ]
            : mapper(apiPath);

    public string ApiPath
        => apiPath;

    public TimeSpan CacheTtl { get; } = TimeSpan.Parse(cacheTtl, CultureInfo.InvariantCulture);
    public Timer? Timer { get; set; }
}
