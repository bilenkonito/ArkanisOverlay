using System.Diagnostics;
using System.IO;
using System.Windows;
using ArkanisOverlay.Data.Storage;
using ArkanisOverlay.Data.UEX.API;
using ArkanisOverlay.Helpers;
using ArkanisOverlay.Windows;
using ArkanisOverlay.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using SingleInstanceCore;

namespace ArkanisOverlay;

/// <summary>
/// Interaction logic for Overlay.xaml
/// </summary>
public partial class App : ISingleInstance
{
    private IServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var isFirstInstance = this.InitializeAsFirstInstance(Constants.INSTANCE_ID);
        if (!isFirstInstance)
        {
            Shutdown();
            return;
        }

        if (!Debugger.IsAttached)
        {
            var splashScreen = new SplashScreen("Resources\\ArkanisTransparent_512x512.png");
            splashScreen.Show(true, true);
        }
        
        if (!Directory.Exists(Constants.LocalAppDataPath))
        {
            Directory.CreateDirectory(Constants.LocalAppDataPath);
        }

        using (var context = new UEXContext())
        {
            context.Database.Migrate();
        }

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
        Resources.Add("services", _serviceProvider);

        var overlayWindow = _serviceProvider.GetRequiredService<OverlayWindow>();
        Current.MainWindow = overlayWindow;

        // needs to be started manually because of DI
        // would normally be specified as `StartupUri` in
        // `App.xaml`
        overlayWindow.Show();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        services.AddScoped<IConfiguration>(_ => configuration);
        
        services.AddLogging(builder
            => builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Debug)
                .AddFilter(
                    (scope, _)
                        => scope?.StartsWith("ArkanisOverlay") ?? false));

        services
            .AddOptions<ConfigurationOptions>()
            .BindConfiguration(ConfigurationOptions.Section)
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddWpfBlazorWebView();
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.NewestOnTop = true;
            config.SnackbarConfiguration.MaxDisplayedSnackbars = 1;
        });
        services.AddSingleton<IServiceProvider>(sp => sp);
        services.AddHttpClient();

        // Windows
        services.AddSingleton<OverlayWindow>();

        // Views

        // ViewModels
        
        // Data
        services.AddSingleton<UEXContext>();

        // Services
        services.AddSingleton<BlurHelper>();
        services.AddSingleton<Client>();

        // Workers
        services.AddSingleton<WindowTracker>();
        services.AddSingleton<GlobalHotkey>();
        services.AddSingleton<DataSync>();
    }

    public void OnInstanceInvoked(string[] args)
    {
        SingleInstance.Cleanup();

        if (Current.MainWindow == null)
        {
            return;
        }

        // from RatScanner - https://github.com/RatScanner/RatScanner
        Current.MainWindow.Activate();
        Current.MainWindow.WindowState = WindowState.Normal;

        // Invert the topmost state twice to bring
        // the window on top but keep the top most state
        Current.MainWindow.Topmost = !Current.MainWindow.Topmost;
        Current.MainWindow.Topmost = !Current.MainWindow.Topmost;
    }

    protected override void OnExit(ExitEventArgs exitEventArgs)
    {
        // Dispose of services if needed
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}