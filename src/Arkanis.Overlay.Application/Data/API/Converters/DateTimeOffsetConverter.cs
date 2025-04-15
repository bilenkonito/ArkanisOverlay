using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arkanis.Overlay.Application.Data.API.Converters;

public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.TryGetInt64(out var value) ? DateTimeOffset.FromUnixTimeSeconds(value) : DateTimeOffset.MinValue;

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }
}