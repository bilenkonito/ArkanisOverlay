namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync.Live;

using Domain.Abstractions.Services;
using External.UEX;
using Infrastructure.Repositories.Sync;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

public class LiveUexSyncRepositoryTestFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        => services
            .AddHttpClient()
            .AddSingleton(typeof(IGameEntityRepository<>), typeof(GameEntityRepositoryMock<>))
            .AddAllUexApiClients()
            .AddUexSyncRepositoryServices();

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        => [];

    protected override ValueTask DisposeAsyncCore()
        => ValueTask.CompletedTask;
}
