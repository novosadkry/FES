using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FES__File_Encryption_Software_.Resources.Languages.LoginForm;

namespace FES__File_Encryption_Software_
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            try
            {
                byte[] password = Convert.FromBase64String(Properties.Settings.Default.Password);
                byte[] IV = password.Skip(password.Length - 16).Take(16).ToArray();

                password = password.Take(password.Length - 16).ToArray();
                password = ProtectedData.Unprotect(password, IV, DataProtectionScope.CurrentUser);

                textBox2.Text = Properties.Settings.Default.Password = Encoding.UTF8.GetString(password);
                textBox1.Text = Properties.Settings.Default.Username;
            }
            catch (Exception) { }

            if (textBox1.Text != "" && textBox2.Text != "")
                rememberCheck.Checked = true;

            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Properties.Settings.Default.Lang);

            //Localization
            Text = LoginFormLang.title;
            button1.Text = LoginFormLang.loginBtn;
            button2.Text = LoginFormLang.registerBtn;
            usernameLabel.Text = LoginFormLang.usernameLabel;
            passwordLabel.Text = LoginFormLang.passwordLabel;
            rememberCheck.Text = LoginFormLang.rememberCheck;
        }

        /// <summary>
        /// Gets an enability of all <see cref="Form"/>'s <see cref="Control"/>
        /// </summary>
        private bool InputEnabled
        {
            set
            {
                foreach (Control c in Controls)
                    c.Enabled = value;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            InputEnabled = false;

            if (textBox1.Text.Replace(" ", "") != "" && textBox2.Text.Replace(" ", "") != "")
            {
                try
                {
                    string givenUsername = textBox1.Text.Trim();
                    string givenPassword = textBox2.Text.Trim();

                    string sqldata = await SQLAcc.GetUserHash(givenUsername);
                    string pass;

                    string[] parts = sqldata.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    using (Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(givenPassword, Convert.FromBase64String(parts[1])))
                        pass = Convert.ToBase64String(rfc.GetBytes(32));

                    if (parts[0] == pass)
                    {
                        if (rememberCheck.Checked)
                        {
                            byte[] IV = new byte[16];

                            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                                rng.GetBytes(IV);

                            byte[] password = ProtectedData.Protect(Encoding.UTF8.GetBytes(givenPassword), IV, DataProtectionScope.CurrentUser);
                            Properties.Settings.Default.Password = Convert.ToBase64String(password.Concat(IV).ToArray());
                            Properties.Settings.Default.Username = givenUsername;
                            Properties.Settings.Default.Save();
                        }

                        else
                        {
                            Properties.Settings.Default.Username = "";
                            Properties.Settings.Default.Password = "";
                            Properties.Settings.Default.Save();
                        }

                        DialogResult = DialogResult.OK;
                        MessageBox.Show(LoginFormLang.user + " '" + givenUsername + "' " + LoginFormLang.loginS, LoginFormLang.loginST, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        SQLAcc.LoggedHash = sqldata;
                        SQLAcc.LoggedUser = givenUsername;
                        SQLAcc.LoggedPass = givenPassword;
                    }

                    else
                        MessageBox.Show(LoginFormLang.loginUS, LoginFormLang.loginUST, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                catch (Exception)
                {
                    MessageBox.Show(LoginFormLang.loginUS, LoginFormLang.loginUST, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                GC.Collect();
            }

            InputEnabled = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            InputEnabled = false;

            string username = textBox1.Text.Replace(" ", ""), 
                password = textBox2.Text.Replace(" ", ""), 
                ipaddress = new WebClient().DownloadString("http://icanhazip.com");

            try
            {
                if (Regex.IsMatch(textBox1.Text, @"^[a-zA-Z][a-zA-Z0-9]{4,20}$"))
                {
                    if (Regex.IsMatch(textBox2.Text, @"^[a-zA-Z0-9]{4,}$"))
                    {
                        await SQLAcc.Register(username, password, ipaddress);
                        MessageBox.Show(LoginFormLang.registerS, LoginFormLang.registerST, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    else
                        MessageBox.Show(LoginFormLang.invalidFormatP, LoginFormLang.invalidFormatT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
     
                else
                    MessageBox.Show(LoginFormLang.invalidFormatU, LoginFormLang.invalidFormatT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (Exception)
            {
                DialogResult dr = MessageBox.Show(LoginFormLang.registerUS, LoginFormLang.registerUST, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if (dr == DialogResult.Retry)
                    await SQLAcc.Register(username, password, ipaddress);
            }

            InputEnabled = true;
        }
    }
}
