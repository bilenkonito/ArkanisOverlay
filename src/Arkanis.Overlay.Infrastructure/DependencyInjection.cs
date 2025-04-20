namespace Arkanis.Overlay.Infrastructure;

using Common.Extensions;
using Data;
using External.UEX;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Options;
using Repositories;
using Repositories.Sync;
using Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services
            .AddInMemorySearchServices()
            .AddAllUexApiClients()
            .AddUexDatabaseServices()
            .AddUexSyncRepositoryServices()
            .AddUexInMemoryGameEntityServices()
            .AddUserPreferencesFileManagerServices();

    public static IServiceCollection AddInfrastructureHostedServices(this IServiceCollection services)
        => services
            .AddEndpointManagerHostedService();

    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddConfiguration<ConfigurationOptions>(configuration);
}
