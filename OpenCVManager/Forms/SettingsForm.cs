﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using OpenCVManager.Core;
using OpenCVManager.Extensions;
using OpenCVManager.Utilities;

namespace OpenCVManager.Forms
{
    public partial class SettingsForm : Form
    {
        #region Properties

        private Project Project { get; }

        public string UsedVersion {
            get => usedVersionComboBox.Text;
            private set => usedVersionComboBox.Text = value;
        }

        public Library PreviousLibrary { get; private set; }
        public Library UsedLibrary { get; private set; }
        public List<string> ProjectLibraries { get; private set; }
        public List<string> ProjectModules { get; private set; }
        public bool IsInitialized { get; private set; }

        #endregion

        #region Constructors

        public SettingsForm(Project project)
        {
            Project = project ?? throw new ArgumentNullException(nameof(project));

            InitializeComponent();

            modulesListBox.ItemCheck += OnItemCheck;
        }

        #endregion

        #region Private methods

        private const string UserVersionGlobalVariableName = "OpenCVManager_UsedVersion";
        
        private void UpdateAvailableVersions()
        {
            usedVersionComboBox.Items.Clear();
            usedVersionComboBox.Items.Add(string.Empty);
            usedVersionComboBox.Items.Add("$(Default)");

            foreach (var path in LibraryManager.GetSavedVersions())
            {
                var library = new Library(path);
                if (!usedVersionComboBox.Items.Contains(path) && library.IsAvailable)
                {
                    usedVersionComboBox.Items.Add(path);
                }
            }
        }

        private void UpdateModules()
        {
            IsInitialized = false;

            modulesListBox.Items.Clear();
            foreach (var name in UsedLibrary.AvailableModules)
            {
                modulesListBox.Items.Add(name, UsedLibrary.IsAvailable && ProjectModules.Contains(name, StringComparer.OrdinalIgnoreCase));
            }
            foreach (var lib in ProjectModules.Except(UsedLibrary.AvailableModules, StringComparer.OrdinalIgnoreCase))
            {
                modulesListBox.Items.Add(lib, CheckState.Indeterminate);
            }

            IsInitialized = true;
        }

        #endregion

        #region Event Handlers

        private void OnLoad(object sender, EventArgs e)
        {
            ProjectLibraries = Project.GetProjectLibraries("opencv_").ToList();
            ProjectModules = ProjectLibraries.Select(Library.GetName).ToList();

            //TODO: $(Any) $(Any3.3.0) $(Any3.3.0+)
            UsedVersion = Project.TryGetGlobalVariable(UserVersionGlobalVariableName, out string result)
                ? result : string.Empty;
            UsedLibrary = new Library(UsedVersion);
            PreviousLibrary = UsedLibrary;

            UpdateAvailableVersions();
            UpdateModules();
        }

        private void UsedVersionChanged(object sender, EventArgs e)
        {
            UsedLibrary = new Library(UsedVersion);
            UpdateModules();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e) => 
            StandardEventHandlers.OnKeyPressEscapeCancel(sender, e);

        private string[] GetCheckedLibraries() => modulesListBox.CheckedItems.
            Cast<object>().
            Select(i => Library.GetFileName(i.ToString(), UsedLibrary.VersionShort, ".lib")).
            ToArray();

        private void Save(object sender, EventArgs e)
        {
            // TODO: check used exists in available
            Project.WrireGlobalVariable(UserVersionGlobalVariableName, UsedVersion);

            Project.DeleteProjectDependencies("Release", "x64", ProjectLibraries.ToArray());
            Project.AddProjectDependencies("Release", "x64", GetCheckedLibraries());

            Project.DeleteProjectLibraryDirectories("Release", "x64", PreviousLibrary.LibrariesPath);
            Project.AddProjectLibraryDirectories("Release", "x64", UsedLibrary.LibrariesPath);

            Project.DeleteProjectHeadersDirectories("Release", "x64", PreviousLibrary.HeadersPath);
            Project.AddProjectHeadersDirectories("Release", "x64", UsedLibrary.HeadersPath);

            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }

        private void OnItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!IsInitialized)
            {
                return;
            }

            switch (e.CurrentValue)
            {
                case CheckState.Indeterminate:
                    e.NewValue = CheckState.Unchecked;
                    break;

                case CheckState.Unchecked:
                    if (!((sender as CheckedListBox)?.Items[e.Index] is string name))
                    {
                        return;
                    }

                    var isAvailable = UsedLibrary.IsAvailable && UsedLibrary.AvailableModules.Contains(name);
                    e.NewValue = isAvailable ? CheckState.Checked : CheckState.Indeterminate;
                    if (!isAvailable)
                    {
                        MessageBox.Show(@"Current library is unavailable for the selected version");
                    }
                    break;
            }
        }

        private void Manage(object sender, EventArgs e)
        {
            using (var form = new LibraryManagerForm())
            {
                form.ShowDialog();
            }

            UpdateAvailableVersions();
        }

        #endregion
    }
}
