namespace Arkanis.Overlay.Infrastructure.Data.Entities;

using System.Text.Json.Serialization;

public class CategoryEntity : BaseEntity
{
    [JsonPropertyName("type")]
    public string? Type { get; set; } // string(255) // item, service

    [JsonPropertyName("section")]
    public string? Section { get; set; } // string(255) // category group

    [JsonPropertyName("name")]
    public string? Name { get; set; } // string(255) // category name

    [JsonPropertyName("is_game_related")]
    public bool? IsGameRelated { get; set; } // int(1) // if the category exists in-game

    [JsonPropertyName("is_mining")]
    public bool? IsMining { get; set; } // int(1) // if it's mining related category
}
