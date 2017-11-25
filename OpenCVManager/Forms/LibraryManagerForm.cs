using System;
using System.Windows.Forms;

namespace OpenCVManager.Forms
{
    public partial class LibraryManagerForm : Form
    {
        public LibraryManagerForm()
        {
            InitializeComponent();

            SetupDefaultVersionComboBox(null);
            //SetupWinCEVersionComboBox(null);
            UpdateListBox();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        public void LoadSettings()
        {
        }

        public void SaveSettings()
        {
        }

        private void UpdateListBox()
        {
            UpdateListBox(null);
        }

        private void UpdateListBox(string defaultVersionDir)
        {
            /*
            listView.Items.Clear();
            foreach (string version in versionManager.GetVersions()) 
            {
                string path = null;
                if (defaultVersionDir != null && version == "$(DefaultVersion)")
                    path = defaultVersionDir;
                else
                    path = versionManager.GetInstallPath(version);
                if (path == null && version != "$(DIR)")
                    continue;
                ListViewItem itm = new ListViewItem();
                itm.Tag = version;
                itm.Text = version;
                itm.SubItems.Add(path);
                listView.Items.Add(itm);
            }*/
        }

        private void SetupDefaultVersionComboBox(string version)
        {
            //SetupVersionComboBox(defaultCombo, version, new VIBoolPredicate(isDesktop));
        }
        /*
        private void SetupVersionComboBox(ComboBox box, string version, VIBoolPredicate versionInfoCheck)
        {
            string currentItem = box.Text;
            if (version != null)
                currentItem = version;
            box.Items.Clear();

            foreach (string v in versionManager.GetVersions())
            {
                if (v == "$(DefaultVersion)")
                    continue;
                try
                {
                    VersionInformation vi = new VersionInformation(versionManager.GetInstallPath(v));
                    if (versionInfoCheck(vi))
                        box.Items.Add(v);
                }
                catch (Exception)
                {
                }
            }

            if (box.Items.Count > 0)
            {
                if (box.Items.Contains(currentItem))
                    box.Text = currentItem;
                else
                    box.Text = (string)box.Items[0];
            }
            else
            {
                box.Text = "";
            }
        }
        */

        private void deleteButton_Click(object sender, EventArgs e)
        {
        }

        private void addButton_Click(object sender, EventArgs e)
        {
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
