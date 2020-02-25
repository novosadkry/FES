using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FES__File_Encryption_Software_.Resources.Languages.HelpDialog;
using FES__File_Encryption_Software_.Resources.Languages.LoginForm;

namespace FES__File_Encryption_Software_
{
    public partial class HelpDialog : Form
    {
        private const int bufferSizeConst = 0x100000;

        public HelpDialog()
        {
            InitializeComponent();

            //Localization
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Properties.Settings.Default.Lang);

            pUsername.Text = SQLAcc.LoggedUser;
            SQLAcc.OnUsernameUpdate += () => pUsername.Text = SQLAcc.LoggedUser;

            vLabel.Text = "v" + Application.ProductVersion;
            this.Text = HelpDialogLang.title;

            sDescCheckBox.Checked = Properties.Settings.Default.LBDesc;
            sDropCheckBox.Checked = Properties.Settings.Default.AllowDrop;

            foreach (Language l in Language.AvailableLanguages)
                sLangListBox.Items.Add(l);

            sBufferSize.Text = Convert.ToString(Properties.Settings.Default.BufferSize / bufferSizeConst);
        }

        private void HelpDialog_Closing(object sender, FormClosingEventArgs e)
        {
            SQLAcc.OnUsernameUpdate -= () => pUsername.Text = SQLAcc.LoggedUser;
        }

        private void ShowPanel(Panel panel)
        {
            IEnumerable<Control> panels = from Control p in splitContainer1.Panel2.Controls
                                 where (p is Panel && p != panel)
                                 select p;

            foreach (Panel p in panels)
                p.Hide();

            panel.Show();
        }

        private void accountBtn_Click(object sender, EventArgs e)
        {
            ShowPanel(profilePanel);
        }

        private void aboutBtn_Click(object sender, EventArgs e)
        {
            ShowPanel(aboutPanel);
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            ShowPanel(settingsPanel);
        }

        private async void pUsernameChangeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(pUsernameTextBox.Text, @"^[a-zA-Z][a-zA-Z0-9]{4,20}$"))
                {
                    await SQLAcc.UpdateUsername(pUsernameTextBox.Text);
                    MessageBox.Show(HelpDialogLang.pUsernameChanged + pUsernameTextBox.Text + "'", HelpDialogLang.pUsernameT, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                    MessageBox.Show(LoginFormLang.invalidFormatU, LoginFormLang.invalidFormatT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (SqlException)
            {
                MessageBox.Show(HelpDialogLang.pUsernameTaken, HelpDialogLang.pUsernameT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void pPasswordChangeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(pPasswordTextBox.Text, @"^[a-zA-Z0-9]{4,}$"))
                {
                    await SQLAcc.UpdatePassword(pPasswordTextBox.Text);
                    MessageBox.Show(HelpDialogLang.pPasswordChanged, HelpDialogLang.pPasswordT, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Application.Restart();
                }

                else
                    MessageBox.Show(LoginFormLang.invalidFormatP, LoginFormLang.invalidFormatT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (Exception)
            {
                MessageBox.Show(HelpDialogLang.pPasswordE, HelpDialogLang.pPasswordT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void pAccountDeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(HelpDialogLang.pAccountDeleteQ, HelpDialogLang.pAccountDeleteT, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    await SQLAcc.DeleteAccount();
                    MessageBox.Show(HelpDialogLang.pAccountDeleteS, HelpDialogLang.pAccountDeleteT, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Application.Restart();
                }

                catch (Exception)
                {
                    MessageBox.Show(HelpDialogLang.pAccountDeleteE, HelpDialogLang.pAccountDeleteT, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sLangListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked && sLangListBox.CheckedItems.Count > 0)
            {
                sLangListBox.ItemCheck -= sLangListBox_ItemCheck;
                sLangListBox.SetItemChecked(sLangListBox.CheckedIndices[0], false);
                sLangListBox.ItemCheck += sLangListBox_ItemCheck;
            }
        }

        private void sLangApplyBtn_Click(object sender, EventArgs e)
        {
            long bufferSize = 0;

            try
            {
                bufferSize = Convert.ToInt64(sBufferSize.Text);
            } catch (Exception) { sBufferSize.Text = "Invalid format"; }

            if (sLangListBox.CheckedItems.Count == 1)
                Properties.Settings.Default.Lang = ((Language)sLangListBox.SelectedItems[0]).Code;

            if (Properties.Settings.Default.BufferSize / bufferSizeConst != bufferSize)
                MessageBox.Show(HelpDialogLang.sBufferChange, HelpDialogLang.sChangeT, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (bufferSize != 0)
                Properties.Settings.Default.BufferSize = bufferSize * bufferSizeConst;

            Properties.Settings.Default.LBDesc = sDescCheckBox.Checked;
            Properties.Settings.Default.AllowDrop = sDropCheckBox.Checked;
            Properties.Settings.Default.Save();

            MessageBox.Show(HelpDialogLang.sRestartQ, HelpDialogLang.sChangeT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void sResetBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(HelpDialogLang.sResetQ, HelpDialogLang.sChangeT, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Properties.Settings.Default.Reset();
                    MessageBox.Show(HelpDialogLang.sRestartQ, HelpDialogLang.sChangeT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            catch (Exception)
            {
                MessageBox.Show(HelpDialogLang.sResetE, HelpDialogLang.sChangeT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sDeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(HelpDialogLang.sDeleteQ, HelpDialogLang.sChangeT, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    System.IO.Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FES__File_Encryption_Soft", true);
                    MessageBox.Show(HelpDialogLang.sRestartQ, HelpDialogLang.sChangeT, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            catch (Exception)
            {
                MessageBox.Show(HelpDialogLang.sDeleteE, HelpDialogLang.sChangeT, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
