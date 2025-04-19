namespace Arkanis.Overlay.Infrastructure;

using API;
using Common.Extensions;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Options;
using Repositories.Sync;
using Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services
            .AddSearchServices()
            .AddCustomUexApiServices()
            .AddUexDatabaseServices()
            .AddUexSyncRepositoryServices()
            .AddUserPreferencesFileManagerServices();

    public static IServiceCollection AddInfrastructureHostedServices(this IServiceCollection services)
        => services
            .AddEndpointManagerHostedService();

    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddConfiguration<ConfigurationOptions>(configuration);
}
