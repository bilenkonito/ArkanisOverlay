namespace Arkanis.Overlay.Domain.Models.Analytics;

public abstract record AnalyticsEvent(string EventName, string ModuleName);

public record BuiltInAnalyticsEvent(string EventName) : AnalyticsEvent(EventName, "Arkanis.Overlay");

public abstract record BuiltInFeatureUsageStateChangedEvent(string FeatureName, bool IsEnabled) : BuiltInAnalyticsEvent("feature_usage_state_changed");

public sealed record SearchEvent(string Query) : BuiltInAnalyticsEvent("search");

public sealed record DialogOpenedEvent(string DialogId) : BuiltInAnalyticsEvent(nameof(DialogOpenedEvent));
