namespace Arkanis.Overlay.Components.Services;

using Abstractions;
using Blazor.Analytics;
using Blazor.Analytics.Abstractions;
using Blazor.Analytics.GoogleAnalytics;
using Common;
using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class DependencyInjection
{
    public static IServiceCollection AddGlobalKeyboardProxyService(this IServiceCollection services)
        => services.AddScoped<IKeyboardProxy, GlobalOverlayKeyboardProxy>();

    public static IServiceCollection AddGoogleTrackingServices(this IServiceCollection services)
        => services
            .AddScoped<ITrackingNavigationState, TrackingNavigationState>()
            .AddScoped<IAnalytics>(provider =>
                {
                    var hostEnvironment = provider.GetRequiredService<IHostEnvironment>();
                    var instance = ActivatorUtilities.CreateInstance<GoogleAnalyticsStrategy>(provider);
                    instance.Configure(ApplicationConstants.GoogleAnalyticsTrackingId, !hostEnvironment.IsProduction());
                    return instance;
                }
            )
            .AddScoped<IAnalyticsEventReporter, GoogleAnalyticsEventReporter>()
            .AddSingleton<IGlobalAnalyticsReporter, GlobalAnalyticsReporter>();
}
