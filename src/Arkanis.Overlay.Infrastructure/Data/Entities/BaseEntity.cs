namespace Arkanis.Overlay.Infrastructure.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
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
