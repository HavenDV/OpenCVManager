using System;
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

        public string UsedVersion
        {
            get => usedVersionComboBox.Text;
            private set => usedVersionComboBox.Text = value;
        }

        #endregion

        #region Constructors

        public ProjectSettingsForm(Project project)
        {
            Project = project ?? throw new ArgumentNullException(nameof(project));
            
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private const string UserVersionGlobalVariableName = "OpenCVManagerUsedVersion";
        
        //TODO: $(Any) $(Any3.3.0) $(Any3.3.0+)
        private void InitializeModules()
        {
            UsedVersion = Project.TryGetGlobalVariable(UserVersionGlobalVariableName, out string result)
                ? result : string.Empty;

            var libraryPath = @"D:\Libraries\opencv\3.3.0";
            var availableLibs = LibraryUtilities.GetAvailableLibs(Path.Combine(libraryPath, "install", "x64", "vc15")).
                Select(lib => GetName(lib, "331")).ToList();
            var projectLibs = LibraryUtilities.GetVcProjectLibs(Project, "opencv_", "310").
                Select(lib => GetName(lib, "310")).ToList();

            libsListBox.Items.Clear();
            foreach (var lib in availableLibs)
            {
                libsListBox.Items.Add(lib, projectLibs.Contains(lib, StringComparer.InvariantCultureIgnoreCase));
            }
            foreach (var lib in projectLibs.Except(availableLibs, StringComparer.InvariantCultureIgnoreCase))
            {
                libsListBox.Items.Add(lib, CheckState.Indeterminate);
            }
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

        #endregion
        
        #region Static methods

        public static string GetName(string path, string version) =>
            Path.GetFileNameWithoutExtension(path)?.Replace("opencv_", "").Replace(version, "");

        #endregion
    }
}
