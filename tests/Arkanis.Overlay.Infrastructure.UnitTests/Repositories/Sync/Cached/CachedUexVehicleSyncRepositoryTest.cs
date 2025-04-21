namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync.Cached;

using Domain.Models.Game;
using Xunit.Abstractions;

[Collection("Cached UEX API")]
public class CachedUexGroundVehicleSyncRepositoryTest(ITestOutputHelper testOutputHelper, CachedUexSyncRepositoryTestFixture fixture)
    : UexSyncRepositoryTestBase<GameGroundVehicle, CachedUexSyncRepositoryTestFixture>(testOutputHelper, fixture)
{
    protected override Task LoadDependenciesAsync(CancellationToken cancellationToken)
        => LoadDependenciesAsync<GameCompany>(cancellationToken);
}
