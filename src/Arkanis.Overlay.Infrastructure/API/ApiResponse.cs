namespace Arkanis.Overlay.Infrastructure.API;

using System.Text.Json.Serialization;

public class ApiResponse<T>
{
    [JsonPropertyName("http_code")]
    public int HttpCode { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("data")]
    public T? Data { get; set; } // Data is always present in success case

    public bool IsSuccess
        => HttpCode == 200;
}

// public abstract class SuccessResponse<T> : ApiResponse<T>
// {
//     [JsonPropertyName("data")] public new required T Data { get; set; } // Data is always present in success case
// }
//
// public abstract class ErrorResponse<T> : ApiResponse<T>
// {
//     [JsonPropertyName("data")] public new T? Data => default; // Data is always present in success case
// }
