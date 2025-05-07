namespace Arkanis.Overlay.Application.Workers;

using System.Globalization;
using Windows.UI.Notifications;
using Microsoft.Extensions.Hosting;
using Microsoft.Toolkit.Uwp.Notifications;
using Quartz;
using Velopack;
using Velopack.Sources;

internal class UpdateProcess(IUpdateSource updateSource) : IDisposable
{
    private const string NotificationGroup = "Updates";
    private const string ProgressNotificationTag = "UpdateProgress";

    private const string ProgressValueDataKey = nameof(ProgressValueDataKey);
    private const string ProgressStatusDataKey = nameof(ProgressStatusDataKey);
    private readonly UpdateManager _manager = new(updateSource);

    private readonly HashSet<ToastNotification> _notificationsShown = [];
    private readonly ToastNotifierCompat _notifier = ToastNotificationManagerCompat.CreateToastNotifier();

    public void Dispose()
    {
        foreach (var toastNotification in _notificationsShown.ToList())
        {
            HideToast(toastNotification);
        }
    }

    private ToastNotification ShowToast(ToastNotification toast)
    {
        _notificationsShown.Add(toast);
        _notifier.Show(toast);
        return toast;
    }

    private void HideToast(ToastNotification toast)
    {
        _notificationsShown.Remove(toast);
        _notifier.Hide(toast);
    }

    private static NotificationData CreateProgressToastData(int progressPercent, uint sequenceNumber = 0)
        => new()
        {
            Values =
            {
                [ProgressValueDataKey] = (progressPercent / 100.0).ToString(CultureInfo.InvariantCulture),
                [ProgressStatusDataKey] = progressPercent switch
                {
                    <= 0 => "Preparing...",
                    < 100 => "Downloading...",
                    >= 100 => "Installing...",
                },
            },
            // provide sequence number to prevent out-of-order updates, or assign 0 to indicate "always update"
            SequenceNumber = sequenceNumber,
        };

    private void ShowProgressToast(UpdateInfo updateInfo)
    {
        // Construct the toast content with data bound fields
        var content = new ToastContentBuilder()
            .AddText("Downloading and installing application update...")
            .SetToastScenario(ToastScenario.Reminder)
            .AddVisualChild(
                new AdaptiveProgressBar
                {
                    Title = $"Updating to {updateInfo.TargetFullRelease.Version}",
                    Value = new BindableProgressBarValue(ProgressValueDataKey),
                    Status = new BindableString(ProgressStatusDataKey),
                }
            )
            .GetToastContent();

        // Generate the toast notification
        var toast = new ToastNotification(content.GetXml())
        {
            Group = NotificationGroup,
            Tag = ProgressNotificationTag,
            Data = CreateProgressToastData(0),
        };

        ShowToast(toast);
    }

    private void UpdateProgressToast(int progressPercent)
        => _notifier.Update(CreateProgressToastData(progressPercent), ProgressNotificationTag, NotificationGroup);

    private async Task<bool> ShouldUpdateAsync(UpdateInfo updateInfo, CancellationToken cancellationToken)
    {
        new ToastContentBuilder()
            .SetToastScenario(ToastScenario.Reminder)
            .AddText("Update Pending", AdaptiveTextStyle.Caption)
            .AddText("New overlay version is available!", AdaptiveTextStyle.Body)
            .AddText($"You can now update to version {updateInfo.TargetFullRelease.Version}.", AdaptiveTextStyle.BodySubtle)
            .AddButton(
                new ToastButton("Update now", string.Empty)
                    .SetBackgroundActivation()
                    .AddArgument("action", "updateNow")
            )
            .AddButton(new ToastButtonSnooze("Update later"))
            .Show(toast => toast.ExpirationTime = DateTimeOffset.Now.AddMinutes(1));

        var activationTask = new TaskCompletionSource();
        ToastNotificationManagerCompat.OnActivated += OnToastActivated;

        try
        {
            await activationTask.Task.WaitAsync(TimeSpan.FromMinutes(1), cancellationToken);
            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
        finally
        {
            ToastNotificationManagerCompat.OnActivated -= OnToastActivated;
        }

        void OnToastActivated(ToastNotificationActivatedEventArgsCompat eventArgs)
        {
            if (eventArgs.Argument == "action=updateNow")
            {
                activationTask.TrySetResult();
            }
        }
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        // check for new version
        var newVersion = await _manager.CheckForUpdatesAsync();
        if (newVersion == null)
        {
            // no updates available
            return;
        }

        if (!await ShouldUpdateAsync(newVersion, cancellationToken))
        {
            return;
        }

        // download new version
        ShowProgressToast(newVersion);
        await _manager.DownloadUpdatesAsync(newVersion, UpdateProgressToast, cancelToken: cancellationToken);

        // install new version and restart app
        _manager.ApplyUpdatesAndRestart(newVersion);
    }

    internal class CheckForUpdatesJob(IUpdateSource updateSource) : IJob
    {
        public static ITrigger Trigger { get; } = TriggerBuilder.Create()
            .WithIdentity(nameof(CheckForUpdatesJob))
            .StartNow()
            .WithSimpleSchedule(x => x.WithIntervalInHours(4).RepeatForever())
            .Build();

        public async Task Execute(IJobExecutionContext context)
        {
            using var updateProcess = new UpdateProcess(updateSource);
            await updateProcess.RunAsync(context.CancellationToken);
        }

        private static async Task ScheduleAsync(IScheduler scheduler, CancellationToken cancellationToken)
            => await scheduler.ScheduleJob(CreateJob(), Trigger, cancellationToken);

        private static IJobDetail CreateJob()
            => JobBuilder.Create<CheckForUpdatesJob>()
                .DisallowConcurrentExecution()
                .Build();

        public class SelfScheduleService(ISchedulerFactory schedulerFactory) : IHostedService
        {
            public async Task StartAsync(CancellationToken cancellationToken)
            {
                var scheduler = await schedulerFactory.GetScheduler(cancellationToken);
                await ScheduleAsync(scheduler, cancellationToken);
            }

            public Task StopAsync(CancellationToken cancellationToken)
                => Task.CompletedTask;
        }
    }
}
