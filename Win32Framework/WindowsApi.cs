using System.Runtime.InteropServices;
using Win32Image.Win32Framework.Structures;

namespace Win32Image.Win32Framework;

/// <summary>
/// Windows 32 API link.
/// </summary>
public static class WindowsApi
{
   public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
   public delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

   [DllImport("user32.dll")]
   public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

   [DllImport("user32.dll")]
   public static extern bool TranslateMessage([In] ref MSG lpMsg);

   [DllImport("user32.dll")]
   public static extern sbyte GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

   [DllImport("user32.dll", SetLastError = true, EntryPoint = "CreateWindowEx")]
   public static extern IntPtr CreateWindowEx(
      WindowStylesEx dwExStyle,
      UInt16 lpClassName,
      string lpWindowName,
      WindowStyles dwStyle,
      int x,
      int y,
      int nWidth,
      int nHeight,
      IntPtr hWndParent,
      IntPtr hMenu,
      IntPtr hInstance,
      IntPtr lpParam);

   [DllImport("user32.dll")]
   public static extern ushort RegisterClass([In] ref WNDCLASS lpWndClass);

   [DllImport("user32.dll")]
   [return: MarshalAs(UnmanagedType.Bool)]
   public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

   [DllImport("user32.dll")]
   public static extern IntPtr BeginPaint(IntPtr hwnd, out PAINTSTRUCT lpPaint);

   [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
   public static extern IntPtr LoadImageW(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

   [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
   public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

   [DllImport("user32.dll")]
   public static extern IntPtr DefWindowProc(IntPtr hWnd, WindowsMessage uMsg, IntPtr wParam, IntPtr lParam);

   [DllImport("user32.dll")]
   public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

   [DllImport("user32.dll")]
   public static extern int DrawText(IntPtr hDC, string lpString, int nCount, ref RECT lpRect, uint uFormat);

   [DllImport("user32.dll")]
   public static extern bool EndPaint(IntPtr hWnd, [In] ref PAINTSTRUCT lpPaint);

   [DllImport("user32.dll")]
   public static extern void PostQuitMessage(int nExitCode);

   [DllImport("user32.dll")]
   public static extern IntPtr LoadIcon(IntPtr hInstance, string lpIconName);

   [DllImport("user32.dll")]
   public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIConName);

   [DllImport("gdi32.dll")]
   public static extern IntPtr GetStockObject(StockObjects fnObject);

   [DllImport("user32.dll")]
   public static extern MessageBoxResult MessageBox(IntPtr hWnd, string text, string caption, int options);

   [DllImport("user32.dll")]
   public static extern bool UpdateWindow(IntPtr hWnd);

   [DllImport("user32.dll")]
   public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

   [DllImport("user32.dll")]
   public static extern short RegisterClassEx([In] ref WNDCLASS lpwcx);

   [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassEx")]
   public static extern UInt16 RegisterClassEx([In] ref WNDCLASSEX lpwcx);

   [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
   [return: MarshalAs(UnmanagedType.Bool)]
   public static extern bool DeleteObject([In] IntPtr hObject);

   [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
   public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

   [DllImport("gdi32.dll")]
   public extern static IntPtr CreateCompatibleDC(IntPtr hdc);

   [DllImport("gdi32.dll")]
   public extern static IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int height);

   [DllImport("gdi32.dll")]
   static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint cPlanes, uint cBitsPerPel, IntPtr lpvBits);

   [DllImport("gdi32.dll")]
   public extern static int BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

   [DllImport("user32.dll")]
   public static extern IntPtr GetDC(IntPtr hWnd);

   [DllImport("user32.dll")]
   public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

   [DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
   public static extern IntPtr DeleteDC(IntPtr hDc);

}
