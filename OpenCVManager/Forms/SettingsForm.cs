using System;
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

        public List<string> AvailableModules { get; private set; }
        public List<string> ProjectLibs { get; private set; }
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

        //TODO: $(Any) $(Any3.3.0) $(Any3.3.0+)
        private void InitializeModules()
        {
            IsInitialized = false;

            UsedVersion = Project.TryGetGlobalVariable(UserVersionGlobalVariableName, out string result)
                ? result : string.Empty;

            var library = new Library(@"D:\Libraries\opencv\3.3.0");
            AvailableModules = library.AvailableModules;
            ProjectLibs = GetOpenCvProjectLibraries(Project);

            UpdateAvailableVersions();

            modulesListBox.Items.Clear();
            foreach (var name in AvailableModules)
            {
                modulesListBox.Items.Add(name, ProjectLibs.Contains(name, StringComparer.OrdinalIgnoreCase));
            }
            foreach (var lib in ProjectLibs.Except(AvailableModules, StringComparer.OrdinalIgnoreCase))
            {
                modulesListBox.Items.Add(lib, CheckState.Indeterminate);
            }

            IsInitialized = true;
        }

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

        #endregion

        #region Event Handlers

        private void OnLoad(object sender, EventArgs e)
        {
            InitializeModules();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e) => 
            StandardEventHandlers.OnKeyPressEscapeCancel(sender, e);

        private void Save(object sender, EventArgs e)
        {
            // TODO: check used exists in available
            Project.WrireGlobalVariable(UserVersionGlobalVariableName, UsedVersion);

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

                    var isAvailable = AvailableModules.Contains(name);
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

        #region Static methods

        private static List<string> GetOpenCvProjectLibraries(Project project) => LibraryUtilities.
            GetVcProjectLibraries(project, "opencv_").
            Select(Library.GetName).
            ToList();

        #endregion
    }
}
