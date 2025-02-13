using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Windows.Win32.Foundation;
using Microsoft.Extensions.Logging;

namespace ArkanisOverlay.Helpers;

/// <summary>
/// Helper class for setting window composition attributes to enable blur effects.
/// Based on: https://github.com/riverar/sample-win32-acrylicblur/blob/917adc277c7258307799327d12262ebd47fd0308/MainWindow.xaml.cs
/// </summary>
public class BlurHelper(ILogger logger)
{
    [DllImport("user32.dll")]
    internal static extern BOOL SetWindowCompositionAttribute(IntPtr hWnd, ref WindowCompositionAttributeData data);
    
    private uint _blurOpacity;
    public double BlurOpacity
    {
        get => _blurOpacity;
        set => _blurOpacity = (uint)value;
    }
    
    private uint _blurBackgroundColor = 0x990000; /* BGR color format */
    
    internal void EnableBlur(Window window, double blurOpacity = 0)
    {
        BlurOpacity = blurOpacity;
        var windowHelper = new WindowInteropHelper(window);

        var accent = new AccentPolicy();
        accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
        accent.GradientColor = (_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF);

        var accentStructSize = Marshal.SizeOf(accent);

        var accentPtr = Marshal.AllocHGlobal(accentStructSize);
        Marshal.StructureToPtr(accent, accentPtr, false);

        var data = new WindowCompositionAttributeData
        {
            Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
            SizeOfData = accentStructSize,
            Data = accentPtr
        };

        var result = SetWindowCompositionAttribute(windowHelper.Handle, ref data);

        if (!result)
        {
            var errorCode = Marshal.GetLastWin32Error();
            logger.LogWarning("Failed to set window composition attribute; {errorCode}", errorCode);
        }

        Marshal.FreeHGlobal(accentPtr);
    }
}

internal enum AccentState
{
    ACCENT_DISABLED = 0,
    ACCENT_ENABLE_GRADIENT = 1,
    ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
    ACCENT_ENABLE_BLURBEHIND = 3,
    ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
    ACCENT_INVALID_STATE = 5
}

[StructLayout(LayoutKind.Sequential)]
internal struct AccentPolicy
{
    public AccentState AccentState;
    public uint AccentFlags;
    public uint GradientColor;
    public uint AnimationId;
}

[StructLayout(LayoutKind.Sequential)]
internal struct WindowCompositionAttributeData
{
    public WindowCompositionAttribute Attribute;
    public IntPtr Data;
    public int SizeOfData;
}

internal enum WindowCompositionAttribute
{
    // ...
    WCA_ACCENT_POLICY = 19
    // ...
}