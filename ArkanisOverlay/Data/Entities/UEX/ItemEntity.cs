using System.Text.Json.Serialization;

namespace ArkanisOverlay.Data.Entities.UEX;

public class ItemEntity : BaseEntity
{   
    [JsonPropertyName("id_parent")] public int? IdParent { get; set; } // int(11)
    [JsonPropertyName("id_category")] public int? IdCategory { get; set; } // int(11)
    [JsonPropertyName("id_company")] public int? IdCompany { get; set; } // int(11)
    [JsonPropertyName("id_vehicle")] public int? IdVehicle { get; set; } // int(11) // if linked to a vehicle
    [JsonPropertyName("name")] public string? Name { get; set; } // string(255)
    [JsonPropertyName("section")] public string? Section { get; set; } // string(255) // coming from categories
    [JsonPropertyName("category")] public string? Category { get; set; } // string(255) // coming from categories
    [JsonPropertyName("company_name")] public string? CompanyName { get; set; } // string(255) // coming from companies
    [JsonPropertyName("vehicle_name")] public string? VehicleName { get; set; } // string(255) // coming from vehicles
    [JsonPropertyName("slug")] public string? Slug { get; set; } // string(255) // UEX URLs
    [JsonPropertyName("uuid")] public string? Uuid { get; set; } // string(255) // star citizen uuid
    [JsonPropertyName("url_store")] public string? UrlStore { get; set; } // string(255) // pledge store URL

    [JsonPropertyName("is_exclusive_pledge")]
    public bool IsExclusivePledge { get; set; } // int(1)

    [JsonPropertyName("is_exclusive_subscriber")]
    public bool IsExclusiveSubscriber { get; set; } // int(1)

    [JsonPropertyName("is_exclusive_concierge")]
    public bool IsExclusiveConcierge { get; set; } // int(1)
    
    // ! Deprecated or irrelevant
    // [JsonPropertyName("screenshot")]
    // public string Screenshot {get;set;} // string(255) // item image URL (suspended due to server costs)
    // [JsonPropertyName("attributes")]
    // public json attributes {get;set;} // json // deprecated, replaced by items_attributes
    // [JsonPropertyName("notification")]
    // public json notification {get;set;} // json // heads up about an item, such as known bugs, etc.
}