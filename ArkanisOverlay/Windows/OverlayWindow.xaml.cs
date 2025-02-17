using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Windows.Win32;
using Windows.Win32.Foundation;
using ArkanisOverlay.Helpers;
using ArkanisOverlay.Workers;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using Index = ArkanisOverlay.Pages.Overlay.Index;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

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
    private readonly BlurHelper _blurHelper;
    private readonly DataSync _dataSync;

    private HWND _currentWindowHWnd = HWND.Null;


    public OverlayWindow(ILogger<OverlayWindow> logger, WindowTracker windowTracker, GlobalHotkey globalHotkey,
        BlurHelper blurHelper, DataSync dataSync)
    {
        Instance = this;

        _logger = logger;
        _windowTracker = windowTracker;
        _globalHotkey = globalHotkey;
        _blurHelper = blurHelper;
        _dataSync = dataSync;

        SetupWorkerEventListeners();

        _blurHelper.EnableBlur(this, 1);

        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        _windowTracker.Start();
        _globalHotkey.Start();
        _dataSync.Start();
    }

    private void HideOverlay()
    {
        Collapse();
    }

    private void ShowOverlay()
    {
        var mainWindowHandle = (HWND)new WindowInteropHelper(this).Handle;
        var windowThreadProcessId = PInvoke.GetWindowThreadProcessId(PInvoke.GetForegroundWindow(), out _);
        var currentThreadId = PInvoke.GetCurrentThreadId();
        PInvoke.AttachThreadInput(windowThreadProcessId, currentThreadId, true);
        PInvoke.BringWindowToTop(mainWindowHandle);
        Show();
        // PInvoke.ShowWindow((HWND)MainWindowHandle, SHOW_WINDOW_CMD.SW_SHOW);
        PInvoke.AttachThreadInput(windowThreadProcessId, currentThreadId, false);

        // only works when window is visible
        _blurHelper.EnableBlur(this, 1);

        var result = Activate();
        _logger.LogDebug("ShowOverlay(): Activate Window: {result}", result);

        BlazorWebView.WebView.Focus();
        
        // blazorWebView.Focusable = true;
        // result = blazorWebView.Focus();
        // _logger.LogDebug("ShowOverlay(): Focus WebView: {result}", result);

        // Keyboard.Focus(blazorWebView);
        // Mouse.Capture(blazorWebView);

        // Index.SearchBox.FocusAsync();
        Index.SearchBox.SelectAsync();
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
        // _windowTracker.WindowFocusChanged += (_, isFocused) => Dispatcher.Invoke(() =>
        // {
        //     _logger.LogDebug("Overlay: WindowFocusChanged: {isFocused}", isFocused);
        //     _isStarCitizenFocussed = isFocused;
        //
        //     if (isFocused) return;
        //
        //     HideOverlay();
        // });

        _globalHotkey.TabKeyPressed += (_, _) => Dispatcher.Invoke(() =>
        {
            Console.WriteLine("Overlay: HotKeyPressed");

            if (Visibility == Visibility.Visible)
            {
                HideOverlay();
                return;
            }

            if (!_windowTracker.IsWindowFocused()) return;

            ShowOverlay();

            // if (Application.Current.MainWindow == null) return;
            //
            // // if (Application.Current.MainWindow == null) return;
            // Application.Current.MainWindow.Activate();
            // Application.Current.MainWindow.WindowState = WindowState.Normal;
            //
            // // Invert the topmost state twice to bring
            // // the window on top but keep the top most state
            // Application.Current.MainWindow.Topmost = !Application.Current.MainWindow.Topmost;
            // Application.Current.MainWindow.Topmost = !Application.Current.MainWindow.Topmost;
            //
            // Application.Current.MainWindow.Focusable = true;
            // Application.Current.MainWindow.Focus();
            //
            // blazorWebView.Focusable = true;
            // blazorWebView.Focus();
            // //
            // // HWND hWnd = (HWND)new WindowInteropHelper(Application.Current.MainWindow).Handle;
            // // var result = PInvoke.SetFocus(hWnd);
            // // _logger.LogDebug("Overlay: SetFocus: {result}", result.ToString());
            // // PInvoke.SetActiveWindow(hWnd);
            // //
            // // Keyboard.Focus(blazorWebView);
            // //
            // //
            // // if (Pages.Overlay.Index.SearchBox == null) return;
            // // Pages.Overlay.Index.SearchBox.AutoFocus = true;
            // Pages.Overlay.Index.SearchBox.FocusAsync();
            // Pages.Overlay.Index.SearchBox.SelectAsync();
        });
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        _logger.LogDebug("Overlay: KeyDown: {key}", e.Key);
        base.OnKeyDown(e);
        if (e.Key == Key.F)
        {
            BlazorWebView.WebView.Focus();
        }
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
        BlazorWebView.WebView.DefaultBackgroundColor = Color.Transparent;
        BlazorWebView.WebView.NavigationCompleted += WebView_Loaded;
        BlazorWebView.WebView.CoreWebView2InitializationCompleted += CoreWebView_Loaded;
        Visibility = Visibility.Collapsed;
    }

    private void WebView_Loaded(object? sender, CoreWebView2NavigationCompletedEventArgs e)
    {
        // If we are running in a development/debugger mode, open dev tools to help out
        // if (Debugger.IsAttached) blazorWebView.WebView.CoreWebView2.OpenDevToolsWindow();
        BlazorWebView.WebView.CoreWebView2.OpenDevToolsWindow();
        BlazorWebView.Focus();
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