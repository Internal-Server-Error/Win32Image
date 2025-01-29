using System.Runtime.InteropServices;

public static class MonitorInfo
{
    private const Int32 MONITOR_DEFAULTTONEAREST = 0x00000002;

    [DllImport("user32.dll")]
    private static extern IntPtr MonitorFromWindow(IntPtr handle, Int32 flags);

    [DllImport("user32.dll")]
    private static extern Boolean GetMonitorInfo(IntPtr hMonitor, NativeMonitorInfo lpmi);

    public static (int width, int height) GetMonitorInfo(nint windowHandle)
    {
        var monitor = MonitorFromWindow(windowHandle, MONITOR_DEFAULTTONEAREST);

        if (monitor != IntPtr.Zero)
        {
            var monitorInfo = new NativeMonitorInfo();
            GetMonitorInfo(monitor, monitorInfo);

            var width = monitorInfo.Monitor.Right - monitorInfo.Monitor.Left;
            var height = monitorInfo.Monitor.Bottom - monitorInfo.Monitor.Top;

            return (width, height);
        }
        return (800, 600);
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    private struct NativeRectangle
    {
        public Int32 Left;
        public Int32 Top;
        public Int32 Right;
        public Int32 Bottom;

        public NativeRectangle(Int32 left, Int32 top, Int32 right, Int32 bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private sealed class NativeMonitorInfo
    {
        public Int32 Size = Marshal.SizeOf(typeof(NativeMonitorInfo));
        public NativeRectangle Monitor;
        public NativeRectangle Work;
        public Int32 Flags;
    }


}
