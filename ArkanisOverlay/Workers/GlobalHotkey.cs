using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Workers;

/**
 * Captures global hotkeys.
 * Based on: https://learn.microsoft.com/en-us/archive/blogs/toub/low-level-keyboard-hook-in-c
 */
public class GlobalHotkey : IDisposable
{
    private static HOOKPROC? _proc;
    private static HHOOK _hookId = HHOOK.Null;
    private ILogger Logger { get; }

    private Thread? _thread;

    /**
     *
     */
    public event EventHandler? TabKeyPressed;

    public GlobalHotkey(ILogger<GlobalHotkey> logger)
    {
        Logger = logger;
        _proc = HookProc;
    }

    public void Start()
    {
        _thread = new Thread(Run)
        {
            // ensures that the application can exit
            // regardless of this thread
            // see: https://learn.microsoft.com/en-us/dotnet/api/system.threading.thread.isbackground?view=net-9.0
            IsBackground = true
        };
        _thread.Start();
    }

    /**
     * Entry method for Thread.
     * Registers window event hooks and executes the message loop.
     */
    private void Run()
    {
        SetHook();

        // this thread needs a message loop
        // see: https://stackoverflow.com/a/2223270/4161937
        while (PInvoke.GetMessage(out var msg, HWND.Null, 0, 0))
        {
            PInvoke.TranslateMessage(in msg);
            PInvoke.DispatchMessage(in msg);
        }
    }

    private void SetHook()
    {
        var hook = PInvoke.SetWindowsHookEx(WINDOWS_HOOK_ID.WH_KEYBOARD_LL, _proc, HINSTANCE.Null, 0);
        if (hook == HHOOK.Null)
        {
            var errorCode = Marshal.GetLastWin32Error();
            Logger.LogWarning("Failed to set hook; {errorCode}", errorCode);
            return;
        }
        
        _hookId = hook;
    }

    private void Unhook()
    {
        if (_hookId == HHOOK.Null) return;

        var result = PInvoke.UnhookWindowsHookEx(_hookId);
        if (!result)
        {
            var errorCode = Marshal.GetLastWin32Error();
            Logger.LogWarning("Failed to unhook; {errorCode}", errorCode);
            return;
        }

        _hookId = HHOOK.Null;
    }

    private bool _isShiftDown;
    private bool _isAltDown;
    private bool _isSDown;

    /**
     * LowLevelKeyboardProc
     * See: See: https://learn.microsoft.com/en-us/windows/win32/winmsg/lowlevelkeyboardproc
     * @param nCode If nCode is less than zero, the hook procedure must pass the message to the CallNextHookEx function without further processing and should return the value returned by CallNextHookEx.
     * @param wparam The identifier of the keyboard message.
     * This parameter can be one of the following messages: WM_KEYDOWN, WM_KEYUP, WM_SYSKEYDOWN, or WM_SYSKEYUP.
     * @param lparam A pointer to a KBDLLHOOKSTRUCT structure.
     */
    private LRESULT HookProc(int nCode, WPARAM wparam, LPARAM lparam)
    {
        if (nCode >= 0)
        {
            var hookStruct = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lparam);

            switch ((VIRTUAL_KEY)hookStruct.vkCode)
            {
                case VIRTUAL_KEY.VK_LMENU:
                    if (wparam == PInvoke.WM_SYSKEYDOWN || wparam == PInvoke.WM_KEYDOWN)
                        _isAltDown = true;
                    if (wparam == PInvoke.WM_SYSKEYUP || wparam == PInvoke.WM_KEYUP)
                        _isAltDown = false;
                    break;
                case VIRTUAL_KEY.VK_LSHIFT:
                    if (wparam == PInvoke.WM_SYSKEYDOWN || wparam  == PInvoke.WM_KEYDOWN)
                        _isShiftDown = true;
                    if (wparam == PInvoke.WM_SYSKEYUP || wparam == PInvoke.WM_KEYUP)
                        _isShiftDown = false;
                    break;
                case VIRTUAL_KEY.VK_S:
                    if (wparam == PInvoke.WM_SYSKEYDOWN || wparam  == PInvoke.WM_KEYDOWN)
                        _isSDown = true;
                    if (wparam == PInvoke.WM_SYSKEYUP || wparam == PInvoke.WM_KEYUP)
                        _isSDown = false;
                    break;
            }
            
            if (_isSDown && _isShiftDown && _isAltDown)
            {
                TabKeyPressed?.Invoke(null, EventArgs.Empty);
            }
        }

        return PInvoke.CallNextHookEx(_hookId, nCode, wparam, lparam);
    }

    public void Dispose()
    {
        Unhook();
    }
}