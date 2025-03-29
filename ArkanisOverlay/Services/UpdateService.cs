using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Services;

public class UpdateService(ILogger<UpdateService> logger, QueueService queueService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Executing UpdateService");

        while (!stoppingToken.IsCancellationRequested)
        {
            if (queueService.PendingUpdates.TryDequeue(out var updateTask))
            {
                logger.LogInformation("Executing pending update");
                updateTask.Start();
                await updateTask.ConfigureAwait(false);
                logger.LogInformation("Completed pending update");
            }
            else
            {
                await Task.Delay(1_000, stoppingToken).ConfigureAwait(false);
            }
        }
    }
}