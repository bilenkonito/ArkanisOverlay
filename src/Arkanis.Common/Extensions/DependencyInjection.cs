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
        where TRegistered : notnull
    {
        var serviceDescriptor = ServiceDescriptor.Describe(
            typeof(TAlias),
            provider =>
            {
                var requiredService = provider.GetRequiredService<TRegistered>();
                if (requiredService is not TService service)
                {
                    throw new InvalidOperationException(
                        $"Unable to create alias {typeof(TAlias)} for {typeof(TService)} registered as {typeof(TRegistered)},"
                        + $" because the actually registered service {requiredService.GetType()} is not assignable to {typeof(TService)}."
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
