using System;
using System.Collections.Generic;
using System.Drawing;

namespace WinLatch
{
    public enum BlackBarPosition
    {
        Top,
        Bottom,
        Left,
        Right,
        TopBottom,
        LeftRight,
        Remove,
        PerfectTopAlign
    }

    public static class WindowPositioner
    {
        public static void PositionWindow(WindowInfo window, BlackBarPosition position, AspectRatio targetAspectRatio = null)
        {
            if (window?.Handle == IntPtr.Zero)
                return;

            var monitorBounds = WindowManager.GetMonitorBounds(window.Handle);
            var currentSize = window.Size;
            var currentPos = window.Position;

            // If no target aspect ratio specified, try to detect it from current window
            if (targetAspectRatio == null)
            {
                targetAspectRatio = AspectRatio.GetClosestAspectRatio(currentSize);
            }

            var monitorAspectRatio = AspectRatio.GetClosestAspectRatio(monitorBounds.Size);

            switch (position)
            {
                case BlackBarPosition.Bottom:
                    PositionBlackBarsBottom(window, monitorBounds, targetAspectRatio);
                    break;
                case BlackBarPosition.Top:
                    PositionBlackBarsTop(window, monitorBounds, targetAspectRatio);
                    break;
                case BlackBarPosition.Left:
                    PositionBlackBarsLeft(window, monitorBounds, targetAspectRatio);
                    break;
                case BlackBarPosition.Right:
                    PositionBlackBarsRight(window, monitorBounds, targetAspectRatio);
                    break;
                case BlackBarPosition.Remove:
                    RemoveBlackBars(window, monitorBounds, targetAspectRatio);
                    break;
                case BlackBarPosition.PerfectTopAlign:
                    PositionPerfectTopAlign(window, monitorBounds, targetAspectRatio);
                    break;
            }
        }

        private static void PositionBlackBarsBottom(WindowInfo window, Rectangle monitor, AspectRatio targetRatio)
        {
            // Calculate the size that maintains aspect ratio fitting to monitor width
            var newSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: true);
            
            // If calculated height is larger than monitor, fit to height instead
            if (newSize.Height > monitor.Height)
            {
                newSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: false);
            }

            // Position at top of monitor
            var newPos = new Point(
                monitor.X + (monitor.Width - newSize.Width) / 2,
                monitor.Y
            );

            WindowManager.MoveWindow(window.Handle, newPos.X, newPos.Y, newSize.Width, newSize.Height);
        }

        private static void PositionBlackBarsTop(WindowInfo window, Rectangle monitor, AspectRatio targetRatio)
        {
            var newSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: true);
            
            if (newSize.Height > monitor.Height)
            {
                newSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: false);
            }

            // Position at bottom of monitor
            var newPos = new Point(
                monitor.X + (monitor.Width - newSize.Width) / 2,
                monitor.Y + monitor.Height - newSize.Height
            );

            WindowManager.MoveWindow(window.Handle, newPos.X, newPos.Y, newSize.Width, newSize.Height);
        }

        private static void PositionBlackBarsLeft(WindowInfo window, Rectangle monitor, AspectRatio targetRatio)
        {
            var newSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: false);
            
            if (newSize.Width > monitor.Width)
            {
                newSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: true);
            }

            // Position at right of monitor
            var newPos = new Point(
                monitor.X + monitor.Width - newSize.Width,
                monitor.Y + (monitor.Height - newSize.Height) / 2
            );

            WindowManager.MoveWindow(window.Handle, newPos.X, newPos.Y, newSize.Width, newSize.Height);
        }

        private static void PositionBlackBarsRight(WindowInfo window, Rectangle monitor, AspectRatio targetRatio)
        {
            var newSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: false);
            
            if (newSize.Width > monitor.Width)
            {
                newSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: true);
            }

            // Position at left of monitor
            var newPos = new Point(
                monitor.X,
                monitor.Y + (monitor.Height - newSize.Height) / 2
            );

            WindowManager.MoveWindow(window.Handle, newPos.X, newPos.Y, newSize.Width, newSize.Height);
        }

        private static void RemoveBlackBars(WindowInfo window, Rectangle monitor, AspectRatio targetRatio)
        {
            // For ultrawide monitors, we can make the window smaller to remove black bars
            // This allows side-by-side usage
            var monitorRatio = (double)monitor.Width / monitor.Height;
            var targetRatioValue = targetRatio.Ratio;

            if (monitorRatio > targetRatioValue)
            {
                // Monitor is wider than target ratio
                // Calculate width that matches the target ratio for the monitor height
                var newWidth = (int)(monitor.Height * targetRatioValue);
                var newPos = new Point(monitor.X, monitor.Y);
                
                WindowManager.MoveWindow(window.Handle, newPos.X, newPos.Y, newWidth, monitor.Height);
            }
            else
            {
                // Monitor is taller than target ratio or same
                // Calculate height that matches the target ratio for the monitor width
                var newHeight = (int)(monitor.Width / targetRatioValue);
                var newPos = new Point(monitor.X, monitor.Y);
                
                WindowManager.MoveWindow(window.Handle, newPos.X, newPos.Y, monitor.Width, newHeight);
            }
        }

        private static void PositionPerfectTopAlign(WindowInfo window, Rectangle monitor, AspectRatio targetRatio)
        {
            // Calculate the expected content size at target aspect ratio
            var targetSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: true);
            
            // If calculated height is larger than monitor, fit to height instead
            if (targetSize.Height > monitor.Height)
            {
                targetSize = targetRatio.CalculateSize(monitor.Size, fitToWidth: false);
            }

            // Calculate black bar size (difference between window and content)
            var currentWindowSize = window.Size;
            var blackBarHeight = (currentWindowSize.Height - targetSize.Height) / 2;

            // Move window UP by the black bar amount so content aligns with screen top
            // This pushes the bottom black bars off-screen, wrapping them to the bottom
            var newPos = new Point(
                monitor.X + (monitor.Width - currentWindowSize.Width) / 2, // Center horizontally
                monitor.Y - blackBarHeight  // Move up by black bar height
            );

            // Keep the same window size, just reposition it
            WindowManager.MoveWindow(window.Handle, newPos.X, newPos.Y, currentWindowSize.Width, currentWindowSize.Height);
        }

        public static List<Rectangle> CalculateBlackBarAreas(Rectangle windowBounds, AspectRatio contentRatio)
        {
            var blackBarAreas = new List<Rectangle>();
            var windowRatio = (double)windowBounds.Width / windowBounds.Height;
            var contentRatioValue = contentRatio.Ratio;

            if (Math.Abs(windowRatio - contentRatioValue) < 0.01)
            {
                return blackBarAreas; // No black bars
            }

            if (windowRatio > contentRatioValue)
            {
                // Window is wider than content ratio - black bars on left and right
                var contentWidth = (int)(windowBounds.Height * contentRatioValue);
                var blackBarWidth = (windowBounds.Width - contentWidth) / 2;
                
                // Left black bar
                blackBarAreas.Add(new Rectangle(
                    windowBounds.X, 
                    windowBounds.Y, 
                    blackBarWidth, 
                    windowBounds.Height));
                    
                // Right black bar
                blackBarAreas.Add(new Rectangle(
                    windowBounds.X + windowBounds.Width - blackBarWidth, 
                    windowBounds.Y, 
                    blackBarWidth, 
                    windowBounds.Height));
            }
            else
            {
                // Window is taller than content ratio - black bars on top and bottom
                var contentHeight = (int)(windowBounds.Width / contentRatioValue);
                var blackBarHeight = (windowBounds.Height - contentHeight) / 2;
                
                // Top black bar
                blackBarAreas.Add(new Rectangle(
                    windowBounds.X, 
                    windowBounds.Y, 
                    windowBounds.Width, 
                    blackBarHeight));
                    
                // Bottom black bar
                blackBarAreas.Add(new Rectangle(
                    windowBounds.X, 
                    windowBounds.Y + windowBounds.Height - blackBarHeight, 
                    windowBounds.Width, 
                    blackBarHeight));
            }

            return blackBarAreas;
        }

        // Keep the old method for compatibility but mark it as obsolete
        [System.Obsolete("Use CalculateBlackBarAreas(Rectangle, AspectRatio) instead")]
        public static Size CalculateBlackBarAreas(Rectangle windowBounds, Rectangle monitorBounds, AspectRatio contentRatio)
        {
            var areas = CalculateBlackBarAreas(windowBounds, contentRatio);
            return areas.Count > 0 ? areas[0].Size : Size.Empty;
        }
    }
}
