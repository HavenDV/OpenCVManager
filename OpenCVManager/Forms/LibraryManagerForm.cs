using System;
using System.Drawing;
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
            foreach (var path in LibraryManager.GetSavedVersions())
            {
                var library = new Library(path);
                listView.Items.Add(new ListViewItem(path)
                {
                    Tag = path,
                    SubItems = { library.Version, library.MachineType },
                    ForeColor = library.IsAvailable ? DefaultForeColor : Color.LightGray
                });
            }
        }

        #endregion

        #region Event handlers

        private void OnKeyPress(object sender, KeyPressEventArgs e) =>
            StandardEventHandlers.OnKeyPressEscapeCancel(sender, e); //TODO: Is Manager.Cancel required?

        private void Delete(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView.SelectedItems)
            {
                LibraryManager.DeleteVersion(item.Text);
            }
            
            UpdateListBox();
        }

        private void Add(object sender, EventArgs e)
        {
            using (var form = new AddNewVersionForm())
            {
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                LibraryManager.AddNewVersion(form.VersionPath);
            }
            
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
