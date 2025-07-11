namespace Arkanis.Overlay.Infrastructure.Services;

using Abstractions;
using Microsoft.Extensions.Primitives;

public sealed class ChangeTokenManager : IChangeTokenManager, IDisposable, IAsyncDisposable
{
    private readonly Dictionary<Type, CancellationTokenSource> _changeProviders = [];
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public void Dispose()
    {
        foreach (var cancellationTokenSource in _changeProviders.Values)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        _changeProviders.Clear();
    }

    public async ValueTask DisposeAsync()
    {
        foreach (var cancellationTokenSource in _changeProviders.Values)
        {
            await cancellationTokenSource.CancelAsync();
            cancellationTokenSource.Dispose();
        }

        _changeProviders.Clear();
    }

    public IChangeToken GetChangeTokenFor<T>()
    {
        var changeProvider = GetOrCreateChangeProvider(typeof(T));
        return new CancellationChangeToken(changeProvider.Token);
    }

    public async Task<IChangeToken> ResetChangeTokenFor<T>()
    {
        try
        {
            await _semaphore.WaitAsync();
            var targetType = typeof(T);

            if (_changeProviders.TryGetValue(targetType, out var changeProvider))
            {
                await changeProvider.CancelAsync();
                changeProvider.Dispose();
            }

            changeProvider = GetOrCreateChangeProvider(targetType, true);
            return new CancellationChangeToken(changeProvider.Token);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task TriggerChangeForAsync<T>()
        => await ResetChangeTokenFor<T>();

    private CancellationTokenSource GetOrCreateChangeProvider(Type targetType, bool recreate = false)
    {
        CancellationTokenSource? changeProvider = null;
        if (recreate || !_changeProviders.TryGetValue(targetType, out changeProvider))
        {
            _changeProviders[targetType] = changeProvider ??= new CancellationTokenSource();
        }

        return changeProvider;
    }
}
