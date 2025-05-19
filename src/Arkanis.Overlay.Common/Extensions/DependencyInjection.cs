namespace Arkanis.Overlay.Common.Extensions;

using Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration<T>(this IServiceCollection services, IConfiguration configuration) where T : class, ISelfBindableOptions
        => services.Configure<T>(instance => instance.Bind(configuration));

    public static IServiceCollection Alias<TAlias, TService>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        where TService : class, TAlias
        => services.UnsafeAlias<TAlias>(typeof(TService), lifetime);

    public static IServiceCollection UnsafeAlias<TAlias>(
        this IServiceCollection services,
        Type targetServiceType,
        ServiceLifetime lifetime = ServiceLifetime.Singleton
    )
    {
        var serviceDescriptor = ServiceDescriptor.Describe(
            typeof(TAlias),
            provider =>
            {
                var requiredService = provider.GetService(targetServiceType);
                if (requiredService is not TAlias service)
                {
                    throw new InvalidOperationException(
                        $"Unable to resolve aliased service {typeof(TAlias)} from {targetServiceType},"
                        + $" because the actual service instance {requiredService?.GetType().ToString() ?? "<null>"} is not assignable to {typeof(TAlias)}."
                    );
                }

                return service;
            },
            lifetime
        );
        services.Add(serviceDescriptor);

        return services;
    }

    public static IServiceCollection AliasVia<TAlias, TRegistered, TService>(
        this IServiceCollection services,
        ServiceLifetime lifetime = ServiceLifetime.Singleton
    )
        where TService : class, TAlias, TRegistered
        where TRegistered : notnull
    {
        var serviceDescriptor = ServiceDescriptor.Describe(
            typeof(TAlias),
            provider =>
            {
                var requiredService = provider.GetService<TRegistered>();
                if (requiredService is not TService service)
                {
                    throw new InvalidOperationException(
                        $"Unable to resolve aliased service {typeof(TAlias)} from {typeof(TService)} registered as {typeof(TRegistered)},"
                        + $" because the actual service instance {requiredService?.GetType().ToString() ?? "<null>"} is not assignable to {typeof(TService)}."
                    );
                }

                return service;
            },
            lifetime
        );
        services.Add(serviceDescriptor);

        return services;
    }
}
