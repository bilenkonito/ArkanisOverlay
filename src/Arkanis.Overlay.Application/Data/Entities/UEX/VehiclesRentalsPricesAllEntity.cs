namespace Arkanis.Overlay.Application.Data.Entities.UEX;

public class VehiclesRentalsPricesAllEntity : BaseEntity
{
    [ForeignKey(nameof(Vehicle))]
    [JsonPropertyName("id_vehicle")]
    public int IdVehicle { get; set; }

    // Navigation property
    [JsonIgnore]
    public virtual VehicleEntity Vehicle { get; set; } = null!;

    [JsonPropertyName("id_terminal")]
    public int IdTerminal { get; set; }

    [JsonPropertyName("price_rent")]
    public decimal PriceRent { get; set; } // last

    [JsonPropertyName("vehicle_name")]
    public string VehicleName { get; set; }

    [JsonPropertyName("terminal_name")]
    public string TerminalName { get; set; }
}
