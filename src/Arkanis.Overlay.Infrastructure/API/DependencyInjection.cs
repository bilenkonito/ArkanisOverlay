namespace Arkanis.Overlay.Infrastructure.API;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomUexApiServices(this IServiceCollection services)
        => services.AddSingleton<DataClient>();
}
