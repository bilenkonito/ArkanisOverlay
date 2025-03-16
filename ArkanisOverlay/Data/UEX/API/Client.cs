using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using ArkanisOverlay.Data.UEX.API.Converters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArkanisOverlay.Data.UEX.API;

public class Client
{
    private readonly ILogger<Client> _logger;
    private readonly HttpClient _client;
    private readonly ConfigurationOptions _configurationOptions;
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

    public Client(ILogger<Client> logger, HttpClient client, IOptions<ConfigurationOptions> configurationOptions)
    {
        _logger = logger;
        _client = client;
        _configurationOptions = configurationOptions.Value;
        _baseUrl = _configurationOptions.UexBaseUrl;

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