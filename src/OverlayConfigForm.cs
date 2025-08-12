using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinLatch
{
    public partial class OverlayConfigForm : Form
    {
        public string OverlayText { get; private set; }
        public string ImagePath { get; private set; }
        public float Transparency { get; private set; } = 0.5f;

        public OverlayConfigForm()
        {
            InitializeComponent();
            trackTransparency.Value = 50; // 50% transparency
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files|*.*";
                openFileDialog.Title = "Select Overlay Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = openFileDialog.FileName;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OverlayText = txtOverlayText.Text;
            ImagePath = txtImagePath.Text;
            Transparency = trackTransparency.Value / 100f;

            if (string.IsNullOrEmpty(OverlayText) && string.IsNullOrEmpty(ImagePath))
            {
                MessageBox.Show("Please enter text or select an image for the overlay.", "No Content", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(ImagePath) && !File.Exists(ImagePath))
            {
                MessageBox.Show("The selected image file does not exist.", "File Not Found", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void trackTransparency_Scroll(object sender, EventArgs e)
        {
            lblTransparencyValue.Text = $"{trackTransparency.Value}%";
        }

        private void InitializeComponent()
        {
            this.lblOverlayText = new Label();
            this.txtOverlayText = new TextBox();
            this.lblImagePath = new Label();
            this.txtImagePath = new TextBox();
            this.btnBrowseImage = new Button();
            this.lblTransparency = new Label();
            this.trackTransparency = new TrackBar();
            this.lblTransparencyValue = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.grpContent = new GroupBox();
            this.grpAppearance = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackTransparency)).BeginInit();
            this.grpContent.SuspendLayout();
            this.grpAppearance.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOverlayText
            // 
            this.lblOverlayText.AutoSize = true;
            this.lblOverlayText.Location = new Point(6, 25);
            this.lblOverlayText.Name = "lblOverlayText";
            this.lblOverlayText.Size = new Size(31, 15);
            this.lblOverlayText.TabIndex = 0;
            this.lblOverlayText.Text = "Text:";
            // 
            // txtOverlayText
            // 
            this.txtOverlayText.Location = new Point(60, 22);
            this.txtOverlayText.Name = "txtOverlayText";
            this.txtOverlayText.PlaceholderText = "Enter overlay text (optional)";
            this.txtOverlayText.Size = new Size(300, 23);
            this.txtOverlayText.TabIndex = 1;
            // 
            // lblImagePath
            // 
            this.lblImagePath.AutoSize = true;
            this.lblImagePath.Location = new Point(6, 55);
            this.lblImagePath.Name = "lblImagePath";
            this.lblImagePath.Size = new Size(43, 15);
            this.lblImagePath.TabIndex = 2;
            this.lblImagePath.Text = "Image:";
            // 
            // txtImagePath
            // 
            this.txtImagePath.Location = new Point(60, 52);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.PlaceholderText = "Select image file (optional)";
            this.txtImagePath.ReadOnly = true;
            this.txtImagePath.Size = new Size(220, 23);
            this.txtImagePath.TabIndex = 3;
            // 
            // btnBrowseImage
            // 
            this.btnBrowseImage.Location = new Point(286, 52);
            this.btnBrowseImage.Name = "btnBrowseImage";
            this.btnBrowseImage.Size = new Size(75, 23);
            this.btnBrowseImage.TabIndex = 4;
            this.btnBrowseImage.Text = "Browse...";
            this.btnBrowseImage.UseVisualStyleBackColor = true;
            this.btnBrowseImage.Click += new EventHandler(this.btnBrowseImage_Click);
            // 
            // lblTransparency
            // 
            this.lblTransparency.AutoSize = true;
            this.lblTransparency.Location = new Point(6, 25);
            this.lblTransparency.Name = "lblTransparency";
            this.lblTransparency.Size = new Size(79, 15);
            this.lblTransparency.TabIndex = 5;
            this.lblTransparency.Text = "Transparency:";
            // 
            // trackTransparency
            // 
            this.trackTransparency.Location = new Point(91, 22);
            this.trackTransparency.Maximum = 100;
            this.trackTransparency.Name = "trackTransparency";
            this.trackTransparency.Size = new Size(200, 45);
            this.trackTransparency.TabIndex = 6;
            this.trackTransparency.TickFrequency = 10;
            this.trackTransparency.Value = 50;
            this.trackTransparency.Scroll += new EventHandler(this.trackTransparency_Scroll);
            // 
            // lblTransparencyValue
            // 
            this.lblTransparencyValue.AutoSize = true;
            this.lblTransparencyValue.Location = new Point(297, 25);
            this.lblTransparencyValue.Name = "lblTransparencyValue";
            this.lblTransparencyValue.Size = new Size(30, 15);
            this.lblTransparencyValue.TabIndex = 7;
            this.lblTransparencyValue.Text = "50%";
            // 
            // btnOK
            // 
            this.btnOK.Location = new Point(214, 190);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(75, 30);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(295, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            // 
            // grpContent
            // 
            this.grpContent.Controls.Add(this.lblOverlayText);
            this.grpContent.Controls.Add(this.txtOverlayText);
            this.grpContent.Controls.Add(this.lblImagePath);
            this.grpContent.Controls.Add(this.txtImagePath);
            this.grpContent.Controls.Add(this.btnBrowseImage);
            this.grpContent.Location = new Point(12, 12);
            this.grpContent.Name = "grpContent";
            this.grpContent.Size = new Size(370, 90);
            this.grpContent.TabIndex = 10;
            this.grpContent.TabStop = false;
            this.grpContent.Text = "Overlay Content";
            // 
            // grpAppearance
            // 
            this.grpAppearance.Controls.Add(this.lblTransparency);
            this.grpAppearance.Controls.Add(this.trackTransparency);
            this.grpAppearance.Controls.Add(this.lblTransparencyValue);
            this.grpAppearance.Location = new Point(12, 108);
            this.grpAppearance.Name = "grpAppearance";
            this.grpAppearance.Size = new Size(370, 70);
            this.grpAppearance.TabIndex = 11;
            this.grpAppearance.TabStop = false;
            this.grpAppearance.Text = "Appearance";
            // 
            // OverlayConfigForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new Size(394, 232);
            this.Controls.Add(this.grpAppearance);
            this.Controls.Add(this.grpContent);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OverlayConfigForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Configure Overlay";
            ((System.ComponentModel.ISupportInitialize)(this.trackTransparency)).EndInit();
            this.grpContent.ResumeLayout(false);
            this.grpContent.PerformLayout();
            this.grpAppearance.ResumeLayout(false);
            this.grpAppearance.PerformLayout();
            this.ResumeLayout(false);
        }

        private Label lblOverlayText;
        private TextBox txtOverlayText;
        private Label lblImagePath;
        private TextBox txtImagePath;
        private Button btnBrowseImage;
        private Label lblTransparency;
        private TrackBar trackTransparency;
        private Label lblTransparencyValue;
        private Button btnOK;
        private Button btnCancel;
        private GroupBox grpContent;
        private GroupBox grpAppearance;
    }
}
