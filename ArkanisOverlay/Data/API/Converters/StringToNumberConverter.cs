using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArkanisOverlay.Data.API.Converters;

public class StringToNumberConverter<T> : JsonConverter<T> where T : struct
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String when decimal.TryParse(reader.GetString(), out var value) =>
                (T)Convert.ChangeType(value, typeof(T)),
            JsonTokenType.Number => (T)Convert.ChangeType(reader.GetDecimal(), typeof(T)),
            _ => throw new JsonException($"Unexpected token {reader.TokenType}")
        };
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }
}