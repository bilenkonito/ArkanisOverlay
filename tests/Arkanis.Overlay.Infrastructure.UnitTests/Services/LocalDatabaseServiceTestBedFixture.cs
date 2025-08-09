namespace Arkanis.Overlay.Infrastructure.UnitTests.Services;

using Common.Extensions;
using Data;
using Domain.Abstractions.Services;
using Infrastructure.Data.Mappers;
using Infrastructure.Services;
using Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class LocalDatabaseServiceTestBedFixture : OverlayDbContextTestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
    {
        services.AddSingleton<IMemoryCache, FakeMemoryCache>();
        services.AddSingleton<IGameEntityHydrationService, NoHydrationMockService>();
        services.AddSingleton<UexApiDtoMapper>();

        services.AddSingleton<ChangeTokenManager>()
            .Alias<IChangeTokenManager, ChangeTokenManager>();

        services.AddSingleton<LocalDatabaseInventoryManager>()
            .Alias<IInventoryManager, LocalDatabaseInventoryManager>();

        base.AddServices(services, configuration);
    }
}
