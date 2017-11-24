using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using OpenCVManager.Extensions;
using OpenCVManager.Utilities;

namespace OpenCVManager.Forms
{
    public partial class ProjectSettingsForm : Form
    {
        #region Properties

        private Project Project { get; }

        public string UsedVersion {
            get => usedVersionComboBox.Text;
            private set => usedVersionComboBox.Text = value;
        }

        public List<string> AvailableLibs { get; private set; }
        public List<string> ProjectLibs { get; private set; }
        public bool IsInitialized { get; private set; }

        #endregion

        #region Constructors

        public ProjectSettingsForm(Project project)
        {
            Project = project ?? throw new ArgumentNullException(nameof(project));

            InitializeComponent();

            libsListBox.ItemCheck += OnItemCheck;
        }

        #endregion

        #region Private methods

        private const string UserVersionGlobalVariableName = "OpenCVManagerUsedVersion";

        //TODO: $(Any) $(Any3.3.0) $(Any3.3.0+)
        private void InitializeModules()
        {
            IsInitialized = false;

            UsedVersion = Project.TryGetGlobalVariable(UserVersionGlobalVariableName, out string result)
                ? result : string.Empty;

            var libraryPath = @"D:\Libraries\opencv\3.3.0";
            AvailableLibs = LibraryUtilities.GetAvailableLibraries(Path.Combine(libraryPath, "install", "x64", "vc15")).
                Select(lib => GetName(lib, "331")).ToList();
            ProjectLibs = GetOpenCvProjectLibraries(Project);

            libsListBox.Items.Clear();
            foreach (var lib in AvailableLibs)
            {
                libsListBox.Items.Add(lib, ProjectLibs.Contains(lib, StringComparer.InvariantCultureIgnoreCase));
            }
            foreach (var lib in ProjectLibs.Except(AvailableLibs, StringComparer.InvariantCultureIgnoreCase))
            {
                libsListBox.Items.Add(lib, CheckState.Indeterminate);
            }

            IsInitialized = true;
        }

        #endregion

        #region Event Handlers

        private void OnLoad(object sender, EventArgs e)
        {
            InitializeModules();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void Save(object sender, EventArgs e)
        {
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
                    var name = (sender as CheckedListBox)?.Items[e.Index] as string;
                    if (name == null)
                    {
                        return;
                    }

                    var isAvailable = AvailableLibs.Contains(name);
                    e.NewValue = isAvailable ? CheckState.Checked : CheckState.Indeterminate;
                    if (!isAvailable)
                    {
                        MessageBox.Show(@"Current library is unavailable for the selected version");
                    }
                    break;

            }
        }

        #endregion

        #region Static methods

        public static string GetName(string path, string version) =>
            Path.GetFileNameWithoutExtension(path)?.Replace("opencv_", "").Replace(version, "");

        public static string GetVersion(string libPath)
        {
            var name = Path.GetFileNameWithoutExtension(libPath);
            if (name == null)
            {
                throw new ArgumentException(@"Incorrect lib path", nameof(libPath));
            }

            var index = name.Length - 1;
            var character = name[index];
            while (char.IsDigit(character))
            {
                index--;
                character = name[index];
            }

            if (index >= name.Length - 1)
            {
                throw new ArgumentException(@"Lib name should be ended for digit characters", nameof(libPath));
            }

            return name.Substring(index + 1);
        }

        public static List<string> GetOpenCvProjectLibraries(Project project)
        {
            var libs = LibraryUtilities.GetVcProjectLibraries(project, "opencv_");
            if (libs.Count == 0)
            {
                return libs;
            }

            var version = GetVersion(libs[0]);
            return libs.Select(lib => GetName(lib, version)).ToList();
        }

        #endregion
    }
}
