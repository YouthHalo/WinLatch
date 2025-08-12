using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinLatch
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private ListBox lstWindows;
        private Label lblWindowInfo;
        private Button btnMoveBottom;
        private Button btnMoveTop;
        private Button btnMoveLeft;
        private Button btnMoveRight;
        private Button btnRemoveBars;
        private Button btnPerfectTopAlign;
        private Button btnCreateTestWindow;
        private Button btnCreateOverlay;
        private Button btnClearOverlays;
        private ComboBox cmbAspectRatio;
        private Label lblAspectRatio;
        private CheckBox chkShowHidden;
        private GroupBox grpWindowList;
        private GroupBox grpWindowInfo;
        private GroupBox grpPositioning;
        private GroupBox grpOverlays;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lstWindows = new ListBox();
            this.lblWindowInfo = new Label();
            this.btnMoveBottom = new Button();
            this.btnMoveTop = new Button();
            this.btnMoveLeft = new Button();
            this.btnMoveRight = new Button();
            this.btnRemoveBars = new Button();
            this.btnPerfectTopAlign = new Button();
            this.btnCreateTestWindow = new Button();
            this.btnCreateOverlay = new Button();
            this.btnClearOverlays = new Button();
            this.cmbAspectRatio = new ComboBox();
            this.lblAspectRatio = new Label();
            this.chkShowHidden = new CheckBox();
            this.grpWindowList = new GroupBox();
            this.grpWindowInfo = new GroupBox();
            this.grpPositioning = new GroupBox();
            this.grpOverlays = new GroupBox();
            this.grpWindowList.SuspendLayout();
            this.grpWindowInfo.SuspendLayout();
            this.grpPositioning.SuspendLayout();
            this.grpOverlays.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstWindows
            // 
            this.lstWindows.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            this.lstWindows.FormattingEnabled = true;
            this.lstWindows.ItemHeight = 15;
            this.lstWindows.Location = new Point(6, 22);
            this.lstWindows.Name = "lstWindows";
            this.lstWindows.Size = new Size(350, 199);
            this.lstWindows.TabIndex = 0;
            this.lstWindows.SelectedIndexChanged += new EventHandler(this.lstWindows_SelectedIndexChanged);
            // 
            // lblWindowInfo
            // 
            this.lblWindowInfo.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            this.lblWindowInfo.Location = new Point(6, 22);
            this.lblWindowInfo.Name = "lblWindowInfo";
            this.lblWindowInfo.Size = new Size(350, 199);
            this.lblWindowInfo.TabIndex = 1;
            this.lblWindowInfo.Text = "No window selected";
            // 
            // btnMoveBottom
            // 
            this.btnMoveBottom.Location = new Point(6, 52);
            this.btnMoveBottom.Name = "btnMoveBottom";
            this.btnMoveBottom.Size = new Size(100, 30);
            this.btnMoveBottom.TabIndex = 2;
            this.btnMoveBottom.Text = "Bars to Bottom";
            this.btnMoveBottom.UseVisualStyleBackColor = true;
            this.btnMoveBottom.Click += new EventHandler(this.btnMoveBottom_Click);
            // 
            // btnMoveTop
            // 
            this.btnMoveTop.Location = new Point(112, 52);
            this.btnMoveTop.Name = "btnMoveTop";
            this.btnMoveTop.Size = new Size(100, 30);
            this.btnMoveTop.TabIndex = 3;
            this.btnMoveTop.Text = "Bars to Top";
            this.btnMoveTop.UseVisualStyleBackColor = true;
            this.btnMoveTop.Click += new EventHandler(this.btnMoveTop_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Location = new Point(218, 52);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new Size(100, 30);
            this.btnMoveLeft.TabIndex = 4;
            this.btnMoveLeft.Text = "Bars to Left";
            this.btnMoveLeft.UseVisualStyleBackColor = true;
            this.btnMoveLeft.Click += new EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Location = new Point(324, 52);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new Size(100, 30);
            this.btnMoveRight.TabIndex = 5;
            this.btnMoveRight.Text = "Bars to Right";
            this.btnMoveRight.UseVisualStyleBackColor = true;
            this.btnMoveRight.Click += new EventHandler(this.btnMoveRight_Click);
            // 
            // btnRemoveBars
            // 
            this.btnRemoveBars.Location = new Point(6, 88);
            this.btnRemoveBars.Name = "btnRemoveBars";
            this.btnRemoveBars.Size = new Size(150, 30);
            this.btnRemoveBars.TabIndex = 6;
            this.btnRemoveBars.Text = "Remove Black Bars";
            this.btnRemoveBars.UseVisualStyleBackColor = true;
            this.btnRemoveBars.Click += new EventHandler(this.btnRemoveBars_Click);
            // 
            // btnPerfectTopAlign
            // 
            this.btnPerfectTopAlign.Location = new Point(162, 88);
            this.btnPerfectTopAlign.Name = "btnPerfectTopAlign";
            this.btnPerfectTopAlign.Size = new Size(150, 30);
            this.btnPerfectTopAlign.TabIndex = 7;
            this.btnPerfectTopAlign.Text = "Perfect Top Align";
            this.btnPerfectTopAlign.UseVisualStyleBackColor = true;
            this.btnPerfectTopAlign.Click += new EventHandler(this.btnPerfectTopAlign_Click);
            // 
            // btnCreateTestWindow
            // 
            this.btnCreateTestWindow.Location = new Point(318, 88);
            this.btnCreateTestWindow.Name = "btnCreateTestWindow";
            this.btnCreateTestWindow.Size = new Size(120, 30);
            this.btnCreateTestWindow.TabIndex = 8;
            this.btnCreateTestWindow.Text = "Create Test 4:3";
            this.btnCreateTestWindow.UseVisualStyleBackColor = true;
            this.btnCreateTestWindow.Click += new EventHandler(this.btnCreateTestWindow_Click);
            // 
            // btnCreateOverlay
            // 
            this.btnCreateOverlay.Location = new Point(6, 22);
            this.btnCreateOverlay.Name = "btnCreateOverlay";
            this.btnCreateOverlay.Size = new Size(120, 30);
            this.btnCreateOverlay.TabIndex = 7;
            this.btnCreateOverlay.Text = "Create Overlay";
            this.btnCreateOverlay.UseVisualStyleBackColor = true;
            this.btnCreateOverlay.Click += new EventHandler(this.btnCreateOverlay_Click);
            // 
            // btnClearOverlays
            // 
            this.btnClearOverlays.Location = new Point(132, 22);
            this.btnClearOverlays.Name = "btnClearOverlays";
            this.btnClearOverlays.Size = new Size(120, 30);
            this.btnClearOverlays.TabIndex = 8;
            this.btnClearOverlays.Text = "Clear All Overlays";
            this.btnClearOverlays.UseVisualStyleBackColor = true;
            this.btnClearOverlays.Click += new EventHandler(this.btnClearOverlays_Click);
            // 
            // cmbAspectRatio
            // 
            this.cmbAspectRatio.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAspectRatio.FormattingEnabled = true;
            this.cmbAspectRatio.Location = new Point(90, 22);
            this.cmbAspectRatio.Name = "cmbAspectRatio";
            this.cmbAspectRatio.Size = new Size(200, 23);
            this.cmbAspectRatio.TabIndex = 9;
            // 
            // lblAspectRatio
            // 
            this.lblAspectRatio.AutoSize = true;
            this.lblAspectRatio.Location = new Point(6, 25);
            this.lblAspectRatio.Name = "lblAspectRatio";
            this.lblAspectRatio.Size = new Size(78, 15);
            this.lblAspectRatio.TabIndex = 10;
            this.lblAspectRatio.Text = "Aspect Ratio:";
            // 
            // chkShowHidden
            // 
            this.chkShowHidden.AutoSize = true;
            this.chkShowHidden.Location = new Point(6, 227);
            this.chkShowHidden.Name = "chkShowHidden";
            this.chkShowHidden.Size = new Size(150, 19);
            this.chkShowHidden.TabIndex = 11;
            this.chkShowHidden.Text = "Show hidden windows";
            this.chkShowHidden.UseVisualStyleBackColor = true;
            this.chkShowHidden.CheckedChanged += new EventHandler(this.chkShowHidden_CheckedChanged);
            // 
            // grpWindowList
            // 
            this.grpWindowList.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left)));
            this.grpWindowList.Controls.Add(this.lstWindows);
            this.grpWindowList.Controls.Add(this.chkShowHidden);
            this.grpWindowList.Location = new Point(12, 12);
            this.grpWindowList.Name = "grpWindowList";
            this.grpWindowList.Size = new Size(362, 255);
            this.grpWindowList.TabIndex = 11;
            this.grpWindowList.TabStop = false;
            this.grpWindowList.Text = "Available Windows";
            // 
            // grpWindowInfo
            // 
            this.grpWindowInfo.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) 
            | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            this.grpWindowInfo.Controls.Add(this.lblWindowInfo);
            this.grpWindowInfo.Location = new Point(380, 12);
            this.grpWindowInfo.Name = "grpWindowInfo";
            this.grpWindowInfo.Size = new Size(362, 255);
            this.grpWindowInfo.TabIndex = 12;
            this.grpWindowInfo.TabStop = false;
            this.grpWindowInfo.Text = "Window Information";
            // 
            // grpPositioning
            // 
            this.grpPositioning.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left) 
            | AnchorStyles.Right)));
            this.grpPositioning.Controls.Add(this.lblAspectRatio);
            this.grpPositioning.Controls.Add(this.cmbAspectRatio);
            this.grpPositioning.Controls.Add(this.btnMoveBottom);
            this.grpPositioning.Controls.Add(this.btnMoveTop);
            this.grpPositioning.Controls.Add(this.btnMoveLeft);
            this.grpPositioning.Controls.Add(this.btnMoveRight);
            this.grpPositioning.Controls.Add(this.btnRemoveBars);
            this.grpPositioning.Controls.Add(this.btnPerfectTopAlign);
            this.grpPositioning.Controls.Add(this.btnCreateTestWindow);
            this.grpPositioning.Location = new Point(12, 273);
            this.grpPositioning.Name = "grpPositioning";
            this.grpPositioning.Size = new Size(540, 130);
            this.grpPositioning.TabIndex = 13;
            this.grpPositioning.TabStop = false;
            this.grpPositioning.Text = "Window Positioning";
            // 
            // grpOverlays
            // 
            this.grpOverlays.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.grpOverlays.Controls.Add(this.btnCreateOverlay);
            this.grpOverlays.Controls.Add(this.btnClearOverlays);
            this.grpOverlays.Location = new Point(558, 273);
            this.grpOverlays.Name = "grpOverlays";
            this.grpOverlays.Size = new Size(184, 130);
            this.grpOverlays.TabIndex = 14;
            this.grpOverlays.TabStop = false;
            this.grpOverlays.Text = "Overlay Management";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(754, 415);
            this.Controls.Add(this.grpOverlays);
            this.Controls.Add(this.grpPositioning);
            this.Controls.Add(this.grpWindowInfo);
            this.Controls.Add(this.grpWindowList);
            this.MinimumSize = new Size(600, 425);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "WinLatch - Window Black Bar Manager";
            this.Load += new EventHandler(this.MainForm_Load);
            this.grpWindowList.ResumeLayout(false);
            this.grpWindowInfo.ResumeLayout(false);
            this.grpPositioning.ResumeLayout(false);
            this.grpPositioning.PerformLayout();
            this.grpOverlays.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
