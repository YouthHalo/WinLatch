using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinLatch
{
    public partial class TestWindow : Form
    {
        public TestWindow(Size targetSize)
        {
            InitializeComponent();
            SetupTestWindow(targetSize);
        }

        private void SetupTestWindow(Size targetSize)
        {
            // Make it borderless
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = targetSize;
            this.BackColor = Color.DarkBlue;
            this.TopMost = true;

            // Add some visual content to distinguish it
            var label = new Label
            {
                Text = $"Test Window\n{targetSize.Width}x{targetSize.Height}\n4:3 Aspect Ratio\n\nPress ESC to close",
                ForeColor = Color.White,
                Font = new Font("Arial", 16, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            this.Controls.Add(label);

            // Close on ESC key
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            };

            // Show window title in taskbar
            this.Text = $"WinLatch Test Window ({targetSize.Width}x{targetSize.Height})";
            this.ShowInTaskbar = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TestWindow
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 600);
            this.Name = "TestWindow";
            this.Text = "WinLatch Test Window";
            this.ResumeLayout(false);
        }
    }
}
