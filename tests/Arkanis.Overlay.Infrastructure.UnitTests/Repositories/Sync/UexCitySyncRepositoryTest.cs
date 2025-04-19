namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync;

using Domain.Models.Game;
using Xunit.Abstractions;

public class UexCitySyncRepositoryTest(ITestOutputHelper testOutputHelper, UexSyncRepositoryTestFixture fixture)
    : UexSyncRepositoryTestBase<GameCity>(testOutputHelper, fixture)
{
    protected override Task LoadDependenciesAsync(CancellationToken cancellationToken)
        => Task.WhenAll(
            LoadDependenciesAsync<GameStarSystem>(cancellationToken),
            LoadDependenciesAsync<GameMoon>(cancellationToken),
            LoadDependenciesAsync<GamePlanet>(cancellationToken)
        );
}
