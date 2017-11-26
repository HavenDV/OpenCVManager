using System;
using System.Windows.Forms;
using OpenCVManager.Core;
using OpenCVManager.Utilities;

namespace OpenCVManager.Forms
{
    public partial class LibraryManagerForm : Form
    {
        #region Constructors

        public LibraryManagerForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void UpdateListBox()
        {
            listView.Items.Clear();
            foreach (var path in LibraryManager.GetAvailableVersions())
            {
                var library = new Library(path);
                listView.Items.Add(new ListViewItem(path)
                {
                    Tag = path,
                    SubItems = { library.Version, library.MachineType }
                });
            }
        }

        #endregion

        #region Event handlers

        private void OnKeyPress(object sender, KeyPressEventArgs e) =>
            StandardEventHandlers.OnKeyPressEscapeCancel(sender, e); //TODO: Is Manager.Cancel required?

        private void Delete(object sender, EventArgs e)
        {
            LibraryManager.DeleteVersion("test");

            UpdateListBox();
        }

        private void Add(object sender, EventArgs e)
        {
            LibraryManager.AddNewVersion("test");
            
            UpdateListBox();
        }

        private void Save(object sender, EventArgs e)
        {
            LibraryManager.Save();

            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            LibraryManager.Cancel();

            Close();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        #endregion
    }
}
