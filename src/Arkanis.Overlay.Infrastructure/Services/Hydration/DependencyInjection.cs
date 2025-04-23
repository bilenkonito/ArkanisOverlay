namespace Arkanis.Overlay.Infrastructure.Services.Hydration;

using Abstractions;
using Common.Extensions;
using Domain.Models.Game;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddUexHydrationServices(this IServiceCollection services)
        => services
            .AddHydrationServiceFor<UexGameCommodityPriceHydrationService, GameCommodity>()
            .AddHydrationServiceFor<UexGameVehiclePriceHydrationService, GameVehicle>()
            .AddHydrationServiceFor<UexGameItemPriceHydrationService, GameItem>()
            .AddHydrationServiceFor<UexGameItemTraitHydrationService, GameItem>()
            .AddSingleton<IGameEntityHydrationService, GameEntityPriceHydrationService>();

    private static IServiceCollection AddHydrationServiceFor<TService, TEntity>(this IServiceCollection services)
        where TService : class, IHydrationServiceFor<TEntity>
        => services
            .AddSingleton<TService>()
            .Alias<IHydrationServiceFor, TService>()
            .Alias<IHydrationServiceFor<TEntity>, TService>();
}
