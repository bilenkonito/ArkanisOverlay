namespace Arkanis.Overlay.Components.Services;

using Blazor.Analytics;
using Domain.Abstractions.Services;
using Domain.Models.Analytics;
using Microsoft.Extensions.Logging;

public class GoogleAnalyticsTracker(IAnalytics analytics, ILogger<GoogleAnalyticsTracker> logger) : IAnalyticsTracker
{
    public async Task TrackEventAsync(AnalyticsEvent analyticsEvent)
    {
        logger.LogDebug("Reporting event: {AnalyticsEventName}", analyticsEvent.EventName);
        await analytics.TrackEvent(analyticsEvent.EventName, CreateEventData(analyticsEvent));
    }

    private static object CreateEventData(AnalyticsEvent analyticsEvent)
        => analyticsEvent switch
        {
            DialogOpenedEvent @event => CreateSpecificEventData(@event),
            EntitySearchEvent @event => CreateSpecificEventData(@event),
            _ => new object(),
        };

    private static object CreateSpecificEventData(EntitySearchEvent @event)
        => new
        {
            search_term = @event.Query,
        };

    private static object CreateSpecificEventData(DialogOpenedEvent @event)
        => new
        {
            eventCategory = "Dialog",
            eventAction = "Opened",
            eventLabel = @event.DialogId,
        };
}
