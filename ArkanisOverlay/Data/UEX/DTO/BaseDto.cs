using System.Text.Json.Serialization;
using ArkanisOverlay.Data.UEX.API.Converters;

namespace ArkanisOverlay.Data.UEX.DTO;

public abstract class BaseDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("date_added")]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset DateAdded { get; set; } // int(11) // timestamp

    [JsonPropertyName("date_modified")]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset DateModified { get; set; } // int(11) // timestamp
}