namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync;

using Domain.Abstractions.Services;
using Infrastructure.Repositories.Sync;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

public class UexSyncRepositoryTestFixture : TestBedFixture
{
    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
        => services
            .AddHttpClient()
            .AddSingleton(typeof(IGameEntityRepository<>), typeof(GameEntityRepositoryMock<>))
            .AddUexSyncRepositoryServices();

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
        => [];

    protected override ValueTask DisposeAsyncCore()
        => ValueTask.CompletedTask;
}
