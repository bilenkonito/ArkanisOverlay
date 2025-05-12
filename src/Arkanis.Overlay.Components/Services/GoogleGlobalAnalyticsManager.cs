namespace Arkanis.Overlay.Components.Services;

using Blazor.Analytics;
using Common.Abstractions;
using Domain.Abstractions.Services;
using Domain.Models.Analytics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class GoogleAnalyticsEventReporter(
    IAnalytics analytics,
    IHostEnvironment hostEnvironment,
    IAppVersionProvider versionProvider,
    IUserPreferencesProvider userPreferencesProvider,
    ILogger<GoogleAnalyticsEventReporter> logger
) : IAnalyticsEventReporter
{
    private const string EventCategoryKey = "event_category";
    private const string EventLabelKey = "event_label";

    private const string EnvironmentTypeKey = "environment_type";
    private const string InstallationIdKey = "installation_id";
    private const string ApplicationVersionKey = "app_version";
    private const string TrafficTypeKey = "traffic_type";

    public async Task TrackEventAsync(AnalyticsEvent analyticsEvent)
    {
        logger.LogDebug("Reporting event: {AnalyticsEventName}", analyticsEvent.EventName);
        await analytics.TrackEvent(analyticsEvent.EventName, CreateEventData(analyticsEvent));
    }

    private Dictionary<string, object> CreateEventData(AnalyticsEvent analyticsEvent)
    {
        var eventData = analyticsEvent switch
        {
            BuiltInFeatureUsageStateChangedEvent @event => CreateSpecificEventData(@event),
            DialogOpenedEvent @event => CreateSpecificEventData(@event),
            SearchEvent @event => CreateSpecificEventData(@event),
            _ => [],
        };

        AddCommonEventData(eventData);
        return eventData;
    }

    private void AddCommonEventData(Dictionary<string, object> eventData)
    {
        eventData[EnvironmentTypeKey] = hostEnvironment.EnvironmentName;
        eventData[InstallationIdKey] = userPreferencesProvider.CurrentPreferences.InstallationId.ToString();
        eventData[ApplicationVersionKey] = versionProvider.CurrentVersion.ToFullString();
        eventData[TrafficTypeKey] = hostEnvironment.IsProduction() ? "public" : "internal";
    }

    private static Dictionary<string, object> CreateSpecificEventData(BuiltInFeatureUsageStateChangedEvent @event)
        => new()
        {
            [EventCategoryKey] = "Feature",
            [EventLabelKey] = @event.FeatureName,
            ["value"] = @event.IsEnabled ? 1 : 0,
        };

    private static Dictionary<string, object> CreateSpecificEventData(SearchEvent @event)
        => new()
        {
            ["search_term"] = @event.Query,
        };

    private static Dictionary<string, object> CreateSpecificEventData(DialogOpenedEvent @event)
        => new()
        {
            [EventCategoryKey] = "Dialog",
            [EventLabelKey] = "Opened",
            ["value"] = @event.DialogId,
        };
}
