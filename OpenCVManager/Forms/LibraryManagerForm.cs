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

        private void UpdateBoxes()
        {
            listView.Items.Clear();
            defaultX86ComboBox.Items.Clear();
            defaultX64ComboBox.Items.Clear();
            foreach (var path in LibraryManager.SavedVersions)
            {
                var library = new Library(path);
                listView.Items.Add(new ListViewItem(path)
                {
                    Tag = path,
                    SubItems = { library.Version, library.MachineType },
                    ForeColor = library.IsAvailable ? DefaultForeColor : Color.LightGray
                });

                if (!library.IsAvailable)
                {
                    continue;
                }

                if (library.Is64Bit.HasValue && library.Is64Bit.Value)
                {
                    defaultX64ComboBox.Items.Add(path);
                }
                else
                {
                    defaultX86ComboBox.Items.Add(path);
                }
            }

            defaultX86ComboBox.Text = LibraryManager.DefaultX86;
            defaultX64ComboBox.Text = LibraryManager.DefaultX64;
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

            UpdateBoxes();
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

            UpdateBoxes();
        }

        private void DefaultX86Changed(object sender, EventArgs e) =>
            LibraryManager.DefaultX86 = defaultX86ComboBox.Text;

        private void DefaultX64Changed(object sender, EventArgs e) => 
            LibraryManager.DefaultX64 = defaultX64ComboBox.Text;

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
            UpdateBoxes();
        }

        #endregion
    }
}
