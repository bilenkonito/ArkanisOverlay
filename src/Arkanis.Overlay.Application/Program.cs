namespace Arkanis.Overlay.Application;

using System.Data.Common;
using Common;
using Common.Extensions;
using Components.Helpers;
using Components.Services;
using Dapplo.Microsoft.Extensions.Hosting.AppServices;
using Dapplo.Microsoft.Extensions.Hosting.Wpf;
using Domain.Abstractions.Services;
using Domain.Options;
using Helpers;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Data.Extensions;
using Infrastructure.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using NuGet.Versioning;
using Services;
using Services.Factories;
using UI;
using UI.Windows;
using Velopack;
using Velopack.Sources;
using Workers;

// based on:
// https://github.com/dapplo/Dapplo.Microsoft.Extensions.Hosting/blob/master/samples/Dapplo.Hosting.Sample.WpfDemo/Program.cs#L48
public static class Program
{
    private static readonly IUpdateSource UpdateSource = new GithubSource(ApplicationConstants.GitHubRepositoryUrl, null, false);

    [STAThread]
    public static async Task Main(string[] args)
    {
        HandleInstallationBehaviour();

        var hostBuilder = Host.CreateDefaultBuilder(args)
            .ConfigureLogging()
            .ConfigureSingleInstance(options =>
                {
                    options.MutexId = $"{{{Constants.InstanceId}}}";
                    options.WhenNotFirstInstance = (environment, logger) =>
                    {
                        logger.LogInformation("{AppName} is already running", environment.ApplicationName);
                    };
                }
            )
            .ConfigureServices()
            .ConfigureWpf(options =>
                {
                    options.UseApplication<App>();

                    // Windows will be registered as singletons
                    options.UseWindow<OverlayWindow>();
                }
            )
            .UseWpfLifetime()
            .UseConsoleLifetime();

        try
        {
            ApplicationConstants.LocalAppDataDir.Create();
            var host = hostBuilder.Build();

            try
            {
                await host.MigrateDatabaseAsync<OverlayDbContext>().ConfigureAwait(false);
            }
            catch (DbException ex)
            {
                await Console.Error.WriteLineAsync($"Encountered a database error during migration: {ex.Message}");
                await Console.Error.WriteLineAsync($"Trying auto-recovery by deleting default appdata directory at {ApplicationConstants.LocalAppDataDir}");

                ApplicationConstants.LocalAppDataDir.Delete(true);
                ApplicationConstants.LocalAppDataDir.Create();
                await host.MigrateDatabaseAsync<OverlayDbContext>().ConfigureAwait(false);
            }

            await host.RunAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"An error occurred during app startup: {ex.Message}");
            throw;
        }
    }

    private static void HandleInstallationBehaviour()
    {
        var userPreferenceDefaults = new UserPreferences();
        VelopackApp.Build()
            .WithFirstRun(WithFirstRun)
            .WithAfterUpdateFastCallback(WindowsNotifications.ShowUpdatedToast)
            .Run();

        return;

        void WithFirstRun(SemanticVersion _)
        {
            WindowsNotifications.ShowWelcomeToast(userPreferenceDefaults);
            var updateManager = new UpdateManager(UpdateSource);
            using var windowsNotifications = new WindowsNotifications();
            using var update = new UpdateProcess(updateManager, windowsNotifications);
            update.RunAsync(true, CancellationToken.None).GetAwaiter().GetResult();
        }
    }

    private static IHostBuilder ConfigureLogging(this IHostBuilder hostBuilder)
        => hostBuilder.ConfigureLogging((hostContext, configLogging) =>
            configLogging
                .AddConfiguration(hostContext.Configuration.GetSection("Logging"))
                .AddConsole()
                .AddDebug()
                .SetMinimumLevel(LogLevel.Debug)
                .AddFilter((scope, _) => scope?.StartsWith(nameof(Arkanis), StringComparison.InvariantCulture) ?? false)
        );

    private static IHostBuilder ConfigureServices(this IHostBuilder builder)
        => builder.ConfigureServices((context, services) =>
            {
                services.AddInfrastructureConfiguration(context.Configuration);

                services.AddWpfBlazorWebView();
                services.AddMudServices(config =>
                    {
                        config.SnackbarConfiguration.NewestOnTop = true;
                        config.SnackbarConfiguration.MaxDisplayedSnackbars = 1;
                    }
                );
                services.AddSingleton<IServiceProvider>(sp => sp);
                services.AddHttpClient();

                services.AddGlobalKeyboardProxyService();
                services.AddJavaScriptEventInterop();
                services.AddGoogleTrackingServices();
                services.AddSingleton(typeof(WindowProvider<>));

                services.AddHostedService<WindowsAutoStartManager>()
                    .AddSingleton<ISystemAutoStartStateProvider, WindowsAutoStartStateProvider>();

                services.AddSingleton<WindowsNotifications>();

                // Auto updater
                services
                    .AddSingleton<IUpdateSource>(_ => UpdateSource)
                    .AddSingleton<UpdateOptions>(provider => new UpdateOptions
                        {
                            AllowVersionDowngrade = false,
                            ExplicitChannel = provider.GetRequiredService<IUserPreferencesProvider>().CurrentPreferences.UpdateChannel.VelopackChannelId,
                        }
                    )
                    .AddSingleton<UpdateManager>(provider => ActivatorUtilities.CreateInstance<UpdateManager>(provider))
                    .AddHostedService<UpdateProcess.CheckForUpdatesJob.SelfScheduleService>();

                // Data
                services
                    .AddWindowsOverlayControls()
                    .AddPreferenceServiceCollection()
                    .AddInfrastructure();

                // Singleton Services
                services.AddSingleton<BlurHelper>();
                services.AddMemoryCache();

                // Factories
                services.AddSingleton<PreferencesWindowFactory>();

                // Workers
                services.AddSingleton<WindowTracker>()
                    .Alias<IHostedService, WindowTracker>();

                services.AddSingleton<GlobalHotkey>()
                    .Alias<IHostedService, GlobalHotkey>();
            }
        );

    private static async Task Update()
    {
        var mgr = new UpdateManager(new GithubSource("https://github.com/ArkanisCorporation/ArkanisOverlay", null, false));

        // check for new version
        var newVersion = await mgr.CheckForUpdatesAsync();
        if (newVersion == null)
        {
            // no updates available
            return;
        }

        // download new version
        await mgr.DownloadUpdatesAsync(newVersion);

        // install new version and restart app
        mgr.ApplyUpdatesAndRestart(newVersion);
    }
}
