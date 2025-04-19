namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync;

using Domain.Models.Game;
using Xunit.Abstractions;

public class UexCompanySyncRepositoryTest(ITestOutputHelper testOutputHelper, UexSyncRepositoryTestFixture fixture)
    : UexSyncRepositoryTestBase<GameCompany>(testOutputHelper, fixture);
