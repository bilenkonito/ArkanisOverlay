namespace Arkanis.Overlay.Domain.Abstractions.Services;

using Models.Analytics;

public interface IAnalyticsEventReporter
{
    Task TrackEventAsync(AnalyticsEvent analyticsEvent);
}
