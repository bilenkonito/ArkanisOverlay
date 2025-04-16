namespace Arkanis.Overlay.Infrastructure.API.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

public class StringConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.String => reader.GetString(),
            // Workaround for `crew` being returned as int right now
            JsonTokenType.Number => reader.GetInt16().ToString(),
            _ => "",
        };

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        => throw new NotSupportedException();
}
