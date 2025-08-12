using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinLatch
{
    public class WindowInfo
    {
        public IntPtr Handle { get; set; }
        public string Title { get; set; }
        public Rectangle Bounds { get; set; }
        public Rectangle ClientBounds { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFullscreen { get; set; }
        public bool IsBorderless { get; set; }
        public uint ProcessId { get; set; }

        public Size Size => Bounds.Size;
        public Point Position => Bounds.Location;
    }

    public static class WindowManager
    {
        public static List<WindowInfo> GetAllWindows(bool includeHidden = false)
        {
            var windows = new List<WindowInfo>();
            
            WindowsAPI.EnumWindows((hWnd, lParam) =>
            {
                // Include hidden windows if requested, otherwise only visible ones
                if (!includeHidden && !WindowsAPI.IsWindowVisible(hWnd))
                    return true;

                var length = WindowsAPI.GetWindowTextLength(hWnd);
                if (length == 0)
                    return true;

                var sb = new StringBuilder(length + 1);
                WindowsAPI.GetWindowText(hWnd, sb, sb.Capacity);
                
                if (string.IsNullOrWhiteSpace(sb.ToString()))
                    return true;

                WindowsAPI.GetWindowRect(hWnd, out var rect);
                WindowsAPI.GetClientRect(hWnd, out var clientRect);
                WindowsAPI.GetWindowThreadProcessId(hWnd, out var processId);

                var style = WindowsAPI.GetWindowLong(hWnd, WindowsAPI.GWL_STYLE);
                var exStyle = WindowsAPI.GetWindowLong(hWnd, WindowsAPI.GWL_EXSTYLE);

                bool isBorderless = (style & WindowsAPI.WS_CAPTION) == 0 && 
                                   (style & WindowsAPI.WS_THICKFRAME) == 0;
                bool isFullscreen = IsWindowFullscreen(hWnd, rect.ToRectangle());
                bool isVisible = WindowsAPI.IsWindowVisible(hWnd);

                var window = new WindowInfo
                {
                    Handle = hWnd,
                    Title = sb.ToString(),
                    Bounds = rect.ToRectangle(),
                    ClientBounds = new Rectangle(0, 0, clientRect.Right - clientRect.Left, clientRect.Bottom - clientRect.Top),
                    IsVisible = isVisible,
                    IsFullscreen = isFullscreen,
                    IsBorderless = isBorderless,
                    ProcessId = processId
                };

                windows.Add(window);
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        public static WindowInfo GetForegroundWindow()
        {
            var hWnd = WindowsAPI.GetForegroundWindow();
            if (hWnd == IntPtr.Zero)
                return null;

            var windows = GetAllWindows();
            return windows.FirstOrDefault(w => w.Handle == hWnd);
        }

        public static bool IsWindowFullscreen(IntPtr hWnd, Rectangle windowRect)
        {
            var monitor = WindowsAPI.MonitorFromWindow(hWnd, WindowsAPI.MONITOR_DEFAULTTONEAREST);
            var monitorInfo = new WindowsAPI.MONITORINFO { cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<WindowsAPI.MONITORINFO>() };
            
            if (WindowsAPI.GetMonitorInfo(monitor, ref monitorInfo))
            {
                var monitorRect = monitorInfo.rcMonitor;
                return windowRect.Width >= monitorRect.Width && windowRect.Height >= monitorRect.Height;
            }

            return false;
        }

        public static void MoveWindow(IntPtr hWnd, int x, int y, int width, int height)
        {
            WindowsAPI.SetWindowPos(hWnd, IntPtr.Zero, x, y, width, height, 0);
        }

        public static void ResizeWindow(IntPtr hWnd, int width, int height)
        {
            WindowsAPI.SetWindowPos(hWnd, IntPtr.Zero, 0, 0, width, height, 
                WindowsAPI.SWP_NOMOVE | WindowsAPI.SWP_NOZORDER);
        }

        public static void SetWindowPosition(IntPtr hWnd, int x, int y)
        {
            WindowsAPI.SetWindowPos(hWnd, IntPtr.Zero, x, y, 0, 0, 
                WindowsAPI.SWP_NOSIZE | WindowsAPI.SWP_NOZORDER);
        }

        public static Rectangle GetMonitorBounds(IntPtr hWnd)
        {
            var monitor = WindowsAPI.MonitorFromWindow(hWnd, WindowsAPI.MONITOR_DEFAULTTONEAREST);
            var monitorInfo = new WindowsAPI.MONITORINFO { cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<WindowsAPI.MONITORINFO>() };
            
            if (WindowsAPI.GetMonitorInfo(monitor, ref monitorInfo))
            {
                return monitorInfo.rcMonitor;
            }

            return Screen.PrimaryScreen.Bounds;
        }
    }
}
