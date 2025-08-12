using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinLatch
{
    public partial class OverlayForm : Form
    {
        private string _overlayText;
        private Image _overlayImage;
        private float _transparency = 0.5f;
        private Rectangle _blackBarArea;

        public OverlayForm()
        {
            InitializeComponent();
            SetupOverlay();
        }

        public OverlayForm(Rectangle blackBarArea, string text = null, Image image = null, float transparency = 0.5f)
        {
            InitializeComponent();
            _blackBarArea = blackBarArea;
            _overlayText = text;
            _overlayImage = image;
            _transparency = transparency;
            SetupOverlay();
        }

        private void SetupOverlay()
        {
            // Make the form transparent and click-through
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.WindowState = FormWindowState.Normal;
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Magenta;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            // Set the form to cover the black bar area
            if (_blackBarArea != Rectangle.Empty)
            {
                this.Bounds = _blackBarArea;
            }

            // Make it semi-transparent
            this.Opacity = _transparency;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Fill the background if not fully transparent
            if (_transparency > 0)
            {
                using (var brush = new SolidBrush(Color.FromArgb((int)(255 * _transparency), Color.Black)))
                {
                    g.FillRectangle(brush, this.ClientRectangle);
                }
            }

            // Draw image if provided
            if (_overlayImage != null)
            {
                var imageRect = GetCenteredImageRect(_overlayImage.Size);
                g.DrawImage(_overlayImage, imageRect);
            }

            // Draw text if provided
            if (!string.IsNullOrEmpty(_overlayText))
            {
                using (var font = new Font("Segoe UI", 12, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var textSize = g.MeasureString(_overlayText, font);
                    var textRect = new PointF(
                        (this.Width - textSize.Width) / 2,
                        (this.Height - textSize.Height) / 2
                    );
                    g.DrawString(_overlayText, font, brush, textRect);
                }
            }
        }

        private Rectangle GetCenteredImageRect(Size imageSize)
        {
            // Scale image to fit while maintaining aspect ratio
            var scale = Math.Min((float)this.Width / imageSize.Width, (float)this.Height / imageSize.Height);
            var scaledSize = new Size((int)(imageSize.Width * scale), (int)(imageSize.Height * scale));
            
            return new Rectangle(
                (this.Width - scaledSize.Width) / 2,
                (this.Height - scaledSize.Height) / 2,
                scaledSize.Width,
                scaledSize.Height
            );
        }

        public void UpdateOverlay(string text = null, Image image = null, float transparency = 0.5f)
        {
            _overlayText = text;
            _overlayImage = image;
            _transparency = transparency;
            this.Opacity = transparency;
            this.Invalidate();
        }

        public void UpdatePosition(Rectangle blackBarArea)
        {
            _blackBarArea = blackBarArea;
            this.Bounds = blackBarArea;
        }

        // Make the window click-through
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x80000 /* WS_EX_LAYERED */ | 0x20 /* WS_EX_TRANSPARENT */;
                return cp;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // OverlayForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 100);
            this.Name = "OverlayForm";
            this.Text = "WinLatch Overlay";
            this.ResumeLayout(false);
        }
    }

    public static class OverlayManager
    {
        private static readonly List<OverlayForm> _activeOverlays = new List<OverlayForm>();

        public static OverlayForm CreateOverlay(Rectangle blackBarArea, string text = null, string imagePath = null, float transparency = 0.5f)
        {
            Image image = null;
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                try
                {
                    image = Image.FromFile(imagePath);
                }
                catch
                {
                    // Ignore if image can't be loaded
                }
            }

            var overlay = new OverlayForm(blackBarArea, text, image, transparency);
            _activeOverlays.Add(overlay);
            overlay.Show();
            return overlay;
        }

        public static void CloseAllOverlays()
        {
            foreach (var overlay in _activeOverlays)
            {
                try
                {
                    overlay.Close();
                    overlay.Dispose();
                }
                catch
                {
                    // Ignore disposal errors
                }
            }
            _activeOverlays.Clear();
        }

        public static void UpdateOverlays(Rectangle newBlackBarArea)
        {
            foreach (var overlay in _activeOverlays)
            {
                overlay.UpdatePosition(newBlackBarArea);
            }
        }
    }
}
