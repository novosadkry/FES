using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FES__File_Encryption_Software_
{
    public partial class SetKeyDialog : Form
    {
        public SetKeyDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.Text.Length == 44 && Regex.IsMatch(textBox1.Text, "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$"))
            {
                Key.SetValue(Convert.FromBase64String(textBox1.Text));
                ActiveForm.Close();
            }

            else
                MessageBox.Show(Resources.Languages.MainForm.MainFormLang.setKeyError, Resources.Languages.MainForm.MainFormLang.setKeyQT, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
