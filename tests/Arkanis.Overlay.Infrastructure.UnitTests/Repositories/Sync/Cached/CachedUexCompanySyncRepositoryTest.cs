namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync.Cached;

using Domain.Models.Game;
using Xunit.Abstractions;

[Collection("Cached UEX API")]
public class CachedUexCompanySyncRepositoryTest(ITestOutputHelper testOutputHelper, CachedUexSyncRepositoryTestFixture fixture)
    : UexSyncRepositoryTestBase<GameCompany, CachedUexSyncRepositoryTestFixture>(testOutputHelper, fixture);
