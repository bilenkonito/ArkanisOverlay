namespace Arkanis.Overlay.Infrastructure.Services;

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

    public TimeSpan CacheTtl { get; } = TimeSpan.Parse(cacheTtl);
    public Timer? Timer { get; set; }
}
