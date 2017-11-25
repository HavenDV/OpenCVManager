﻿namespace OpenCVManager.Forms
{
    partial class LibraryManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LibraryManagerForm));
            this.cancelButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.librariesPage = new System.Windows.Forms.TabPage();
            this.winCECombo = new System.Windows.Forms.ComboBox();
            this.defaultx64Label = new System.Windows.Forms.Label();
            this.defaultCombo = new System.Windows.Forms.ComboBox();
            this.defaultx86Label = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.optionsPage = new System.Windows.Forms.TabPage();
            this.optionsPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.okButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.librariesPage.SuspendLayout();
            this.optionsPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(337, 287);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.librariesPage);
            this.tabControl.Controls.Add(this.optionsPage);
            this.tabControl.Location = new System.Drawing.Point(2, 2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(423, 279);
            this.tabControl.TabIndex = 20;
            // 
            // librariesPage
            // 
            this.librariesPage.BackColor = System.Drawing.SystemColors.Control;
            this.librariesPage.Controls.Add(this.winCECombo);
            this.librariesPage.Controls.Add(this.defaultx64Label);
            this.librariesPage.Controls.Add(this.defaultCombo);
            this.librariesPage.Controls.Add(this.defaultx86Label);
            this.librariesPage.Controls.Add(this.deleteButton);
            this.librariesPage.Controls.Add(this.addButton);
            this.librariesPage.Controls.Add(this.listView);
            this.librariesPage.Location = new System.Drawing.Point(4, 22);
            this.librariesPage.Name = "librariesPage";
            this.librariesPage.Padding = new System.Windows.Forms.Padding(3);
            this.librariesPage.Size = new System.Drawing.Size(415, 253);
            this.librariesPage.TabIndex = 0;
            this.librariesPage.Text = "Libraries";
            // 
            // winCECombo
            // 
            this.winCECombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winCECombo.FormattingEnabled = true;
            this.winCECombo.Location = new System.Drawing.Point(113, 218);
            this.winCECombo.Name = "winCECombo";
            this.winCECombo.Size = new System.Drawing.Size(292, 21);
            this.winCECombo.TabIndex = 24;
            // 
            // defaultx64Label
            // 
            this.defaultx64Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.defaultx64Label.AutoSize = true;
            this.defaultx64Label.Location = new System.Drawing.Point(6, 221);
            this.defaultx64Label.Name = "defaultx64Label";
            this.defaultx64Label.Size = new System.Drawing.Size(101, 13);
            this.defaultx64Label.TabIndex = 23;
            this.defaultx64Label.Text = "Default x64 version:";
            // 
            // defaultCombo
            // 
            this.defaultCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.defaultCombo.FormattingEnabled = true;
            this.defaultCombo.Location = new System.Drawing.Point(113, 188);
            this.defaultCombo.Name = "defaultCombo";
            this.defaultCombo.Size = new System.Drawing.Size(292, 21);
            this.defaultCombo.TabIndex = 22;
            // 
            // defaultx86Label
            // 
            this.defaultx86Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.defaultx86Label.AutoSize = true;
            this.defaultx86Label.Location = new System.Drawing.Point(6, 191);
            this.defaultx86Label.Name = "defaultx86Label";
            this.defaultx86Label.Size = new System.Drawing.Size(101, 13);
            this.defaultx86Label.TabIndex = 21;
            this.defaultx86Label.Text = "Default x86 version:";
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(224, 158);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(181, 24);
            this.deleteButton.TabIndex = 20;
            this.deleteButton.Text = "&Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(33, 158);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(185, 24);
            this.addButton.TabIndex = 19;
            this.addButton.Text = "&Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(6, 6);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(399, 146);
            this.listView.TabIndex = 18;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // optionsPage
            // 
            this.optionsPage.BackColor = System.Drawing.SystemColors.Control;
            this.optionsPage.Controls.Add(this.optionsPropertyGrid);
            this.optionsPage.Location = new System.Drawing.Point(4, 22);
            this.optionsPage.Name = "optionsPage";
            this.optionsPage.Padding = new System.Windows.Forms.Padding(3);
            this.optionsPage.Size = new System.Drawing.Size(415, 253);
            this.optionsPage.TabIndex = 1;
            this.optionsPage.Text = "Options";
            // 
            // optionsPropertyGrid
            // 
            this.optionsPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsPropertyGrid.HelpVisible = false;
            this.optionsPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
            this.optionsPropertyGrid.Location = new System.Drawing.Point(3, 3);
            this.optionsPropertyGrid.Name = "optionsPropertyGrid";
            this.optionsPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.optionsPropertyGrid.Size = new System.Drawing.Size(409, 247);
            this.optionsPropertyGrid.TabIndex = 0;
            this.optionsPropertyGrid.ToolbarVisible = false;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(251, 287);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(80, 24);
            this.okButton.TabIndex = 18;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // LibraryManagerForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(427, 322);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "LibraryManagerForm";
            this.ShowInTaskbar = false;
            this.Text = "OpenCV Libraries Manager";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.tabControl.ResumeLayout(false);
            this.librariesPage.ResumeLayout(false);
            this.librariesPage.PerformLayout();
            this.optionsPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage librariesPage;
        private System.Windows.Forms.ComboBox winCECombo;
        private System.Windows.Forms.Label defaultx64Label;
        private System.Windows.Forms.ComboBox defaultCombo;
        private System.Windows.Forms.Label defaultx86Label;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.TabPage optionsPage;
        private System.Windows.Forms.PropertyGrid optionsPropertyGrid;
        private System.Windows.Forms.Button okButton;
    }
}