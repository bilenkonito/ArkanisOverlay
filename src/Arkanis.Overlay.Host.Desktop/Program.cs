namespace Arkanis.Overlay.Host.Desktop;

using System.Globalization;
using System.IO;
using Common;
using Common.Abstractions;
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
using Serilog;
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
    [STAThread]
    public static async Task Main(string[] args)
    {
        await HandleInstallationBehaviourAsync(args);

        var hostBuilder = Host.CreateDefaultBuilder(args)
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
            var host = hostBuilder.Build();
            await host.MigrateDatabaseAsync<OverlayDbContext>().ConfigureAwait(false);
            await host.RunAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly");
            await Console.Error.WriteLineAsync($"An error occurred during app startup: {ex.Message}");
            throw;
        }
    }

    private static async Task HandleInstallationBehaviourAsync(string[] args)
    {
        using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder.AddSerilog());
        var logger = loggerFactory.CreateLogger(typeof(Program));
        logger.LogDebug("Running velopack with args: '{Args}'", string.Join("', '", args));

        var userPreferenceDefaults = new UserPreferences();
        VelopackApp.Build()
            .SetArgs(args)
            .WithFirstRun(_ => WindowsNotifications.ShowWelcomeToast(userPreferenceDefaults))
            .WithAfterUpdateFastCallback(WindowsNotifications.ShowUpdatedToast)
            .Run();

        var updateChannel = userPreferenceDefaults.UpdateChannel;
        var updateSource = UpdateHelper.CreateSourceFor(updateChannel);
        var updateManagerLogger = loggerFactory.CreateLogger<ArkanisOverlayUpdateManager>();
        var updateManager = new ArkanisOverlayUpdateManager(updateSource, updateManagerLogger);
        using var windowsNotifications = new WindowsNotifications();

        logger.LogDebug("Loading updates for channel: {UpdateChannel}", updateChannel);
        var updateProcessLogger = loggerFactory.CreateLogger<UpdateProcess>();
        using var update = new UpdateProcess(updateManager, windowsNotifications, updateProcessLogger);
        await update.RunAsync(true, CancellationToken.None);
    }

    private static IHostBuilder ConfigureServices(this IHostBuilder builder)
        => builder.ConfigureServices((context, services) =>
            {
                services.AddSerilog(loggerConfig => loggerConfig
                    .WriteTo.Console(
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}) {Message:lj}{NewLine}{Exception}",
                        formatProvider: CultureInfo.InvariantCulture
                    )
                    .WriteTo.File(
                        Path.Join(ApplicationConstants.LocalAppDataPath, "logs", "app.log"),
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] ({SourceContext}) {Message:lj}{NewLine}{Exception}",
                        buffered: true,
                        rollingInterval: RollingInterval.Day,
                        retainedFileCountLimit: 7,
                        formatProvider: CultureInfo.InvariantCulture
                    )
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(context.Configuration)
                );

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

                services.AddGoogleTrackingServices()
                    .AddSingleton<SharedAnalyticsPropertyProvider, DesktopAnalyticsPropertyProvider>();

                services.AddGlobalKeyboardProxyService();
                services.AddJavaScriptEventInterop();
                services.AddSingleton(typeof(WindowProvider<>));

                services.AddHostedService<WindowsAutoStartManager>()
                    .AddSingleton<ISystemAutoStartStateProvider, WindowsAutoStartStateProvider>();

                services.AddSingleton<WindowsNotifications>();

                // Auto updater
                services
                    .AddSingleton<IUpdateSource>(provider
                        => UpdateHelper.CreateSourceFor(provider.GetRequiredService<IUserPreferencesProvider>().CurrentPreferences.UpdateChannel)
                    )
                    .AddSingleton<UpdateOptions>(provider => new UpdateOptions
                        {
                            AllowVersionDowngrade = false,
                            ExplicitChannel = provider.GetRequiredService<IUserPreferencesProvider>().CurrentPreferences.UpdateChannel.VelopackChannelId,
                        }
                    )
                    .AddSingleton<ArkanisOverlayUpdateManager>(provider => ActivatorUtilities.CreateInstance<ArkanisOverlayUpdateManager>(provider))
                    .AddSingleton<IAppVersionProvider, VelopackAppVersionProvider>()
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
}
