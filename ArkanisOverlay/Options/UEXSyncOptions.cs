using ArkanisOverlay.Data.Entities.UEX;

namespace ArkanisOverlay.Options;

public static class UEXSyncOptions
{
    public static List<IEndpointConfig> Endpoints { get; set; } =
    [
        new EndpointConfig<VehicleEntity>("vehicles", "00:00:10"),
        new EndpointConfig<ItemEntity, CategoryEntity>("items", "00:00:10",
            (categories) => categories.Where(c => c.Type == "item").ToList()
        ),
    ];
}

public interface IEndpointConfig
{
    string Endpoint { get; }
    TimeSpan CacheTtl { get; }

    Type DataType { get; }
    Type? DependencyType { get; } // Nullable, since not all configs have dependencies
}

public class EndpointConfig<TData>(
    string endpoint,
    string cacheTtl
) : IEndpointConfig where TData : BaseEntity
{
    public string Endpoint { get; } = endpoint;
    public TimeSpan CacheTtl { get; } = TimeSpan.Parse(cacheTtl);
    public Type DataType { get; } = typeof(TData);
    public Type? DependencyType => null; // No dependency
}

public class EndpointConfig<TData, TDependsOn>(
    string endpoint,
    string cacheTtl,
    Func<List<TDependsOn>, List<TDependsOn>>? dependencyFilter
) : IEndpointConfig
    where TData : BaseEntity where TDependsOn : BaseEntity
{
    public Type DataType { get; } = typeof(TData);
    public string Endpoint { get; } = endpoint;

    public TimeSpan CacheTtl { get; } = TimeSpan.Parse(cacheTtl);

    //* since there is no way to set an optional type constraint on a generic type parameter
    //* we need to check if the depends on type is the same as the data type
    //* => EndpointConfig<TData, TData> = EndpointConfig<TData, null>
    public Type? DependencyType { get; } = typeof(TData) == typeof(TDependsOn) ? null : typeof(TDependsOn);
    public Func<List<TDependsOn>, List<TDependsOn>>? DependencyFilter { get; } = dependencyFilter;
}