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
    [JsonPropertyName("scu")] public float Scu { get; init; } // int(11)
    [JsonPropertyName("crew")] public string? Crew { get; init; } // string?(10) // comma separated
    [JsonPropertyName("mass")] public float Mass { get; init; } // int(11)
    [JsonPropertyName("width")] public float Width { get; init; } // int(11)
    [JsonPropertyName("height")] public float Height { get; init; } // int(11)
    [JsonPropertyName("length")] public float Length { get; init; } // int(11)
    [JsonPropertyName("fuel_quantum")] public float FuelQuantum { get; init; } // int(11) // SCU
    [JsonPropertyName("fuel_hydrogen")] public float FuelHydrogen { get; init; } // int(11) // SCU
    [JsonPropertyName("container_sizes")] public string? ContainerSizes { get; init; } // string? // SCU, comma separated
    [JsonPropertyName("is_addon")] public bool IsAddon { get; init; } // int(1) // e.g. RSI Galaxy Refinery Module
    [JsonPropertyName("is_boarding")] public bool IsBoarding { get; init; } // int(1)
    [JsonPropertyName("is_bomber")] public bool IsBomber { get; init; } // int(1)
    [JsonPropertyName("is_cargo")] public bool IsCargo { get; init; } // int(1)
    [JsonPropertyName("is_carrier")] public bool IsCarrier { get; init; } // int(1)
    [JsonPropertyName("is_civilian")] public bool IsCivilian { get; init; } // int(1)
    [JsonPropertyName("is_concept")] public bool IsConcept { get; init; } // int(1)
    [JsonPropertyName("is_construction")] public bool IsConstruction { get; init; } // int(1)
    [JsonPropertyName("is_datarunner")] public bool IsDatarunner { get; init; } // int(1)
    [JsonPropertyName("is_docking")] public bool IsDocking { get; init; } // int(1) // contains docking port
    [JsonPropertyName("is_emp")] public bool IsEmp { get; init; } // int(1)
    [JsonPropertyName("is_exploration")] public bool IsExploration { get; init; } // int(1)

    [JsonPropertyName("is_ground_vehicle")]
    public bool IsGroundVehicle { get; init; } // int(1)

    [JsonPropertyName("is_hangar")] public bool IsHangar { get; init; } // int(1) // contains hangar
    [JsonPropertyName("is_industrial")] public bool IsIndustrial { get; init; } // int(1)
    [JsonPropertyName("is_interdiction")] public bool IsInterdiction { get; init; } // int(1)

    [JsonPropertyName("is_loading_dock")]
    public bool IsLoadingDock { get; init; } // int(1) // cargo can be loaded/unloaded via docking

    [JsonPropertyName("is_medical")] public bool IsMedical { get; init; } // int(1)
    [JsonPropertyName("is_military")] public bool IsMilitary { get; init; } // int(1)
    [JsonPropertyName("is_mining")] public bool IsMining { get; init; } // int(1)
    [JsonPropertyName("is_passenger")] public bool IsPassenger { get; init; } // int(1)
    [JsonPropertyName("is_qed")] public bool IsQed { get; init; } // int(1)
    [JsonPropertyName("is_racing")] public bool IsRacing { get; init; } // int(1)
    [JsonPropertyName("is_refinery")] public bool IsRefinery { get; init; } // int(1)
    [JsonPropertyName("is_refuel")] public bool IsRefuel { get; init; } // int(1)
    [JsonPropertyName("is_repair")] public bool IsRepair { get; init; } // int(1)
    [JsonPropertyName("is_research")] public bool IsResearch { get; init; } // int(1)
    [JsonPropertyName("is_salvage")] public bool IsSalvage { get; init; } // int(1)
    [JsonPropertyName("is_scanning")] public bool IsScanning { get; init; } // int(1)
    [JsonPropertyName("is_science")] public bool IsScience { get; init; } // int(1)

    [JsonPropertyName("is_showdown_winner")]
    public bool IsShowdownWinner { get; init; } // int(1)

    [JsonPropertyName("is_spaceship")] public bool IsSpaceship { get; init; } // int(1)
    [JsonPropertyName("is_starter")] public bool IsStarter { get; init; } // int(1)
    [JsonPropertyName("is_stealth")] public bool IsStealth { get; init; } // int(1)
    [JsonPropertyName("is_tractor_beam")] public bool IsTractorBeam { get; init; } // int(1)
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