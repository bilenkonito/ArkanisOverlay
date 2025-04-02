using System.Text.Json.Serialization;

namespace ArkanisOverlay.Data.Entities.UEX;

public class VehiclesRentalsPricesAllEntity : BaseEntity
{
    [JsonPropertyName("id_vehicle")] public int IdVehicle { get; set; }
    [JsonPropertyName("id_terminal")] public int IdTerminal { get; set; }
    [JsonPropertyName("price_rent")] public float PriceRent { get; set; } // last
    [JsonPropertyName("vehicle_name")] public string VehicleName { get; set; }
    [JsonPropertyName("terminal_name")] public string TerminalName { get; set; }
}