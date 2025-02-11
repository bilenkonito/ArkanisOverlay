using Windows.Win32.Foundation;

namespace Windows.Win32;

partial class PInvoke
{
    /**
     * Workaround for unsafe pointer access.
     * https://github.com/microsoft/CsWin32/issues/137#issuecomment-1879493081
     */
    internal static unsafe uint GetWindowThreadProcessId(HWND hWnd, out uint processId)
    {
        fixed (uint* lpdwProcessId = &processId)
            return GetWindowThreadProcessId(hWnd, lpdwProcessId);
    }
}