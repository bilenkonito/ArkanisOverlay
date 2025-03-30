using ArkanisOverlay.Data.Entities.UEX;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timer = System.Threading.Timer;

namespace ArkanisOverlay.Services;

public class DataService(ILogger<DataService> logger, QueueService queueService)
    : IHostedService, IDisposable
{
    private readonly Dictionary<Type, Timer> _timers = new();

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting DataSyncService");
        InitializeAsync();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping DataSyncService");
        foreach (var (type, timer) in _timers)
        {
            logger.LogInformation("Stopping timer for {type}", type);
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        foreach (var (type, timer) in _timers)
        {
            logger.LogInformation("Disposing timer for {type}", type);
            timer.Dispose();
        }
    }

    private void InitializeAsync()
    {
        logger.LogInformation("Initializing DataSyncService");

        RegisterEndpoint<CommodityEntity>("commodities", "00:00:10");
        RegisterEndpoint<VehicleEntity>("vehicles", "00:00:10");
        RegisterEndpoint<CategoryEntity>("categories", "00:00:10");
    }

    private void RegisterEndpoint<T>(string apiPath, string cacheTtl) where T : BaseEntity, new()
    {
        var dataType = typeof(T);
        var interval = TimeSpan.Parse(cacheTtl);

        logger.LogInformation("Initializing timer for {type} with interval {interval} and endpoint {endpoint}",
            dataType, interval, apiPath);

        var timer = new Timer(TimerCallback<T>, apiPath, TimeSpan.Zero, interval);
        _timers.Add(dataType, timer);
    }

    private void TimerCallback<T>(object? state) where T : BaseEntity, new()
    {
        if (state is not string apiPath) return;
        
        queueService.EnqueueUpdate<T>(apiPath);
    }
}