namespace Arkanis.Overlay.Domain.Models.Analytics;

public abstract record AnalyticsEvent(string EventName, string ModuleName);

public record BuiltInAnalyticsEvent(string EventName) : AnalyticsEvent(EventName, "user_behaviour");

#region Events with custom values (must also be explicitly registered in global analytics manager to work properly)

public record FeatureUsageStateChangedEvent(string EventName, bool IsEnabled) : AnalyticsEvent(EventName, "feature_usage_state_changed")
{
    public static FeatureUsageStateChangedEvent TerminateWithGame(bool enabled = true)
        => new("overlay__app__terminate", enabled);

    public static FeatureUsageStateChangedEvent AutoStart(bool enabled = true)
        => new("overlay__app__autostart", enabled);

    public static FeatureUsageStateChangedEvent Analytics(bool enabled = true)
        => new("overlay__app__analytics", enabled);

    public static FeatureUsageStateChangedEvent BlurBackground(bool enabled = true)
        => new("overlay__blur", enabled);
}

public sealed record SearchEvent(string Query) : BuiltInAnalyticsEvent("search");

public sealed record DialogOpenedEvent(string DialogId) : BuiltInAnalyticsEvent("dialog_opened");

#endregion

#region Regular events

public sealed record SearchCalculationEvent() : BuiltInAnalyticsEvent("search_calculate");

public sealed record OverlayShownEvent() : BuiltInAnalyticsEvent("overlay_shown");

public sealed record OverlayHiddenEvent() : BuiltInAnalyticsEvent("overlay_hidden");

#endregion
