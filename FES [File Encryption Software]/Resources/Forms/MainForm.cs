using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using FES__File_Encryption_Software_.Resources.Languages.MainForm;

namespace FES__File_Encryption_Software_
{
    public partial class MainForm : Form
    {
        //Creates MainForm's ToolTip
        private ToolTip toolTip = new ToolTip();

        public MainForm()
        {
            InitializeComponent();

            //Localization
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Properties.Settings.Default.Lang);

            //Adds up tips for buttons
            toolTip.SetToolTip(btnRefresh, MainFormLang.btnRefresh);
            toolTip.SetToolTip(btnHelp, MainFormLang.btnHelp);
            toolTip.SetToolTip(btnLogout, MainFormLang.btnLogout);

            //Allows drag and drop
            AllowDrop = Properties.Settings.Default.AllowDrop;

            //Events
            DragEnter += MainForm_DragEnter;
            DragDrop += MainForm_DragDrop;
            if (Properties.Settings.Default.LBDesc) listBoxUF.MouseMove += ListBox_MouseMove;
            if (Properties.Settings.Default.LBDesc) listBoxEF.MouseMove += ListBox_MouseMove;

            usernameLabel.Text = MainFormLang.usernameLabel + " " + SQLAcc.LoggedUser;
            SQLAcc.OnUsernameUpdate += () => usernameLabel.Text = MainFormLang.usernameLabel + " " + SQLAcc.LoggedUser;

            try
            {
                Task.Run(async () =>
                {
                    Key.SetValue(Convert.FromBase64String(await SQLAcc.GetKey()));
                }).GetAwaiter().GetResult();
            }

            catch (Exception error)
            {
                Key.SetValue(Key.Generate());
                MessageBox.Show(MainFormLang.errorMsg0R + "\n\n" + error.ToString(), MainFormLang.errorLabel0R, MessageBoxButtons.OK);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;

            else
                e.Effect = DragDropEffects.None;
        }

        private async void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            InputEnabled = false;
            bool containsDuals = false;

            string[] files = await Task.Run(() => (string[])e.Data.GetData(DataFormats.FileDrop));

            for (int i = 0; i < files.Length; i++)
            {
                if (!containsDuals)
                    containsDuals = (files.Contains(files[i] + ".AES") || files.Contains(files[i].Substring(0, files[i].Length - 4)));

                else
                    break;
            }

            try
            {
                if (!containsDuals)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        string filepath = files[i];
                        int progressMultiplier = i * 100;
                        IProgress<byte> progress = new Progress<byte>((byte b) => ProgressBarUpdate((byte)((b + progressMultiplier) / (files.Length))));

                        if (!filepath.EndsWith(".AES"))
                        {
                            using (Task t = Program.Encrypt(filepath, Properties.Settings.Default.BufferSize, progress))
                            {
                                await t;
                                File.Delete(filepath);
                            }
                        }

                        else
                        {
                            using (Task t = Program.Decrypt(filepath, Properties.Settings.Default.BufferSize, progress))
                            {
                                await t;
                                File.Delete(filepath);
                            }
                        }
                    }

                    await Task.Delay(800);
                    ProgressBarUpdate(0);
                }

                else { MessageBox.Show(MainFormLang.errorMsg0D, MainFormLang.errorLabel0D, MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }

            catch (System.Security.Cryptography.CryptographicException)
            {
                errorLabel.Text = MainFormLang.errorLabel0C;
                await Task.Delay(1200);
                errorLabel.Text = "";
            }

            catch (FileNotFoundException)
            {
                errorLabel.Text = MainFormLang.errorLabel0F;
                await Task.Delay(1200);
                errorLabel.Text = "";
            }

            catch (IOException)
            {
                errorLabel.Text = MainFormLang.errorLabel1F;
                await Task.Delay(1200);
                errorLabel.Text = "";
            }

            InputEnabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = InProgress;

            if (InProgress)
                MessageBox.Show(MainFormLang.errorMsg0E, MainFormLang.errorLabel0E, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ListBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                ListBox lb = (ListBox)sender;
                int i = lb.IndexFromPoint(e.Location);

                if (i >= 0 && i < lb.Items.Count && lb.Focused)
                {
                    try
                    {
                        long lenght = new FileInfo(DirectoryPath + lb.Items[i]).Length;
                        toolTip.SetToolTip(lb, String.Format(lb.Items[i] + " | " + (
                            (lenght < 1024) ? lenght + " B" 
                            : ((lenght / 1024) < 1024) ? (lenght / 1024) + " KB" 
                            : (lenght / 1024 / 1024) + " MB")
                        ));
                    } catch (FileNotFoundException) { toolTip.SetToolTip(lb, "File not found"); }
                } else { toolTip.Hide(lb); }
            } catch (Exception) { }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            resourcesMonitor.RunWorkerAsync();
            connectionChecker.RunWorkerAsync();
        }

        private static string _directoryPath;
        /// <summary>
        /// Path of the selected directory
        /// </summary>
        internal static string DirectoryPath
        {
            get { return _directoryPath; }
            set { _directoryPath = value + Path.DirectorySeparatorChar; }
        }

        /// <summary>
        /// Indicates whether the <see cref="Form"/> is doing a asynchronous <see cref="Task"/>
        /// </summary>
        internal bool InProgress { get; private set; }

        /// <summary>
        /// Gets or sets an enability of all <see cref="Form"/>'s <see cref="Control"/>
        /// </summary>
        private bool InputEnabled
        {
            get => !InProgress;
            set
            {
                UseWaitCursor = !value;
                InProgress = !value;
                foreach (Control c in Controls)
                    if (c is Button || c is ListBox)
                        c.Enabled = value;
            }
        }

        /// <summary>
        /// Resets content of <see cref="ListBox"/>es
        /// </summary>
        private void ListBoxReset()
        {
            listBoxUF.Items.Clear();
            listBoxEF.Items.Clear();

            if (DirectoryPath != null)
            {
                foreach (string s in Directory.GetFiles(DirectoryPath))
                {
                    if (s.EndsWith(".AES"))
                        listBoxEF.Items.Add(Path.GetFileName(s));

                    else
                        listBoxUF.Items.Add(Path.GetFileName(s));
                }
            }
        }

        /// <summary>
        /// Updates <see cref="progressBar"/>
        /// </summary>
        /// <param name="percentage">Number of percentage</param>
        private void ProgressBarUpdate(byte percentage)
        {
            if (InvokeRequired)
                Invoke(new Action<byte>(ProgressBarUpdate), percentage);

            else
            {
                if (percentage <= 100)
                    progressBar.Value = percentage;

                else
                    throw new ArgumentOutOfRangeException("Percentage must be a number from 0 to 100", "percentage");
            }
        }

        private void btnLDirectory_Click(object sender, EventArgs e)
        {
            folderBrowser.ShowDialog();

            if (folderBrowser.SelectedPath != "")
            {
                DirectoryPath = folderBrowser.SelectedPath;
                folderBrowser.Reset();
            }

            ListBoxReset();
        }

        private async void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (listBoxUF.Items.Count != 0 && listBoxUF.SelectedIndex != -1)
            {
                InputEnabled = false;

                try
                {
                    for (int i = 0; i < listBoxUF.SelectedItems.Count; i++)
                    {
                        string select = DirectoryPath + listBoxUF.SelectedItems[i].ToString();
                        int count = listBoxUF.SelectedItems.Count;

                        int progressMultiplier = i * 100;
                        IProgress<byte> progress = new Progress<byte>((byte b) => ProgressBarUpdate((byte)((b + progressMultiplier) / count)));

                        await Program.Encrypt(select, Properties.Settings.Default.BufferSize, progress);
                        File.Delete(DirectoryPath + listBoxUF.SelectedItems[i].ToString());
                    }
                }

                catch (FileNotFoundException)
                {
                    errorLabel.Text = MainFormLang.errorLabel0F;
                    await Task.Delay(1200);
                    errorLabel.Text = "";
                }

                catch (IOException)
                {
                    errorLabel.Text = MainFormLang.errorLabel1F;
                    await Task.Delay(1200);
                    errorLabel.Text = "";
                }

                ListBoxReset();
                InputEnabled = true;
                await Task.Delay(800);
                ProgressBarUpdate(0);
                GC.Collect();
            }
        }

        private async void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (listBoxEF.Items.Count != 0 && listBoxEF.SelectedIndex != -1)
            {
                InputEnabled = false;

                try
                {
                    for (int i = 0; i < listBoxEF.SelectedItems.Count; i++)
                    {
                        string select = DirectoryPath + listBoxEF.SelectedItems[i].ToString();
                        int count = listBoxEF.SelectedItems.Count;

                        int progressMultiplier = i * 100;
                        Progress<byte> progress = new Progress<byte>((byte b) => ProgressBarUpdate((byte)((b + progressMultiplier) / count)));

                        await Program.Decrypt(select, Properties.Settings.Default.BufferSize, progress);
                        File.Delete(DirectoryPath + listBoxEF.SelectedItems[i].ToString());
                    }
                }

                catch (System.Security.Cryptography.CryptographicException)
                {
                    errorLabel.Text = MainFormLang.errorLabel0C;
                    await Task.Delay(1200);
                    errorLabel.Text = "";
                }

                catch (FileNotFoundException)
                {
                    errorLabel.Text = MainFormLang.errorLabel0F;
                    await Task.Delay(1200);
                    errorLabel.Text = "";
                }

                catch (IOException)
                {
                    errorLabel.Text = MainFormLang.errorLabel1F;
                    await Task.Delay(1200);
                    errorLabel.Text = "";
                }

                ListBoxReset();
                InputEnabled = true;
                await Task.Delay(800);
                ProgressBarUpdate(0);
                GC.Collect();
            }
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MainFormLang.btnResetQ, MainFormLang.btnResetT, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await SQLAcc.UpdateKey();
                Application.Restart();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            using (ShowKeyDialog dialog = new ShowKeyDialog())
                dialog.ShowDialog();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            using (SetKeyDialog dialog = new SetKeyDialog())
                dialog.ShowDialog();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            using (HelpDialog dialog = new HelpDialog())
                dialog.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListBoxReset();
        }

        private void listBoxUF_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEncrypt_Click(this, null);
        }

        private void listBoxEF_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDecrypt_Click(this, null);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private async void resourcesMonitor_DoWork(object sender, DoWorkEventArgs e)
        {
            PerformanceCounter Timer_Tick_PC = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            while (true)
            {
                uint memory = Convert.ToUInt32(Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024);
                byte cpu = (byte)Math.Round((decimal)Timer_Tick_PC.NextValue());

                try
                {
                    Invoke(new Action(() =>
                    {
                        Text = "File Encryption Software [FES]   [ RAM: " + memory + " MB" + "   |   CPU: " + cpu + "% ]";
                    }));
                }

                catch (Exception) { }
                finally { await Task.Delay(1000); }
            }
        }

        private async void connectionChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    Invoke(new Action(() =>
                    {
                        connectionLabel.Text = "SQL: Offline";
                    }));

                    bool IsConnected = await SQLAcc.CheckConnection();

                    try
                    {
                        Invoke(new Action(() =>
                        {
                            if (IsConnected)
                                connectionLabel.Text = "SQL: Online";
                        }));
                    }

                    catch (Exception) { }
                    finally { await Task.Delay(2500); }
                }
            }

            catch (Exception) { }
        }
    }
}
