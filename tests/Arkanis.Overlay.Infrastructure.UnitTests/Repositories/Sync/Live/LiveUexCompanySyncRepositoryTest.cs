namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync.Live;

using Domain.Models.Game;
using Xunit.Abstractions;

[Collection("Live UEX API")]
public class LiveUexCompanySyncRepositoryTest(ITestOutputHelper testOutputHelper, LiveUexSyncRepositoryTestFixture fixture)
    : UexSyncRepositoryTestBase<GameCompany, LiveUexSyncRepositoryTestFixture>(testOutputHelper, fixture);
