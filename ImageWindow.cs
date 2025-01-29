using System.ComponentModel;
using System.Runtime.InteropServices;
using Win32Image.Win32Framework;
using Win32Image.Win32Framework.Structures;
using static Win32Image.Win32Framework.WindowsApi;

namespace Win32Image;

public static class ImageWindow
{
    private static UInt16 atom;
    private static string windowTitle = String.Empty;
    private static string imagePath = String.Empty;

    private static nint? hBitmap = null;
    private static nint? hdcSource = null;

    public static int Open(string imagePath)
    {
        windowTitle = imagePath;
        ImageWindow.imagePath = imagePath;

        IntPtr hinstance = System.Diagnostics.Process.GetCurrentProcess().Handle;
        int nCmdShow = (int)ShowWindowCommands.Normal;

        if (!InitApplication(hinstance) || !InitInstance(hinstance, nCmdShow, windowTitle))
        {
            return 1;
        }

        sbyte hasMessage = WindowsApi.GetMessage(out var msg, IntPtr.Zero, 0, 0);

        while (hasMessage != 0 && hasMessage != -1)
        {
            WindowsApi.TranslateMessage(ref msg);
            WindowsApi.DispatchMessage(ref msg);
            hasMessage = WindowsApi.GetMessage(out msg, IntPtr.Zero, 0, 0);
        }

        return 0;
    }

    private static bool InitApplication(IntPtr hinstance)
    {
        WNDCLASSEX wcx = new WNDCLASSEX();

        wcx.cbSize = Marshal.SizeOf(wcx);
        wcx.style = (int)(ClassStyles.VerticalRedraw | ClassStyles.HorizontalRedraw);
        wcx.lpfnWndProc = Marshal.GetFunctionPointerForDelegate((Delegate)(WndProc)MainWindowProcess);
        wcx.cbClsExtra = 0;
        wcx.cbWndExtra = 0;
        wcx.hInstance = hinstance;
        wcx.hIcon = WindowsApi.LoadIcon(IntPtr.Zero, new IntPtr((int)SystemIcons.IDI_APPLICATION));
        wcx.hCursor = WindowsApi.LoadCursor(IntPtr.Zero, (int)Win32_IDC_Constant.IDC_ARROW);
        wcx.hbrBackground = WindowsApi.GetStockObject(StockObjects.WHITE_BRUSH);
        wcx.lpszMenuName = "MainMenu";
        wcx.lpszClassName = "MainWClass";

        UInt16 ret = WindowsApi.RegisterClassEx(ref wcx);
        if (ret != 0)
        {
            string message = new Win32Exception(Marshal.GetLastWin32Error()).Message;
            Console.WriteLine("Failed to call RegisterClasEx, error = {0}", message);
        }
        atom = ret;
        return ret != 0;
    }

    private static bool InitInstance(IntPtr hInstance, int nCmdShow, string windowTitle)
    {
        IntPtr hwnd;

        hwnd = WindowsApi.CreateWindowEx(
            0,
            atom,
            windowTitle,
            WindowStyles.WS_OVERLAPPEDWINDOW,
            Win32_CW_Constant.CW_USEDEFAULT,
            Win32_CW_Constant.CW_USEDEFAULT,
            Win32_CW_Constant.CW_USEDEFAULT,
            Win32_CW_Constant.CW_USEDEFAULT,
            IntPtr.Zero,
            IntPtr.Zero,
            hInstance,
            IntPtr.Zero);
        if (hwnd == IntPtr.Zero)
        {
            string error = new Win32Exception(Marshal.GetLastWin32Error()).Message;
            Console.WriteLine("Failed to InitInstance , error = {0}", error);
            return false;
        }
        WindowsApi.ShowWindow(hwnd, (ShowWindowCommands)nCmdShow);
        WindowsApi.UpdateWindow(hwnd);
        return true;
    }

    private static IntPtr MainWindowProcess(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        switch ((WindowsMessage)msg)
        {
            case WindowsMessage.CREATE:
                hBitmap = WindowsApi.LoadImage(hWnd, imagePath, ImageConstant.IMAGE_BITMAP, 0, 0, ImageConstant.LR_LOADFROMFILE);
                hdcSource = CreateCompatibleDC(GetDC(0));
                if (hdcSource == null || hBitmap == null)
                {
                    Console.Write("Failed to load image.");
                    throw new Exception("Failed to load image.");
                }
                WindowsApi.SelectObject(hdcSource.Value, hBitmap.Value);
                break;
            case WindowsMessage.PAINT:
                if (hdcSource.HasValue)
                {
                    var hdc = WindowsApi.BeginPaint(hWnd, out var ps);
                    var monitorSize = MonitorInfo.GetMonitorInfo(hWnd);
                    WindowsApi.BitBlt(hdc, 0, 0, monitorSize.width, monitorSize.height, hdcSource.Value, 0, 0, (int)TernaryRasterOperations.SRCCOPY);
                    WindowsApi.EndPaint(hWnd, ref ps);
                }
                break;

            case WindowsMessage.DESTROY:
                if (hBitmap.HasValue)
                {
                    WindowsApi.DeleteObject(hBitmap.Value);
                }
                WindowsApi.PostQuitMessage(0);
                return IntPtr.Zero;
        }

        return WindowsApi.DefWindowProc(hWnd, (WindowsMessage)msg, wParam, lParam);
    }

}
