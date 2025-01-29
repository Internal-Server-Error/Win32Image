using System.Runtime.InteropServices;

namespace Win32Image.Win32Framework.Structures;

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct MSG
{
    public IntPtr hwnd;
    public UInt32 message;
    public UIntPtr wParam;
    public UIntPtr lParam;
    public UInt32 time;
    public POINT pt;
}
