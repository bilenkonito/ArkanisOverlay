namespace Arkanis.Overlay.Domain.Models.Analytics;

public abstract record AnalyticsEvent(string EventName, string ModuleName);

public record BuiltInAnalyticsEvent(string EventName) : AnalyticsEvent(EventName, "Arkanis.Overlay");

public sealed record EntitySearchEvent(string Query) : BuiltInAnalyticsEvent(nameof(EntitySearchEvent));

public sealed record DialogOpenedEvent(string DialogId) : BuiltInAnalyticsEvent(nameof(DialogOpenedEvent));
