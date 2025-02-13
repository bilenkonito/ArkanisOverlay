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
    // private readonly Thread _keyboardTrackerThread;
    private readonly Thread _globalHotKeyThread;
    
    private bool IsStarCitizenFocussed = false;

    public OverlayWindow()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddLogging(builder => builder.AddConsole());
        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddMudServices();

        WindowTracker windowTracker =
            new(
                // ugly temp "solution"
                new Logger<WindowTracker>(LoggerFactory.Create(builder =>
                    builder.AddConsole().SetMinimumLevel(LogLevel.Debug))),
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

        windowTracker.WindowFocusChanged += (sender, isFocused) => Dispatcher.Invoke(() =>
        {
            Console.WriteLine("Overlay: WindowFocusChanged: {0}", isFocused);
            IsStarCitizenFocussed = isFocused;
            
            if (isFocused) return;
            
            Visibility = Visibility.Collapsed;
            // Topmost = isFocused;
        });

        _windowTrackerThread = new Thread(windowTracker.Start);
        _windowTrackerThread.Start();

        // KeyboardTracker keyboardTracker =
        //     new(
        //         // ugly temp "solution"
        //         new Logger<KeyboardTracker>(LoggerFactory.Create(builder =>
        //             builder.AddConsole().SetMinimumLevel(LogLevel.Debug)))
        //     );
        // serviceCollection.AddSingleton(keyboardTracker);
        //
        // keyboardTracker.TabKeyPressed += (sender, args) => Dispatcher.Invoke(() =>
        // {
        //     if (!IsStarCitizenFocussed) return;
        //     
        //     Visibility = Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        // });
        //
        // _keyboardTrackerThread = new Thread(keyboardTracker.Start);
        // _keyboardTrackerThread.Start();
        
        GlobalHotkey globalHotkey =
            new(
                // ugly temp "solution"
                new Logger<GlobalHotkey>(LoggerFactory.Create(builder =>
                    builder.AddConsole().SetMinimumLevel(LogLevel.Debug)))
            );
        serviceCollection.AddSingleton(globalHotkey);

        globalHotkey.TabKeyPressed += (sender, args) => Dispatcher.Invoke(() =>
        {
            Console.WriteLine("Overlay: TabKeyPressed");
            
            if (!IsStarCitizenFocussed) return;

            Visibility = Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        });
        
        _globalHotKeyThread = new Thread(globalHotkey.Start);
        _globalHotKeyThread.Start();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        Resources.Add("services", serviceProvider);

        Visibility = Visibility.Collapsed;
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
        Visibility = Visibility.Collapsed;
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