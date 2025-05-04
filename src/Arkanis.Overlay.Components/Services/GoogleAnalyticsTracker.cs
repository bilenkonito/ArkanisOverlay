namespace Arkanis.Overlay.Components.Services;

using Blazor.Analytics;
using Domain.Abstractions.Services;
using Domain.Models.Analytics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class GoogleAnalyticsTracker(IAnalytics analytics, IHostEnvironment hostEnvironment, ILogger<GoogleAnalyticsTracker> logger) : IAnalyticsTracker
{
    private const string EventCategoryKey = "event_category";
    private const string EventLabelKey = "event_label";
    private const string EnvironmentTypeKey = "environment_type";
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
            DialogOpenedEvent @event => CreateSpecificEventData(@event),
            EntitySearchEvent @event => CreateSpecificEventData(@event),
            _ => [],
        };

        AddCommonEventData(eventData);
        return eventData;
    }

    private void AddCommonEventData(Dictionary<string, object> eventData)
    {
        eventData[EnvironmentTypeKey] = hostEnvironment.EnvironmentName;
        eventData[TrafficTypeKey] = hostEnvironment.IsProduction() ? "public" : "internal";
    }

    private static Dictionary<string, object> CreateSpecificEventData(EntitySearchEvent @event)
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
