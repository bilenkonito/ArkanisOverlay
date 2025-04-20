namespace Arkanis.Overlay.Infrastructure.Services;

using Domain.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemorySearchServices(this IServiceCollection services)
        => services.AddScoped<ISearchService, InMemorySearchService>();

    public static IServiceCollection AddEndpointManagerHostedService(this IServiceCollection services)
        => services.AddHostedService<EndpointManager>();

    public static IServiceCollection AddUserPreferencesFileManagerServices(this IServiceCollection services)
        => services
            .AddSingleton<IUserPreferencesManager, UserPreferencesJsonFileManager>()
            .AddSingleton<IUserPreferencesProvider>(provider => provider.GetRequiredService<IUserPreferencesManager>())
            .AddHostedService<UserPreferencesLoader>();
}
