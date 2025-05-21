namespace Arkanis.Overlay.External.UEX;

using System.Net.Http.Headers;

internal abstract class UexApiClientBase
{
    private readonly IHttpClientFactory? _httpClientFactory;
    private readonly UexApiOptions _options;

    protected UexApiClientBase()
    {
    }

    protected UexApiClientBase(IHttpClientFactory httpClientFactory, UexApiOptions? options = null) : this()
    {
        _httpClientFactory = httpClientFactory;
        _options = options ?? new UexApiOptions();
    }

    public ValueTask<HttpClient> CreateHttpClientAsync(CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory?.CreateClient(GetType().Name);
        httpClient ??= new HttpClient();

        if (_options.ApplicationToken is { } applicationToken)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", applicationToken);
        }

        return ValueTask.FromResult(httpClient);
    }
}
