﻿namespace Arkanis.Overlay.Host.Desktop.UI;

using System.Diagnostics;
using System.Windows;
using Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

            //? Splash Screen with fixed delay
            // splashScreen.Show(false, true);
            // close after 2 seconds()
            // Task.Delay(2000).ContinueWith(_ => splashScreen.Close(TimeSpan.FromSeconds(0.5)));
        }

        Resources.Add("services", _serviceProvider);

        var overlayWindow = _serviceProvider.GetRequiredService<OverlayWindow>();
        Current.MainWindow = overlayWindow;

        // needs to be started manually because of DI
        // would normally be specified as `StartupUri` in
        // `App.xaml`
        overlayWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _logger.LogInformation("Shutting down");

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
            _logger.LogError(ex, "Error during shutdown");
        }

        base.OnExit(e);
        _hostApplicationLifetime.StopApplication();
    }
}
