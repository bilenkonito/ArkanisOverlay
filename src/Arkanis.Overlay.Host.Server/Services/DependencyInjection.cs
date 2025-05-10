namespace Arkanis.Overlay.Host.Server.Services;

using Common.Extensions;
using Domain.Abstractions.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServerOverlayControls(this IServiceCollection services)
        => services.AddScoped<WebOverlayControls>()
            .Alias<IOverlayControls, WebOverlayControls>(ServiceLifetime.Scoped);
}
