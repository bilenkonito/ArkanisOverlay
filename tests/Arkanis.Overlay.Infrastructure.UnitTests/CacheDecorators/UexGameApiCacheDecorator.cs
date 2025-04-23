namespace Arkanis.Overlay.Infrastructure.UnitTests.CacheDecorators;

using External.UEX.Abstractions;
using Microsoft.Extensions.Logging;

public sealed class UexGameApiCacheDecorator(IUexGameApi gameApi, ILogger<UexGameApiCacheDecorator> logger) : ServiceCacheDecorator(logger), IUexGameApi
{
    public Task<UexApiResponse<GetCategoriesOkResponse>> GetCategoriesAsync(string? type = null, CancellationToken cancellationToken = default)
        => gameApi.GetCategoriesAsync(type, cancellationToken);

    public Task<UexApiResponse<GetCategoriesAttributesOkResponse>> GetCategoriesAttributesAsync(
        double? id_category = null,
        CancellationToken cancellationToken = default
    )
        => gameApi.GetCategoriesAttributesAsync(id_category, cancellationToken);

    public Task<UexApiResponse<GetCitiesOkResponse>> GetCitiesAsync(double? id_moon = null, CancellationToken cancellationToken = default)
        => gameApi.GetCitiesAsync(id_moon, cancellationToken);

    public Task<UexApiResponse<GetCompaniesOkResponse>> GetCompaniesAsync(string? is_item_manufacturer = null, CancellationToken cancellationToken = default)
        => CacheAsync(
            new
            {
                is_item_manufacturer,
            },
            nameof(GetCompaniesAsync),
            x => gameApi.GetCompaniesAsync(x.is_item_manufacturer, cancellationToken)
        );

    public Task<UexApiResponse<GetContactsOkResponse>> GetContactsAsync(CancellationToken cancellationToken = default)
        => gameApi.GetContactsAsync(cancellationToken);

    public Task<UexApiResponse<GetContractsOkResponse>> GetContractsAsync(CancellationToken cancellationToken = default)
        => gameApi.GetContractsAsync(cancellationToken);

    public Task<UexApiResponse<GetFactionsOkResponse>> GetFactionsAsync(CancellationToken cancellationToken = default)
        => gameApi.GetFactionsAsync(cancellationToken);

    public Task<UexApiResponse<GetGameVersionsOkResponse>> GetGameVersionsAsync(CancellationToken cancellationToken = default)
        => gameApi.GetGameVersionsAsync(cancellationToken);

    public Task<UexApiResponse<GetJumpPointsOkResponse>> GetJumpPointsAsync(double? id_orbit_origin = null, CancellationToken cancellationToken = default)
        => gameApi.GetJumpPointsAsync(id_orbit_origin, cancellationToken);

    public Task<UexApiResponse<GetJurisdictionsOkResponse>> GetJurisdictionsAsync(CancellationToken cancellationToken = default)
        => gameApi.GetJurisdictionsAsync(cancellationToken);

    public Task<UexApiResponse<GetMoonsOkResponse>> GetMoonsAsync(double? id_star_system = null, CancellationToken cancellationToken = default)
        => gameApi.GetMoonsAsync(id_star_system, cancellationToken);

    public Task<UexApiResponse<GetOrbitsOkResponse>> GetOrbitsAsync(double? id_star_system = null, CancellationToken cancellationToken = default)
        => gameApi.GetOrbitsAsync(id_star_system, cancellationToken);

    public Task<UexApiResponse> GetOrbitsDistancesAsync(CancellationToken cancellationToken = default)
        => gameApi.GetOrbitsDistancesAsync(cancellationToken);

    public Task<UexApiResponse<GetOutpostsOkResponse>> GetOutpostsAsync(double? id_moon = null, CancellationToken cancellationToken = default)
        => gameApi.GetOutpostsAsync(id_moon, cancellationToken);

    public Task<UexApiResponse<GetPlanetsOkResponse>> GetPlanetsAsync(double? id_star_system = null, CancellationToken cancellationToken = default)
        => gameApi.GetPlanetsAsync(id_star_system, cancellationToken);

    public Task<UexApiResponse<GetPoiOkResponse>> GetPoiAsync(double? id_outpost = null, CancellationToken cancellationToken = default)
        => gameApi.GetPoiAsync(id_outpost, cancellationToken);

    public Task<UexApiResponse<GetReleaseNotesOkResponse>> GetReleaseNotesAsync(CancellationToken cancellationToken = default)
        => gameApi.GetReleaseNotesAsync(cancellationToken);

    public Task<UexApiResponse<GetSpaceStationsOkResponse>> GetSpaceStationsAsync(double? id_moon = null, CancellationToken cancellationToken = default)
        => gameApi.GetSpaceStationsAsync(id_moon, cancellationToken);

    public Task<UexApiResponse<GetStarSystemsOkResponse>> GetStarSystemsAsync(CancellationToken cancellationToken = default)
        => gameApi.GetStarSystemsAsync(cancellationToken);

    public Task<UexApiResponse<GetTerminalsOkResponse>> GetTerminalsAsync(double? id_outpost = null, CancellationToken cancellationToken = default)
        => gameApi.GetTerminalsAsync(id_outpost, cancellationToken);

    public Task<UexApiResponse> GetTerminalsDistancesAsync(CancellationToken cancellationToken = default)
        => gameApi.GetTerminalsDistancesAsync(cancellationToken);

    public Task<UexApiResponse<GetVehiclesOkResponse>> GetVehiclesAsync(double? id_company = null, CancellationToken cancellationToken = default)
        => CacheAsync(
            new
            {
                id_company,
            },
            nameof(GetVehiclesAsync),
            x => gameApi.GetVehiclesAsync(x.id_company, cancellationToken)
        );
}
