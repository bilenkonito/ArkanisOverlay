namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Models.Game;
using Shouldly;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

public abstract class UexSyncRepositoryTestBase<T>(ITestOutputHelper testOutputHelper, UexSyncRepositoryTestFixture fixture)
    : TestBed<UexSyncRepositoryTestFixture>(testOutputHelper, fixture)
    where T : GameEntity
{
    protected CancellationTokenSource TestCancellation { get; } = new(TimeSpan.FromSeconds(5));

    protected CancellationToken TestCancellationToken
        => TestCancellation.Token;

    protected IGameEntityRepository<TEntity> ResolveRepositoryFor<TEntity>() where TEntity : class, IGameEntity
        => _fixture.GetService<IGameEntityRepository<TEntity>>(_testOutputHelper).ShouldNotBeNull();

    protected IGameEntityExternalSyncRepository<T> ResolveSyncRepositoryFor()
        => _fixture.GetService<IGameEntityExternalSyncRepository<T>>(_testOutputHelper).ShouldNotBeNull();

    [Fact]
    public async Task GetAllAsync_Should_Correctly_Map_All_Items()
    {
        var repositorySUT = ResolveSyncRepositoryFor();

        await LoadDependenciesAsync(TestCancellationToken);
        var syncData = await repositorySUT.GetAllAsync(TestCancellationToken);
        var entities = await syncData.GameEntities.ToListAsync(TestCancellationToken);

        entities.ShouldNotBeEmpty();
    }

    protected async Task LoadDependenciesAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class, IGameEntity
    {
        var planetRepository = ResolveRepositoryFor<TEntity>();
        var gameDataState = new SyncedGameDataState(StarCitizenVersion.Create("4.1.0"), DateTimeOffset.UtcNow);
        var cacheUntil = gameDataState.UpdatedAt + TimeSpan.FromMinutes(10);
        var syncData = new GameEntitySyncData<TEntity>(planetRepository.GetAllAsync(cancellationToken), gameDataState, cacheUntil);
        await planetRepository.UpdateAllAsync(syncData, cancellationToken);
    }

    protected virtual Task LoadDependenciesAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
