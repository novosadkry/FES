using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FES__File_Encryption_Software_
{
    public partial class ShowKeyDialog : Form
    {
        public ShowKeyDialog()
        {
            InitializeComponent();
        }

        private void ShowKeyDialog_Load(object sender, EventArgs e)
        {
            keyLabel.Text = Convert.ToBase64String(Key.Value);
            lenghtLabel.Text = Convert.ToString(Convert.ToBase64String(Key.Value).Length) + " char";
        }

        private async void btnClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Convert.ToBase64String(Key.Value));
            clipboardLabel.Text = Resources.Languages.MainForm.MainFormLang.showKeyCC;
            await Task.Delay(1000);
            clipboardLabel.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }
    }
}
