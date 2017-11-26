using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCVManager.Core;

namespace OpenCVManager.Forms
{
    public partial class AddNewVersionForm : Form
    {
        public string VersionPath
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }

        public AddNewVersionForm()
        {
            InitializeComponent();
        }

        private void Browse(object sender, EventArgs e)
        {
            var result = folderBrowserDialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            VersionPath = folderBrowserDialog.SelectedPath;
        }

        private void SetError(Control control, string text)
        {
            errorProvider.SetError(control, text);
            control.BackColor = Color.LightPink;
        }

        private void ClearError(Control control)
        {
            errorProvider.Clear();
            control.BackColor = Color.LightGreen;
        }

        private bool Validate(string path)
        {
            try
            {
                var library = new Library(path);
                if (!library.IsAvailable)
                {
                    SetError(textBox, library.Error);
                    return false;
                }

                ClearError(textBox);
                return true;
            }
            catch (Exception exception)
            {
                SetError(textBox, exception.Message);

                return false;
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = Validate(VersionPath);
        }

        private void Save(object sender, EventArgs e)
        {
            Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            Close();
        }
    }
}
