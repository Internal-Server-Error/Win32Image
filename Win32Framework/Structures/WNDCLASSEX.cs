using System.Runtime.InteropServices;

namespace Win32Image.Win32Framework.Structures;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public struct WNDCLASSEX
{
    [MarshalAs(UnmanagedType.U4)]
    public int cbSize;
    [MarshalAs(UnmanagedType.U4)]
    public int style;
    public IntPtr lpfnWndProc;
    public int cbClsExtra;
    public int cbWndExtra;
    public IntPtr hInstance;
    public IntPtr hIcon;
    public IntPtr hCursor;
    public IntPtr hbrBackground;
    public string lpszMenuName;
    public string lpszClassName;
    public IntPtr hIconSm;
}
