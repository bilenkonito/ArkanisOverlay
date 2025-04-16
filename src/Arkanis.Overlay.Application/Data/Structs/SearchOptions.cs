namespace Arkanis.Overlay.Application.Data.Structs;

public struct SearchOptions
{
    public bool Commodities { get; set; }
    public bool Ships { get; set; }
    public bool Vehicles { get; set; }
    public bool Items { get; set; }
    public bool Locations { get; set; }
}