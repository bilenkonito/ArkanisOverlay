namespace Arkanis.Overlay.Application.Data.API.Converters;

public class ArrayConverter : JsonConverter<List<string>>
{
    public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var result = reader.TokenType switch
        {
            JsonTokenType.String => JsonSerializer.Deserialize<List<string>>(reader.GetString() ?? "[]", options),
            JsonTokenType.StartArray => JsonSerializer.Deserialize<List<string>>(ref reader, options)!,
            JsonTokenType.Null => null,
            _ => throw new JsonException(),
        };

        return result
               ??
               [
               ];
    }

    public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        => throw new NotSupportedException();
}
