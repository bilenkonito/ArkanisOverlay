namespace Arkanis.Overlay.Components.Services;

using Abstractions;
using Blazor.Analytics;
using Blazor.Analytics.Abstractions;
using Blazor.Analytics.GoogleAnalytics;
using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddGlobalKeyboardProxyService(this IServiceCollection services)
        => services.AddScoped<IKeyboardProxy, GlobalOverlayKeyboardProxy>();

    public static IServiceCollection AddGoogleTrackingServices(this IServiceCollection services)
        => services
            .AddScoped<GoogleAnalyticsStrategy>()
            .AddScoped<IAnalytics, ConfigurableGoogleAnalyticsStrategy>()
            .AddScoped<ITrackingNavigationState, TrackingNavigationState>()
            .AddScoped<IAnalyticsEventReporter, GoogleAnalyticsEventReporter>()
            .AddSingleton<IGlobalAnalyticsReporter, GlobalAnalyticsReporter>();
}
