using System.Runtime.InteropServices;
using static Win32Image.Win32Framework.WindowsApi;

namespace Win32Image.Win32Framework.Structures;

[StructLayout(LayoutKind.Sequential)]
public struct WNDCLASS
{
    public ClassStyles style;
    [MarshalAs(UnmanagedType.FunctionPtr)]
    public WndProc lpfnWndProc;
    public int cbClsExtra;
    public int cbWndExtra;
    public IntPtr hInstance;
    public IntPtr hIcon;
    public IntPtr hCursor;
    public IntPtr hbrBackground;
    [MarshalAs(UnmanagedType.LPTStr)]
    public string lpszMenuName;
    [MarshalAs(UnmanagedType.LPTStr)]
    public string lpszClassName;
}
