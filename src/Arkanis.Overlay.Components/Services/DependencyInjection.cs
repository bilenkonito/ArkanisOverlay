namespace Arkanis.Overlay.Components.Services;

using Abstractions;
using Blazor.Analytics;
using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddGlobalKeyboardProxyService(this IServiceCollection services)
        => services.AddScoped<IKeyboardProxy, GlobalOverlayKeyboardProxy>();

    public static IServiceCollection AddGoogleTrackingServices(this IServiceCollection services)
        => services.AddGoogleAnalytics("G-ND6WBR51VP", true)
            .AddScoped<IAnalyticsTracker, GoogleAnalyticsTracker>();
}
