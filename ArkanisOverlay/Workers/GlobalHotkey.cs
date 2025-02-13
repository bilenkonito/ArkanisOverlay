using System.Runtime.InteropServices;
using System.Windows.Input;
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
    private static HOOKPROC _proc;
    private static HHOOK _hookId = HHOOK.Null;
    private ILogger Logger { get; }

    /**
     *
     */
    public event EventHandler? TabKeyPressed;

    public GlobalHotkey(ILogger logger)
    {
        Logger = logger;
        _proc = HookProc;
    }

    /**
     * Entry method for Thread.
     * Registers hotkeys and starts message loop.
     */
    public void Start()
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
        };
        
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

    private bool isShiftDown = false;
    private bool isAltDown = false;

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
                    if (wparam == PInvoke.WM_SYSKEYDOWN) 
                        isAltDown = true;
                    if (wparam == PInvoke.WM_KEYUP)
                        isAltDown = false;
                    break;
                case VIRTUAL_KEY.VK_LSHIFT:
                    if (wparam == PInvoke.WM_SYSKEYDOWN)
                        isShiftDown = true;
                    if (wparam == PInvoke.WM_KEYUP)
                        isShiftDown = false;
                    break;
                case VIRTUAL_KEY.VK_S:
                    if (wparam == PInvoke.WM_SYSKEYDOWN)
                    {
                        if (isShiftDown && isAltDown)
                        {
                            TabKeyPressed?.Invoke(null, EventArgs.Empty);
                        }
                    }
                    break;
                // case VIRTUAL_KEY.VK_TAB:
                //     if (wparam == PInvoke.WM_KEYDOWN)
                //     {
                //         TabKeyPressed?.Invoke(null, EventArgs.Empty);
                //     }
                //     break;
            }
        }
        
        return PInvoke.CallNextHookEx(_hookId, nCode, wparam, lparam);
    }
    
    public void Dispose()
    {
        Unhook();
    }
}