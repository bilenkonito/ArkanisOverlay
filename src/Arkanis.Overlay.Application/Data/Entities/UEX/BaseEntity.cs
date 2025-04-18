namespace Arkanis.Overlay.Application.Data.Entities.UEX;

using API.Converters;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("date_added")]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset DateAdded { get; set; } // int(11) // timestamp

    [JsonPropertyName("date_modified")]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset DateModified { get; set; } // int(11) // timestamp
}
