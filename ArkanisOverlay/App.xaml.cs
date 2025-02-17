using System.Diagnostics;
using System.Windows;
using ArkanisOverlay.Data.UEX.API;
using ArkanisOverlay.Helpers;
using ArkanisOverlay.Windows;
using ArkanisOverlay.Workers;
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
        services.AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug).AddFilter((scope,
            _) => scope?.StartsWith("ArkanisOverlay") ?? false));
        services.AddWpfBlazorWebView();
        services.AddMudServices();
        services.AddSingleton<IServiceProvider>(sp => sp);
        services.AddHttpClient();

        // Windows
        services.AddSingleton<OverlayWindow>();

        // Views

        // ViewModels

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