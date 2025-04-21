namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync.Cached;

using Domain.Models.Game;
using Xunit.Abstractions;

[Collection("Cached UEX API")]
public class CachedUexSpaceShipSyncRepositoryTest(ITestOutputHelper testOutputHelper, CachedUexSyncRepositoryTestFixture fixture)
    : UexSyncRepositoryTestBase<GameSpaceShip, CachedUexSyncRepositoryTestFixture>(testOutputHelper, fixture)
{
    protected override Task LoadDependenciesAsync(CancellationToken cancellationToken)
        => LoadDependenciesAsync<GameCompany>(cancellationToken);
}
