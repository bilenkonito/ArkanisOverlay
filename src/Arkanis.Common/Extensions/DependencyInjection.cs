namespace Arkanis.Common.Extensions;

using Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddConfiguration<T>(this IServiceCollection services, IConfiguration configuration) where T : class, ISelfBindableOptions
        => services.Configure<T>(instance => instance.Bind(configuration));

    public static IServiceCollection Alias<TAlias, TService>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        where TService : TAlias
    {
        var serviceDescriptor = ServiceDescriptor.Describe(
            typeof(TAlias),
            provider => provider.GetRequiredService(typeof(TService)),
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
    {
        var serviceDescriptor = ServiceDescriptor.Describe(
            typeof(TAlias),
            provider => provider.GetRequiredService(typeof(TRegistered)) as TService
                        ?? throw new ApplicationException($"Unable to resolve service {typeof(TRegistered)} implemented by {typeof(TService)}."),
            lifetime
        );
        services.Add(serviceDescriptor);
        return services;
    }
}
