using System.Diagnostics;
using System.IO;
using System.Windows;
using ArkanisOverlay.Data.Storage;
using ArkanisOverlay.UI.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SingleInstanceCore;

namespace ArkanisOverlay.UI;

/// <summary>
/// Interaction logic for Overlay.xaml
/// </summary>
public partial class App : ISingleInstance
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<App> _logger;

    // workaround to fix compiler error because of
    // generated Main method in `App.g.cs`
    private App() => throw new NotSupportedException();

    // ReSharper disable once UnusedMember.Global
    public App(ILogger<App> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

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

        // var serviceCollection = new ServiceCollection();
        // ConfigureServices(serviceCollection);
        // _serviceProvider = serviceCollection.BuildServiceProvider();
        Resources.Add("services", _serviceProvider);

        var overlayWindow = _serviceProvider.GetRequiredService<OverlayWindow>();
        Current.MainWindow = overlayWindow;

        // needs to be started manually because of DI
        // would normally be specified as `StartupUri` in
        // `App.xaml`
        overlayWindow.Show();
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
        _logger.LogInformation("Shutting down.");

        try
        {
            // Dispose of services if needed
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during shutdown.");
        }
    }
}