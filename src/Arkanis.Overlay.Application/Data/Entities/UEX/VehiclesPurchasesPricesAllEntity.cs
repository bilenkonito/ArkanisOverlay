using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Arkanis.Overlay.Application.Data.Entities.UEX;

public class VehiclesPurchasesPricesAllEntity : BaseEntity
{
    [ForeignKey(nameof(Vehicle))]
    [JsonPropertyName("id_vehicle")] 
    public int IdVehicle { get; set; }
    
    // Navigation property
    [JsonIgnore]
    public virtual VehicleEntity Vehicle { get; set; } = null!;

    [JsonPropertyName("id_terminal")] public int IdTerminal { get; set; }
    [JsonPropertyName("price_buy")] public decimal PriceBuy { get; set; } // last
    [JsonPropertyName("vehicle_name")] public string? VehicleName { get; set; }
    [JsonPropertyName("terminal_name")] public string? TerminalName { get; set; }
}