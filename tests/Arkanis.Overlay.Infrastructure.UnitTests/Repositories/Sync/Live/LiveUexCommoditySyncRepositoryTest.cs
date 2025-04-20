namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync.Live;

using Domain.Models.Game;
using Xunit.Abstractions;

[Collection("Live UEX API")]
public class LiveUexCommoditySyncRepositoryTest(ITestOutputHelper testOutputHelper, LiveUexSyncRepositoryTestFixture fixture)
    : UexSyncRepositoryTestBase<GameCommodity, LiveUexSyncRepositoryTestFixture>(testOutputHelper, fixture);
