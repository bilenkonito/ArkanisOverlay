using System.Diagnostics;
using System.Windows;
using ArkanisOverlay.Windows;
using ArkanisOverlay.Workers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace ArkanisOverlay;

/// <summary>
/// Interaction logic for Overlay.xaml
/// </summary>
public partial class App
{
    private IServiceProvider? _serviceProvider;
    protected override void OnStartup(StartupEventArgs e)
    {
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
        base.OnStartup(e);
        
        // needs to be started manually because of DI
        // would normally be specified as `StartupUri` in
        // `App.xaml`
        overlayWindow.Show();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug).AddFilter((scope,
            level) => scope?.StartsWith("ArkanisOverlay") ?? false));
        services.AddWpfBlazorWebView();
        services.AddMudServices();
        services.AddSingleton<IServiceProvider>(sp => sp);
        
        // Windows
        services.AddSingleton<OverlayWindow>();
        
        // Views
        
        // ViewModels
        
        // Services
        
        // Workers
        services.AddSingleton<WindowTracker>();
        services.AddSingleton<GlobalHotkey>();
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