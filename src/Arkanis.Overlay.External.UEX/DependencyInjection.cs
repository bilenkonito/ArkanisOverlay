namespace Arkanis.Overlay.External.UEX;

using Abstractions;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddAllUexApiClients(this IServiceCollection services, Func<IServiceProvider, UexApiOptions>? createOptions = null)
        => services
            .AddSingleton(createOptions ?? (_ => new UexApiOptions()))
            .AddSingleton<IUexCrewApi, UexCrewApi>()
            .AddSingleton<IUexCommoditiesApi, UexCommoditiesApi>()
            .AddSingleton<IUexFuelApi, UexFuelApi>()
            .AddSingleton<IUexGameApi, UexGameApi>()
            .AddSingleton<IUexItemsApi, UexItemsApi>()
            .AddSingleton<IUexMarketplaceApi, UexMarketplaceApi>()
            .AddSingleton<IUexOrganizationsApi, UexOrganizationsApi>()
            .AddSingleton<IUexRefineriesApi, UexRefineriesApi>()
            .AddSingleton<IUexStaticApi, UexStaticApi>()
            .AddSingleton<IUexUserApi, UexUserApi>()
            .AddSingleton<IUexVehiclesApi, UexVehiclesApi>();
}
