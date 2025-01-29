using System.ComponentModel;
using System.Runtime.InteropServices;
using Win32Image.Win32Framework;
using Win32Image.Win32Framework.Structures;
using static Win32Image.Win32Framework.WindowsApi;

namespace Win32Image;

public static class ErrorWindow
{
    private static UInt16 atom;

    private static string DisplayText = String.Empty;

    public static int Open(string message)
    {
        DisplayText = message;
        IntPtr hinstance = System.Diagnostics.Process.GetCurrentProcess().Handle;
        int nCmdShow = (int)ShowWindowCommands.Normal;

        MSG msg;

        if (!InitApplication(hinstance))
            return 1;

        if (!InitInstance(hinstance, nCmdShow))
            return 1;

        sbyte hasMessage;

        while ((hasMessage = WindowsApi.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0 && hasMessage != -1)
        {
            WindowsApi.TranslateMessage(ref msg);
            WindowsApi.DispatchMessage(ref msg);
        }
        return 0;
    }

    private static bool InitApplication(IntPtr hinstance)
    {

        WNDCLASSEX wcx = new WNDCLASSEX();

        wcx.cbSize = Marshal.SizeOf(wcx);
        wcx.style = (int)(ClassStyles.VerticalRedraw | ClassStyles.HorizontalRedraw);


        IntPtr address2 = Marshal.GetFunctionPointerForDelegate((Delegate)(WndProc)MainWndProc);
        wcx.lpfnWndProc = address2;


        wcx.cbClsExtra = 0;
        wcx.cbWndExtra = 0;
        wcx.hInstance = hinstance;
        wcx.hIcon = WindowsApi.LoadIcon(
                IntPtr.Zero, new IntPtr((int)SystemIcons.IDI_APPLICATION));
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

    private static bool InitInstance(IntPtr hInstance, int nCmdShow)
    {
        IntPtr hwnd;

        hwnd = WindowsApi.CreateWindowEx(
            0,
            atom,
            "Error",
            WindowStyles.WS_OVERLAPPEDWINDOW,
            Win32_CW_Constant.CW_USEDEFAULT,
            Win32_CW_Constant.CW_USEDEFAULT,
            800,
            600,
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

    private static IntPtr MainWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        IntPtr hdc;
        PAINTSTRUCT ps;
        RECT rect;
        switch ((WindowsMessage)msg)
        {
            case WindowsMessage.PAINT:
                hdc = WindowsApi.BeginPaint(hWnd, out ps);
                WindowsApi.GetClientRect(hWnd, out rect);
                WindowsApi.DrawText(hdc, DisplayText, -1, ref rect, Win32_DT_Constant.DT_SINGLELINE | Win32_DT_Constant.DT_CENTER | Win32_DT_Constant.DT_VCENTER);
                WindowsApi.EndPaint(hWnd, ref ps);
                return IntPtr.Zero;
            case WindowsMessage.DESTROY:
                WindowsApi.PostQuitMessage(0);
                return IntPtr.Zero;
        }

        return WindowsApi.DefWindowProc(hWnd, (WindowsMessage)msg, wParam, lParam);
    }

}
