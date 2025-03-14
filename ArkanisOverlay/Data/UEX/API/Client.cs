using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using ArkanisOverlay.Data.UEX.API.Converters;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Data.UEX.API;

public class Client
{
    private readonly ILogger<Client> _logger;
    private readonly HttpClient _client;
    private const string _BASE_URL = "https://api.uexcorp.space/2.0/";

    private readonly JsonSerializerOptions _options = new()
    {
        Converters =
        {
            new BoolConverter(),
            // hotfix for UEX float -> string issue
            new StringToNumberConverter<decimal>(),
        }
    };

    public Client(ILogger<Client> logger, HttpClient client)
    {
        _logger = logger;
        _client = client;

        if (!_BASE_URL.EndsWith('/')) throw new Exception("BaseUrl must end with '/'.");
    }

    public async Task<T?> Get<T>(string endpoint)
    {
        try
        {
            var response = await _client.GetFromJsonAsync<ApiResponse<T>>(_BASE_URL + endpoint, _options)
                .ConfigureAwait(false);
            
            if (response is null) throw new Exception("Response is `null`.");

            if (response is { IsSuccess: true })
            {
                return response.Data;
            }

            _logger.LogError(
                "Failed to fetch {Endpoint}. HttpCode: {HttpCode}. Status: {Status}. Message: {Message}.",
                endpoint, response.HttpCode, response.Status, response.Message
            );
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to fetch {Endpoint}. Exception: {Exception}.", endpoint, e);
        }

        return default;
    }
}