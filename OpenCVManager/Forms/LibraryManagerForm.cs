using System;
using System.Windows.Forms;
using OpenCVManager.Core;
using OpenCVManager.Utilities;

namespace OpenCVManager.Forms
{
    public partial class LibraryManagerForm : Form
    {
        public LibraryManagerForm()
        {
            InitializeComponent();
        }

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

        private void OnKeyPress(object sender, KeyPressEventArgs e) =>
            StandardEventHandlers.OnKeyPressEscapeCancel(sender, e);

        private void Delete(object sender, EventArgs e)
        {
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
            Close();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            UpdateListBox();
        }
    }
}
