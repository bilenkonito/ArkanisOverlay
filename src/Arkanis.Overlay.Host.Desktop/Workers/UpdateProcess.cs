namespace Arkanis.Overlay.Host.Desktop.Workers;

using Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Velopack;

internal class UpdateProcess(UpdateManager updateManager, WindowsNotifications notifications) : IDisposable
{
    private WindowsNotifications.UpdatableNotification<WindowsNotifications.UpdateProgressParams>? _progressToast;

    public void Dispose()
        => _progressToast?.Hide();

    /// <summary>
    ///     Executes the update process by checking for new updates, prompting the user for confirmation,
    ///     downloading, and applying the updates, followed by restarting the application.
    /// </summary>
    /// <param name="forced">Indicates whether user confirmation is requested</param>
    /// <param name="cancellationToken">A token to cancel the operation</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task RunAsync(bool forced, CancellationToken cancellationToken = default)
    {
        if (!updateManager.IsInstalled)
        {
            // do not check for updates if the application is not installed (=app is not updatable)
            return;
        }

        // check for new version
        var newVersion = await updateManager.CheckForUpdatesAsync();
        if (newVersion == null)
        {
            // no updates available
            return;
        }

        if (!forced && !await notifications.ShouldUpdateNowAsync(newVersion, cancellationToken))
        {
            // the user has chosen not to update
            return;
        }

        // download new version
        ShowProgressToast(newVersion);
        await updateManager.DownloadUpdatesAsync(newVersion, UpdateProgressToast, cancelToken: cancellationToken);

        // install new version and restart app
        updateManager.ApplyUpdatesAndRestart(newVersion);
    }

    private void ShowProgressToast(UpdateInfo newVersion)
        => _progressToast ??= notifications.ShowUpdateProgressToast(newVersion);

    private void UpdateProgressToast(int progressPercent)
        => _progressToast?.Update(new WindowsNotifications.UpdateProgressParams(progressPercent));

    internal class CheckForUpdatesJob(IServiceProvider serviceProvider) : IJob
    {
        public static readonly TimeSpan UpdateCheckInterval = TimeSpan.FromMinutes(15);

        public static ITrigger Trigger { get; } = TriggerBuilder.Create()
            .WithIdentity(nameof(CheckForUpdatesJob))
            .StartNow()
            .WithSimpleSchedule(x => x.WithInterval(UpdateCheckInterval).RepeatForever())
            .Build();

        public async Task Execute(IJobExecutionContext context)
        {
            using var updateProcess = ActivatorUtilities.CreateInstance<UpdateProcess>(serviceProvider);
            await updateProcess.RunAsync(false, context.CancellationToken);
        }

        private static async Task ScheduleAsync(IScheduler scheduler, CancellationToken cancellationToken)
            => await scheduler.ScheduleJob(CreateJob(), Trigger, cancellationToken);

        private static IJobDetail CreateJob()
            => JobBuilder.Create<CheckForUpdatesJob>()
                .WithIdentity(nameof(CheckForUpdatesJob))
                .DisallowConcurrentExecution()
                .Build();

        public class SelfScheduleService(UpdateManager updateManager, ISchedulerFactory schedulerFactory, ILogger<SelfScheduleService> logger) : IHostedService
        {
            public async Task StartAsync(CancellationToken cancellationToken)
            {
                if (!updateManager.IsInstalled)
                {
                    logger.LogInformation("The application is not installed, skipping scheduling for updates");
                    return;
                }

                var scheduler = await schedulerFactory.GetScheduler(cancellationToken);
                await ScheduleAsync(scheduler, cancellationToken);
            }

            public Task StopAsync(CancellationToken cancellationToken)
                => Task.CompletedTask;
        }
    }
}
