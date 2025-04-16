namespace Arkanis.Overlay.Application.UI;

using Windows;
using Data.Contexts;

/// <summary>
///     Interaction logic for Overlay.xaml
/// </summary>
public partial class App
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger<App> _logger;
    private readonly IServiceProvider _serviceProvider;


    // workaround to fix compiler error because of
    // generated Main method in `App.g.cs`
    private App()
        => throw new NotSupportedException();

    // ReSharper disable once UnusedMember.Global
    public App(ILogger<App> logger, IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _hostApplicationLifetime = hostApplicationLifetime;
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
        this.Resources.Add("services", _serviceProvider);

        var overlayWindow = _serviceProvider.GetRequiredService<OverlayWindow>();
        Current.MainWindow = overlayWindow;

        // needs to be started manually because of DI
        // would normally be specified as `StartupUri` in
        // `App.xaml`
        overlayWindow.Show();
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

        base.OnExit(exitEventArgs);
        _hostApplicationLifetime.StopApplication();
    }
}
