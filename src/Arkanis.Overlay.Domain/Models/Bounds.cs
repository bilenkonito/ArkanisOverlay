namespace Arkanis.Overlay.Domain.Models;

public record Bounds<T>(T Min, T Max, T Avg) where T : IComparable<T>;
