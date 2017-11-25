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
            System.Windows.Forms.Label label;
            this.OptionsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.libsPage = new System.Windows.Forms.TabPage();
            this.libsListBox = new System.Windows.Forms.CheckedListBox();
            this.optionsPage = new System.Windows.Forms.TabPage();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.usedVersionComboBox = new System.Windows.Forms.ComboBox();
            this.manageButton = new System.Windows.Forms.Button();
            label = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.libsPage.SuspendLayout();
            this.optionsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new System.Drawing.Point(19, 368);
            label.Name = "label";
            label.Size = new System.Drawing.Size(72, 13);
            label.TabIndex = 13;
            label.Text = "Used version:";
            // 
            // OptionsPropertyGrid
            // 
            this.OptionsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OptionsPropertyGrid.HelpVisible = false;
            this.OptionsPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.OptionsPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.OptionsPropertyGrid.Name = "OptionsPropertyGrid";
            this.OptionsPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.OptionsPropertyGrid.Size = new System.Drawing.Size(449, 312);
            this.OptionsPropertyGrid.TabIndex = 8;
            this.OptionsPropertyGrid.ToolbarVisible = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.libsPage);
            this.tabControl.Controls.Add(this.optionsPage);
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
            this.libsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libsListBox.FormattingEnabled = true;
            this.libsListBox.IntegralHeight = false;
            this.libsListBox.Location = new System.Drawing.Point(3, 3);
            this.libsListBox.MultiColumn = true;
            this.libsListBox.Name = "libsListBox";
            this.libsListBox.Size = new System.Drawing.Size(449, 312);
            this.libsListBox.TabIndex = 0;
            // 
            // optionsPage
            // 
            this.optionsPage.BackColor = System.Drawing.SystemColors.Control;
            this.optionsPage.Controls.Add(this.OptionsPropertyGrid);
            this.optionsPage.Location = new System.Drawing.Point(4, 22);
            this.optionsPage.Name = "optionsPage";
            this.optionsPage.Padding = new System.Windows.Forms.Padding(3);
            this.optionsPage.Size = new System.Drawing.Size(455, 318);
            this.optionsPage.TabIndex = 0;
            this.optionsPage.Text = "Options";
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(236, 406);
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
            this.cancelButton.Location = new System.Drawing.Point(360, 406);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(115, 30);
            this.cancelButton.TabIndex = 12;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.Click += new System.EventHandler(this.Cancel);
            // 
            // usedVersionComboBox
            // 
            this.usedVersionComboBox.FormattingEnabled = true;
            this.usedVersionComboBox.Items.AddRange(new object[] {
            "",
            "$(Default)"});
            this.usedVersionComboBox.Location = new System.Drawing.Point(97, 365);
            this.usedVersionComboBox.Name = "usedVersionComboBox";
            this.usedVersionComboBox.Size = new System.Drawing.Size(257, 21);
            this.usedVersionComboBox.TabIndex = 14;
            // 
            // manageButton
            // 
            this.manageButton.Location = new System.Drawing.Point(360, 359);
            this.manageButton.Name = "manageButton";
            this.manageButton.Size = new System.Drawing.Size(115, 30);
            this.manageButton.TabIndex = 15;
            this.manageButton.Text = "Manage";
            this.manageButton.UseVisualStyleBackColor = true;
            // 
            // ProjectSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 448);
            this.Controls.Add(this.manageButton);
            this.Controls.Add(this.usedVersionComboBox);
            this.Controls.Add(label);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ProjectSettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "OpenCV Manager Settings";
            this.Load += new System.EventHandler(this.OnLoad);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.tabControl.ResumeLayout(false);
            this.libsPage.ResumeLayout(false);
            this.optionsPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid OptionsPropertyGrid;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage optionsPage;
        private System.Windows.Forms.TabPage libsPage;
        private System.Windows.Forms.CheckedListBox libsListBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox usedVersionComboBox;
        private System.Windows.Forms.Button manageButton;
    }
}