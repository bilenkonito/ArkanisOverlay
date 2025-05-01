namespace Arkanis.Overlay.Components.Services;

using Abstractions;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddGlobalKeyboardProxyService(this IServiceCollection services)
        => services.AddScoped<IKeyboardProxy, GlobalOverlayKeyboardProxy>();
}
