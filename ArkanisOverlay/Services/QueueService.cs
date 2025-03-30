using System.Collections.Concurrent;
using ArkanisOverlay.Data.Entities.UEX;
using ArkanisOverlay.Workers;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Services;

public class QueueService(ILogger<QueueService> logger, DataSync dataSync)
{
    public readonly ConcurrentQueue<Task> PendingUpdates = new();
    
    public void EnqueueUpdate<T>(string apiPath) where T : BaseEntity, new()
    {
        logger.LogInformation("Enqueuing update for {type} from {endpoint}", typeof(T).Name, apiPath);
        PendingUpdates.Enqueue(new Task(() =>
        {
            dataSync.Update<T>(apiPath).ConfigureAwait(false).GetAwaiter().GetResult();
        }));
    }
}