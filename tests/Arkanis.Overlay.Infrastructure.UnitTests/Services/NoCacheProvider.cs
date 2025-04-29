namespace Arkanis.Overlay.Infrastructure.UnitTests.Services;

using Domain.Abstractions.Services;
using Domain.Models;

public class NoCacheProvider<T> : IExternalSyncCacheProvider<T> where T : class
{
    public Task StoreAsync<TSource>(TSource source, DataCached dataState, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public Task<SyncDataCache<TSource>> LoadAsync<TSource>(InternalDataState currentDataState, CancellationToken cancellationToken = default)
        => Task.FromResult(SyncDataCache.Missing<TSource>());
}
