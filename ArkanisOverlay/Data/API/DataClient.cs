using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using ArkanisOverlay.Data.API.Converters;
using ArkanisOverlay.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArkanisOverlay.Data.API;

public class DataClient
{
    private readonly ILogger<DataClient> _logger;
    private readonly HttpClient _client;
    private readonly string _baseUrl;

    private readonly JsonSerializerOptions _options = new()
    {
        Converters =
        {
            new BoolConverter(),
            // hotfix for UEX float -> string issue
            new StringToNumberConverter<decimal>(),
        }
    };

    public DataClient(ILogger<DataClient> logger, HttpClient client, IOptions<ConfigurationOptions> configurationOptions)
    {
        _logger = logger;
        _client = client;
        _baseUrl = configurationOptions.Value.UexBaseUrl;

        if (!_baseUrl.EndsWith('/')) throw new Exception("BaseUrl must end with '/'.");
    }

    public async Task<T?> Get<T>(string endpoint)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<ApiResponse<T>>($"{_baseUrl}{endpoint}", _options)
                .ConfigureAwait(false);
            
            switch (response)
            {
                case null:
                    throw new Exception("Response is `null`.");
                case { IsSuccess: true }:
                    return response.Data;
                default:
                    _logger.LogError(
                        "Failed to fetch {Endpoint}. HttpCode: {HttpCode}. Status: {Status}. Message: {Message}.",
                        endpoint, response.HttpCode, response.Status, response.Message
                    );
                    break;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to fetch {Endpoint}. Exception: {Exception}.", endpoint, e);
        }

        return default;
    }
}