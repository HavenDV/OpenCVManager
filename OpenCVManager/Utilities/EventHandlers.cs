using System.Windows.Forms;

namespace OpenCVManager.Utilities
{
    public static class StandardEventHandlers
    {
        public static void OnKeyPressEscapeCancel(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                if (!(sender is Form form))
                {
                    return;
                }

                form.DialogResult = DialogResult.Cancel;
                form.Close();
            }
        }
    }
}
