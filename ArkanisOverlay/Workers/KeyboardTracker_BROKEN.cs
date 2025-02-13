using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Workers;

public class KeyboardTracker
{
    private ILogger Logger { get; }

    /**
     *
     */
    public event EventHandler? TabKeyPressed;

    public KeyboardTracker(ILogger logger)
    {
        Logger = logger;
    }

    /**
     * Entry method for Thread.
     * Registers hotkeys and starts message loop.
     */
    public void Start()
    {
        // register hotkeys
        var result = PInvoke.RegisterHotKey(
            HWND.Null,
            0x0000,
            (HOT_KEY_MODIFIERS.MOD_ALT | HOT_KEY_MODIFIERS.MOD_SHIFT),
            (uint)VIRTUAL_KEY.VK_TAB
        );
        
        if (result)
        {
            Logger.LogInformation("Hotkey registered.");
        }
        else
        {
            var errorCode = Marshal.GetLastWin32Error();
            Logger.LogWarning("Failed to register hotkey; {errorCode}", errorCode);
            return;
        }

        // this thread needs a message loop
        // see: https://stackoverflow.com/a/2223270/4161937
        while (PInvoke.GetMessage(out var msg, HWND.Null, 0, 0))
        {
            if (msg.message == PInvoke.WM_HOTKEY && msg.wParam == 0)
            {
                Logger.LogDebug("Tab key pressed.");
                TabKeyPressed?.Invoke(this, EventArgs.Empty);
            }

            if (msg.message == PInvoke.WM_KEYDOWN)
            {
                Logger.LogDebug("Key pressed: {msg.wParam}", msg.wParam);
            }

            PInvoke.TranslateMessage(in msg);
            PInvoke.DispatchMessage(in msg);
        }
    }
}