namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync.Live;

using Domain.Models.Game;
using Xunit.Abstractions;

/// <remarks>
///     This is the most dependency-complex test.
/// </remarks>
[Trait(nameof(TestConstants.Traits.DataSource), TestConstants.Traits.DataSource.ExternalApi)]
[Trait(nameof(TestConstants.Traits.DataState), TestConstants.Traits.DataState.Live)]
public class LiveUexTerminalSyncRepositoryTest(ITestOutputHelper testOutputHelper, LiveUexSyncRepositoryTestFixture fixture)
    : UexSyncRepositoryTestBase<GameTerminal, LiveUexSyncRepositoryTestFixture>(testOutputHelper, fixture)
{
    protected override Task LoadDependenciesAsync(CancellationToken cancellationToken)
        => Task.WhenAll(
            LoadDependenciesAsync<GameStarSystem>(cancellationToken),
            LoadDependenciesAsync<GameMoon>(cancellationToken),
            LoadDependenciesAsync<GamePlanet>(cancellationToken),
            LoadDependenciesAsync<GameCity>(cancellationToken),
            LoadDependenciesAsync<GameOutpost>(cancellationToken),
            LoadDependenciesAsync<GameSpaceStation>(cancellationToken)
        );
}
