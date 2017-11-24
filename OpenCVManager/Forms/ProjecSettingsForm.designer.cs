namespace OpenCVManager.Forms
{
    partial class ProjectSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OptionsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.libsPage = new System.Windows.Forms.TabPage();
            this.libsListBox = new System.Windows.Forms.CheckedListBox();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.libsPage.SuspendLayout();
            this.settingsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // OptionsPropertyGrid
            // 
            this.OptionsPropertyGrid.HelpVisible = false;
            this.OptionsPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.OptionsPropertyGrid.Location = new System.Drawing.Point(6, 6);
            this.OptionsPropertyGrid.Name = "OptionsPropertyGrid";
            this.OptionsPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.OptionsPropertyGrid.Size = new System.Drawing.Size(443, 213);
            this.OptionsPropertyGrid.TabIndex = 8;
            this.OptionsPropertyGrid.ToolbarVisible = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.libsPage);
            this.tabControl.Controls.Add(this.settingsPage);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(463, 344);
            this.tabControl.TabIndex = 10;
            // 
            // libsPage
            // 
            this.libsPage.Controls.Add(this.libsListBox);
            this.libsPage.Location = new System.Drawing.Point(4, 22);
            this.libsPage.Name = "libsPage";
            this.libsPage.Padding = new System.Windows.Forms.Padding(3);
            this.libsPage.Size = new System.Drawing.Size(455, 318);
            this.libsPage.TabIndex = 2;
            this.libsPage.Text = "Libs";
            // 
            // libsListBox
            // 
            this.libsListBox.CheckOnClick = true;
            this.libsListBox.Enabled = false;
            this.libsListBox.FormattingEnabled = true;
            this.libsListBox.IntegralHeight = false;
            this.libsListBox.Location = new System.Drawing.Point(6, 6);
            this.libsListBox.MultiColumn = true;
            this.libsListBox.Name = "libsListBox";
            this.libsListBox.Size = new System.Drawing.Size(443, 306);
            this.libsListBox.TabIndex = 0;
            // 
            // settingsPage
            // 
            this.settingsPage.BackColor = System.Drawing.SystemColors.Control;
            this.settingsPage.Controls.Add(this.OptionsPropertyGrid);
            this.settingsPage.Location = new System.Drawing.Point(4, 22);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsPage.Size = new System.Drawing.Size(455, 318);
            this.settingsPage.TabIndex = 0;
            this.settingsPage.Text = "Settings";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(226, 396);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(118, 30);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.Save);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(354, 396);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(115, 30);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.Cancel);
            // 
            // ProjectSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 438);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ProjectSettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "OpenCV Manager Options";
            this.Load += new System.EventHandler(this.OnLoad);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.tabControl.ResumeLayout(false);
            this.libsPage.ResumeLayout(false);
            this.settingsPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid OptionsPropertyGrid;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.TabPage libsPage;
        private System.Windows.Forms.CheckedListBox libsListBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}