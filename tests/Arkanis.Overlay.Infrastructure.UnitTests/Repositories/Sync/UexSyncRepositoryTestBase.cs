namespace Arkanis.Overlay.Infrastructure.UnitTests.Repositories.Sync;

using Domain.Abstractions.Game;
using Domain.Abstractions.Services;
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

    protected IGameEntityLocalRepository<TEntity> ResolveRepositoryFor<TEntity>() where TEntity : class, IGameEntity
        => _fixture.GetService<IGameEntityLocalRepository<TEntity>>(_testOutputHelper).ShouldNotBeNull();

    protected IGameEntityExternalSyncRepository<T> ResolveSyncRepositoryFor()
        => _fixture.GetService<IGameEntityExternalSyncRepository<T>>(_testOutputHelper).ShouldNotBeNull();

    [Fact]
    public async Task GetAllAsync_Should_Correctly_Map_All_Items()
    {
        var repositorySUT = ResolveSyncRepositoryFor();

        await LoadDependenciesAsync(TestCancellationToken);
        var result = await repositorySUT.GetAllAsync(TestCancellationToken).ToListAsync(TestCancellationToken);
        result.ShouldNotBeEmpty();
    }

    protected async Task LoadDependenciesAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class, IGameEntity
    {
        var planetRepository = ResolveRepositoryFor<TEntity>();
        await planetRepository.UpdateAllAsync(planetRepository.GetAllAsync(cancellationToken), cancellationToken);
    }

    protected virtual Task LoadDependenciesAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
