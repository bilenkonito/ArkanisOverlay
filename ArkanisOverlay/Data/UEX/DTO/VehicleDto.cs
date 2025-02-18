using System.Text.Json.Serialization;
using ArkanisOverlay.Data.UEX.API.Converters;

namespace ArkanisOverlay.Data.UEX.DTO;

public class VehicleDto : BaseDto
{
    [JsonPropertyName("id_company")] public int IdCompany { get; init; } // int(11) // vehicle manufacturer
    [JsonPropertyName("id_parent")] public int IdParent { get; init; } // int(11) // parent ship series

    [JsonPropertyName("ids_vehicles_loaners")]
    public string? IdsVehiclesLoaners { get; init; } // string?(40) // vehicles loaned, comma separated

    [JsonPropertyName("name")] public string? Name { get; init; } // string?(255)
    [JsonPropertyName("name_full")] public string? NameFull { get; init; } // string?(255)
    [JsonPropertyName("slug")] public string? Slug { get; init; } // string?(255)
    [JsonPropertyName("uuid")] public string? Uuid { get; init; } // string?(255) // star citizen uuid
    [JsonPropertyName("scu")] public int Scu { get; init; } // int(11)
    [JsonPropertyName("crew")] public string? Crew { get; init; } // string?(10) // comma separated
    [JsonPropertyName("mass")] public int Mass { get; init; } // int(11)
    [JsonPropertyName("width")] public int Width { get; init; } // int(11)
    [JsonPropertyName("height")] public int Height { get; init; } // int(11)
    [JsonPropertyName("length")] public int Length { get; init; } // int(11)
    [JsonPropertyName("fuel_quantum")] public int FuelQuantum { get; init; } // int(11) // SCU
    [JsonPropertyName("fuel_hydrogen")] public int FuelHydrogen { get; init; } // int(11) // SCU
    [JsonPropertyName("container_sizes")] public string? ContainerSizes { get; init; } // string? // SCU, comma separated
    [JsonPropertyName("is_addon")] public int IsAddon { get; init; } // int(1) // e.g. RSI Galaxy Refinery Module
    [JsonPropertyName("is_boarding")] public int IsBoarding { get; init; } // int(1)
    [JsonPropertyName("is_bomber")] public int IsBomber { get; init; } // int(1)
    [JsonPropertyName("is_cargo")] public int IsCargo { get; init; } // int(1)
    [JsonPropertyName("is_carrier")] public int IsCarrier { get; init; } // int(1)
    [JsonPropertyName("is_civilian")] public int IsCivilian { get; init; } // int(1)
    [JsonPropertyName("is_concept")] public int IsConcept { get; init; } // int(1)
    [JsonPropertyName("is_construction")] public int IsConstruction { get; init; } // int(1)
    [JsonPropertyName("is_datarunner")] public int IsDatarunner { get; init; } // int(1)
    [JsonPropertyName("is_docking")] public int IsDocking { get; init; } // int(1) // contains docking port
    [JsonPropertyName("is_emp")] public int IsEmp { get; init; } // int(1)
    [JsonPropertyName("is_exploration")] public int IsExploration { get; init; } // int(1)

    [JsonPropertyName("is_ground_vehicle")]
    public int IsGroundVehicle { get; init; } // int(1)

    [JsonPropertyName("is_hangar")] public int IsHangar { get; init; } // int(1) // contains hangar
    [JsonPropertyName("is_industrial")] public int IsIndustrial { get; init; } // int(1)
    [JsonPropertyName("is_interdiction")] public int IsInterdiction { get; init; } // int(1)

    [JsonPropertyName("is_loading_dock")]
    public int IsLoadingDock { get; init; } // int(1) // cargo can be loaded/unloaded via docking

    [JsonPropertyName("is_medical")] public int IsMedical { get; init; } // int(1)
    [JsonPropertyName("is_military")] public int IsMilitary { get; init; } // int(1)
    [JsonPropertyName("is_mining")] public int IsMining { get; init; } // int(1)
    [JsonPropertyName("is_passenger")] public int IsPassenger { get; init; } // int(1)
    [JsonPropertyName("is_qed")] public int IsQed { get; init; } // int(1)
    [JsonPropertyName("is_racing")] public int IsRacing { get; init; } // int(1)
    [JsonPropertyName("is_refinery")] public int IsRefinery { get; init; } // int(1)
    [JsonPropertyName("is_refuel")] public int IsRefuel { get; init; } // int(1)
    [JsonPropertyName("is_repair")] public int IsRepair { get; init; } // int(1)
    [JsonPropertyName("is_research")] public int IsResearch { get; init; } // int(1)
    [JsonPropertyName("is_salvage")] public int IsSalvage { get; init; } // int(1)
    [JsonPropertyName("is_scanning")] public int IsScanning { get; init; } // int(1)
    [JsonPropertyName("is_science")] public int IsScience { get; init; } // int(1)

    [JsonPropertyName("is_showdown_winner")]
    public int IsShowdownWinner { get; init; } // int(1)

    [JsonPropertyName("is_spaceship")] public int IsSpaceship { get; init; } // int(1)
    [JsonPropertyName("is_starter")] public int IsStarter { get; init; } // int(1)
    [JsonPropertyName("is_stealth")] public int IsStealth { get; init; } // int(1)
    [JsonPropertyName("is_tractor_beam")] public int IsTractorBeam { get; init; } // int(1)
    [JsonPropertyName("url_store")] public string? UrlStore { get; init; } // string?(255)
    [JsonPropertyName("url_brochure")] public string? UrlBrochure { get; init; } // string?(255)
    [JsonPropertyName("url_hotsite")] public string? UrlHotsite { get; init; } // string?(255)
    [JsonPropertyName("url_video")] public string? UrlVideo { get; init; } // string?(255)

    [JsonPropertyName("url_photos")]
    [JsonConverter(typeof(ArrayConverter))]
    public List<string?>?
        UrlPhotos // ! WRONG ! // List<string?> // array(65535) // sourced from RSI website, not currently updated
    {
        get;
        init;
    }

    [JsonPropertyName("pad_type")] public string? PadType { get; init; } // string?(255) // XS, S, M, L, XL

    [JsonPropertyName("game_version")]
    public string? GameVersion { get; init; } // string?(255) // version it was announced or updated

    [JsonPropertyName("company_name")] public string? CompanyName { get; init; } // string?(255) // manufacturer name
}