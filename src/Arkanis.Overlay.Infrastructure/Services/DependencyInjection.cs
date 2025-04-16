namespace Arkanis.Overlay.Infrastructure.Services;

using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddSearchServices(this IServiceCollection services)
        => services.AddScoped<ISearchService, SearchService>();

    public static IServiceCollection AddEndpointManagerHostedService(this IServiceCollection services)
        => services.AddHostedService<EndpointManager>();
}
