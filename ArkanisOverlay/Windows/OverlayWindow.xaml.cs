using System.Diagnostics;
using System.Windows;
using Windows.Win32;
using Windows.Win32.Foundation;
using ArkanisOverlay.Workers;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;

namespace ArkanisOverlay.Windows;

/// <summary>
/// Interaction logic for OverlayWindow.xaml
/// </summary>
public partial class OverlayWindow
{
    public static OverlayWindow? Instance { get; private set; }

    private readonly ILogger _logger;
    private readonly WindowTracker _windowTracker;
    private readonly GlobalHotkey _globalHotkey;

    private bool _isStarCitizenFocussed;
    private HWND _currentWindowHWnd = HWND.Null;
    

    public OverlayWindow(ILogger<OverlayWindow> logger, WindowTracker windowTracker, GlobalHotkey globalHotkey)
    {
        Instance = this;

        _logger = logger;
        _windowTracker = windowTracker;
        _globalHotkey = globalHotkey;

        SetupWorkerEventListeners();
        
        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        
        _windowTracker.Start();
        _globalHotkey.Start();
    }

    private void SetupWorkerEventListeners()
    {
        _windowTracker.WindowFound += (_, hWnd) => Dispatcher.Invoke(() => { _currentWindowHWnd = hWnd; });
        _windowTracker.WindowLost += (_, _) => Dispatcher.Invoke(() => { _currentWindowHWnd = HWND.Null; });
        _windowTracker.WindowPositionChanged += (_, position) => Dispatcher.Invoke(() =>
        {
            _logger.LogDebug("Overlay: WindowPositionChanged: {position}", position.ToString());
            Top = position.Y;
            Left = position.X;
        });
        _windowTracker.WindowSizeChanged += (_, size) => Dispatcher.Invoke(() =>
        {
            _logger.LogDebug("Overlay: WindowSizeChanged: {size}", size.ToString());
            Width = size.Width;
            Height = size.Height;
        });
        _windowTracker.WindowFocusChanged += (_, isFocused) => Dispatcher.Invoke(() =>
        {
            _logger.LogDebug("Overlay: WindowFocusChanged: {isFocused}", isFocused);
            _isStarCitizenFocussed = isFocused;

            if (isFocused) return;

            Visibility = Visibility.Collapsed;
        });

        _globalHotkey.TabKeyPressed += (_, _) => Dispatcher.Invoke(() =>
        {
            Console.WriteLine("Overlay: HotKeyPressed");

            if (!_isStarCitizenFocussed) return;

            Visibility = Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        });
    }

    // private void Handle_UrlLoading(object sender, UrlLoadingEventArgs urlLoadingEventArgs)
    // {
    //     if (urlLoadingEventArgs.Url.Host != "0.0.0.0")
    //     {
    //         urlLoadingEventArgs.UrlLoadingStrategy =
    //             UrlLoadingStrategy.OpenInWebView;
    //     }
    // }

    private void MainWindow_Loaded(object? sender, RoutedEventArgs e)
    {
        blazorWebView.WebView.DefaultBackgroundColor = Color.Transparent;
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

    /// <summary>
    /// Interface to hide the overlay from JS.
    /// Collapses the window. This will hide the window and give focus back to Star Citizen.
    /// </summary>
    public void Collapse()
    {
        Visibility = Visibility.Collapsed;
        // we switch focus back to Star Citizen because
        // otherwise the previously active window will
        // receive focus instead for some reason
        if (_currentWindowHWnd == HWND.Null) return;
        PInvoke.SetForegroundWindow(_currentWindowHWnd);
    }
}