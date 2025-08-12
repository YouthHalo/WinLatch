using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinLatch
{
    public partial class MainForm : Form
    {
        private WindowInfo _selectedWindow;
        private Timer _refreshTimer;
        private List<OverlayForm> _overlays = new List<OverlayForm>();

        public MainForm()
        {
            InitializeComponent();
            InitializeForm();
            RefreshWindowList();
            StartAutoRefresh();
        }

        private void InitializeForm()
        {
            this.Text = "WinLatch - Window Black Bar Manager";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(800, 600);
            this.MinimumSize = new Size(600, 400);
        }

        private void StartAutoRefresh()
        {
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 5000; // Refresh every 5 seconds (less aggressive)
            _refreshTimer.Tick += (s, e) => RefreshWindowList();
            _refreshTimer.Start();
        }

        private void RefreshWindowList()
        {
            var includeHidden = chkShowHidden?.Checked ?? false;
            var windows = WindowManager.GetAllWindows(includeHidden)
                .Where(w => !string.IsNullOrEmpty(w.Title) && w.Size.Width > 100 && w.Size.Height > 100)
                .OrderBy(w => w.Title)
                .ToList();

            // Preserve the currently selected window
            var selectedHandle = _selectedWindow?.Handle ?? IntPtr.Zero;

            lstWindows.DataSource = null;
            lstWindows.DataSource = windows;
            lstWindows.DisplayMember = "Title";
            lstWindows.ValueMember = "Handle";

            // Restore selection if the window still exists
            if (selectedHandle != IntPtr.Zero)
            {
                var selectedWindow = windows.FirstOrDefault(w => w.Handle == selectedHandle);
                if (selectedWindow != null)
                {
                    lstWindows.SelectedItem = selectedWindow;
                }
                else
                {
                    // Window no longer exists, clear selection
                    _selectedWindow = null;
                }
            }

            // Update current window info if one is selected
            if (_selectedWindow != null)
            {
                var updated = windows.FirstOrDefault(w => w.Handle == _selectedWindow.Handle);
                if (updated != null)
                {
                    var oldBounds = _selectedWindow.Bounds;
                    var oldFullscreen = _selectedWindow.IsFullscreen;
                    var oldBorderless = _selectedWindow.IsBorderless;
                    
                    _selectedWindow = updated;
                    
                    // Only update UI if window properties have changed
                    if (updated.Bounds != oldBounds || 
                        updated.IsFullscreen != oldFullscreen ||
                        updated.IsBorderless != oldBorderless)
                    {
                        UpdateWindowInfo();
                    }
                }
            }
        }

        private void lstWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedWindow = lstWindows.SelectedItem as WindowInfo;
            UpdateWindowInfo();
            UpdatePositionButtons();
        }

        private void UpdateWindowInfo()
        {
            if (_selectedWindow == null)
            {
                lblWindowInfo.Text = "No window selected";
                return;
            }

            var monitor = WindowManager.GetMonitorBounds(_selectedWindow.Handle);
            var aspectRatio = AspectRatio.GetClosestAspectRatio(_selectedWindow.Size);
            var monitorAspectRatio = AspectRatio.GetClosestAspectRatio(monitor.Size);

            lblWindowInfo.Text = $@"Window: {_selectedWindow.Title}
Size: {_selectedWindow.Size.Width} x {_selectedWindow.Size.Height}
Position: {_selectedWindow.Position.X}, {_selectedWindow.Position.Y}
Detected Aspect Ratio: {aspectRatio}
Monitor: {monitor.Width} x {monitor.Height} ({monitorAspectRatio})
Visible: {_selectedWindow.IsVisible}
Fullscreen: {_selectedWindow.IsFullscreen}
Borderless: {_selectedWindow.IsBorderless}";
        }

        private void UpdatePositionButtons()
        {
            bool hasWindow = _selectedWindow != null;
            btnMoveBottom.Enabled = hasWindow;
            btnMoveTop.Enabled = hasWindow;
            btnMoveLeft.Enabled = hasWindow;
            btnMoveRight.Enabled = hasWindow;
            btnRemoveBars.Enabled = hasWindow;
            btnPerfectTopAlign.Enabled = hasWindow;
            btnCreateOverlay.Enabled = hasWindow;
            btnClearOverlays.Enabled = hasWindow;
        }

        private void btnMoveBottom_Click(object sender, EventArgs e)
        {
            if (_selectedWindow == null) return;
            
            var targetRatio = GetSelectedAspectRatio();
            WindowPositioner.PositionWindow(_selectedWindow, BlackBarPosition.Bottom, targetRatio);
            RefreshWindowInfo();
        }

        private void btnMoveTop_Click(object sender, EventArgs e)
        {
            if (_selectedWindow == null) return;
            
            var targetRatio = GetSelectedAspectRatio();
            WindowPositioner.PositionWindow(_selectedWindow, BlackBarPosition.Top, targetRatio);
            RefreshWindowInfo();
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            if (_selectedWindow == null) return;
            
            var targetRatio = GetSelectedAspectRatio();
            WindowPositioner.PositionWindow(_selectedWindow, BlackBarPosition.Left, targetRatio);
            RefreshWindowInfo();
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            if (_selectedWindow == null) return;
            
            var targetRatio = GetSelectedAspectRatio();
            WindowPositioner.PositionWindow(_selectedWindow, BlackBarPosition.Right, targetRatio);
            RefreshWindowInfo();
        }

        private void btnRemoveBars_Click(object sender, EventArgs e)
        {
            if (_selectedWindow == null) return;
            
            var targetRatio = GetSelectedAspectRatio();
            WindowPositioner.PositionWindow(_selectedWindow, BlackBarPosition.Remove, targetRatio);
            RefreshWindowInfo();
        }

        private void btnPerfectTopAlign_Click(object sender, EventArgs e)
        {
            if (_selectedWindow == null) return;
            
            var targetRatio = GetSelectedAspectRatio();
            WindowPositioner.PositionWindow(_selectedWindow, BlackBarPosition.PerfectTopAlign, targetRatio);
            RefreshWindowInfo();
        }

        private void btnCreateTestWindow_Click(object sender, EventArgs e)
        {
            // Get current screen resolution
            var screen = Screen.PrimaryScreen;
            var screenWidth = screen.Bounds.Width;
            var screenHeight = screen.Bounds.Height;

            // Calculate 4:3 aspect ratio size that fits the current screen
            int targetWidth, targetHeight;
            
            // Fit to screen width
            targetWidth = screenWidth;
            targetHeight = (int)(screenWidth * 3.0 / 4.0);
            
            // If too tall, fit to screen height instead
            if (targetHeight > screenHeight)
            {
                targetHeight = screenHeight;
                targetWidth = (int)(screenHeight * 4.0 / 3.0);
            }

            var testSize = new Size(targetWidth, targetHeight);
            
            // Create and show the test window
            var testWindow = new TestWindow(testSize);
            testWindow.Show();
            
            // Refresh the window list so the test window appears
            RefreshWindowList();
        }

        private void btnCreateOverlay_Click(object sender, EventArgs e)
        {
            if (_selectedWindow == null) return;

            var aspectRatio = GetSelectedAspectRatio();
            var blackBarAreas = WindowPositioner.CalculateBlackBarAreas(_selectedWindow.Bounds, aspectRatio);
            
            if (blackBarAreas.Count == 0)
            {
                MessageBox.Show("No black bars detected for the current window configuration.", "No Black Bars", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var overlayForm = new OverlayConfigForm();
            if (overlayForm.ShowDialog() == DialogResult.OK)
            {
                // Create an overlay for each black bar area
                foreach (var blackBarArea in blackBarAreas)
                {
                    var overlay = OverlayManager.CreateOverlay(
                        blackBarArea,
                        overlayForm.OverlayText,
                        overlayForm.ImagePath,
                        overlayForm.Transparency
                    );
                    _overlays.Add(overlay);
                }
            }
        }

        private void btnClearOverlays_Click(object sender, EventArgs e)
        {
            OverlayManager.CloseAllOverlays();
            _overlays.Clear();
        }

        private void RefreshWindowInfo()
        {
            // Refresh the selected window's information
            if (_selectedWindow != null)
            {
                var windows = WindowManager.GetAllWindows();
                var updated = windows.FirstOrDefault(w => w.Handle == _selectedWindow.Handle);
                if (updated != null)
                {
                    _selectedWindow = updated;
                    UpdateWindowInfo();
                }
            }
        }

        private AspectRatio GetSelectedAspectRatio()
        {
            if (cmbAspectRatio.SelectedItem is AspectRatio selectedRatio)
            {
                return selectedRatio;
            }
            
            // Default to detecting from current window
            return _selectedWindow != null ? 
                AspectRatio.GetClosestAspectRatio(_selectedWindow.Size) : 
                AspectRatio.CommonAspectRatios[0];
        }

        private void chkShowHidden_CheckedChanged(object sender, EventArgs e)
        {
            // Refresh the window list when the checkbox state changes
            RefreshWindowList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Populate aspect ratio combo box
            cmbAspectRatio.DataSource = AspectRatio.CommonAspectRatios;
            cmbAspectRatio.DisplayMember = "Name";
            
            // Select 16:9 as default
            cmbAspectRatio.SelectedIndex = 0;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
            OverlayManager.CloseAllOverlays();
            base.OnFormClosed(e);
        }
    }
}
