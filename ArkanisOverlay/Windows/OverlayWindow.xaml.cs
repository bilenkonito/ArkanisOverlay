using System.Diagnostics;
using System.Windows;
using Windows.Win32;
using ArkanisOverlay.Workers;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using MudBlazor.Services;

namespace ArkanisOverlay.Windows;

/// <summary>
/// Interaction logic for OverlayWindow.xaml
/// </summary>
public partial class OverlayWindow : Window
{
    private readonly Thread _windowTrackerThread;
    
    public OverlayWindow()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(builder => builder.AddConsole());
        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddMudServices();
        
        WindowTracker windowTracker =
            new(
                // ugly temp "solution"
                new Logger<WindowTracker>(LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug))),
                Constants.WINDOW_CLASS,
                Constants.WINDOW_NAME
            );
        serviceCollection.AddSingleton(windowTracker);
        
        windowTracker.WindowPositionChanged += (sender, position) => Dispatcher.Invoke(() =>
        {
            Top = position.Y;
            Left = position.X;
        });
        
        windowTracker.WindowSizeChanged += (sender, size) => Dispatcher.Invoke(() =>
        {
            Width = size.Width;
            Height = size.Height;
        });

        _windowTrackerThread = new Thread(new ThreadStart(windowTracker.Start));
        _windowTrackerThread.Start();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        Resources.Add("services", serviceProvider);

        InitializeComponent();
    }

    private void Handle_UrlLoading(object sender, UrlLoadingEventArgs urlLoadingEventArgs)
    {
        if (urlLoadingEventArgs.Url.Host != "0.0.0.0")
        {
            urlLoadingEventArgs.UrlLoadingStrategy =
                UrlLoadingStrategy.OpenInWebView;
        }
    }

    private void MainWindow_Loaded(object? sender, RoutedEventArgs e)
    {
        blazorWebView.WebView.NavigationCompleted += WebView_Loaded;
        blazorWebView.WebView.CoreWebView2InitializationCompleted += CoreWebView_Loaded;
    }

    private void WebView_Loaded(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        // If we are running in a development/debugger mode, open dev tools to help out
        if (Debugger.IsAttached) blazorWebView.WebView.CoreWebView2.OpenDevToolsWindow();
    }

    private void CoreWebView_Loaded(object? sender, CoreWebView2InitializationCompletedEventArgs e)
    {
        // blazorWebView.WebView.CoreWebView2.SetVirtualHostNameToFolderMapping("local.data", "Data", CoreWebView2HostResourceAccessKind.Allow);
        // blazorWebView.WebView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
        // blazorWebView.WebView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
    }
}