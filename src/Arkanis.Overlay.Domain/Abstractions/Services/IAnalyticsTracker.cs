namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Models.Analytics;

public interface IAnalyticsTracker
{
    Task TrackEventAsync(AnalyticsEvent analyticsEvent);
}
