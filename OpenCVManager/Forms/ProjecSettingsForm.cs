using System;
using System.IO;
using System.Windows.Forms;
using EnvDTE;
using OpenCVManager.Utilities;

namespace OpenCVManager.Forms
{
    public partial class ProjectSettingsForm : Form
    {
        #region Properties

        private Project Project { get; }
        private string LibraryPath { get; }

        #endregion

        #region Constructors

        public ProjectSettingsForm(Project project, string libraryPath)
        {
            Project = project ?? throw new ArgumentNullException(nameof(project));
            LibraryPath = !string.IsNullOrWhiteSpace(libraryPath)
                ? libraryPath : throw new ArgumentNullException(nameof(libraryPath));

            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void InitializeModules()
        {
            var availableLibs = LibraryUtilities.GetAvailableLibs(Path.Combine(LibraryPath, "install", "x64", "vc15"));
            var projectLibs = LibraryUtilities.GetVcProjectLibs(Project);
            var version = "331";

            libsListBox.Items.Clear();
            foreach (var lib in availableLibs)
            {
                var libName = Path.GetFileName(lib);
                libsListBox.Items.Add(LibraryPathToName(lib, version), projectLibs.Contains(libName));
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
            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
        
        #region Static methods

        public static string LibraryPathToName(string path, string version) =>
            Path.GetFileNameWithoutExtension(path)?.Replace("opencv_", "").Replace(version, "");

        #endregion
    }
}
