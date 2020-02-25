namespace FES__File_Encryption_Software_
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listBoxUF = new System.Windows.Forms.ListBox();
            this.listBoxEF = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnLDirectory = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnReset = new System.Windows.Forms.Button();
            this.errorLabel = new System.Windows.Forms.Label();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.resourcesMonitor = new System.ComponentModel.BackgroundWorker();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.connectionChecker = new System.ComponentModel.BackgroundWorker();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxUF
            // 
            this.listBoxUF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxUF.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.listBoxUF.FormattingEnabled = true;
            this.listBoxUF.ItemHeight = 16;
            this.listBoxUF.Location = new System.Drawing.Point(12, 37);
            this.listBoxUF.Name = "listBoxUF";
            this.listBoxUF.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxUF.Size = new System.Drawing.Size(196, 212);
            this.listBoxUF.Sorted = true;
            this.listBoxUF.TabIndex = 0;
            this.listBoxUF.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxUF_MouseDoubleClick);
            // 
            // listBoxEF
            // 
            this.listBoxEF.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxEF.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.listBoxEF.FormattingEnabled = true;
            this.listBoxEF.ItemHeight = 16;
            this.listBoxEF.Location = new System.Drawing.Point(308, 37);
            this.listBoxEF.Name = "listBoxEF";
            this.listBoxEF.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxEF.Size = new System.Drawing.Size(196, 212);
            this.listBoxEF.Sorted = true;
            this.listBoxEF.TabIndex = 1;
            this.listBoxEF.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxEF_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = global::FES__File_Encryption_Software_.Resources.Languages.MainForm.MainFormLang.nefLabel;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(305, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = global::FES__File_Encryption_Software_.Resources.Languages.MainForm.MainFormLang.efLabel;
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEncrypt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnEncrypt.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btnEncrypt.Location = new System.Drawing.Point(214, 47);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(88, 23);
            this.btnEncrypt.TabIndex = 4;
            this.btnEncrypt.Text = global::FES__File_Encryption_Software_.Resources.Languages.MainForm.MainFormLang.btnEncrypt;
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.btnEncrypt_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecrypt.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btnDecrypt.Location = new System.Drawing.Point(214, 76);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(88, 23);
            this.btnDecrypt.TabIndex = 5;
            this.btnDecrypt.Text = global::FES__File_Encryption_Software_.Resources.Languages.MainForm.MainFormLang.btnDecrypt;
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(13, 258);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(491, 23);
            this.progressBar.TabIndex = 6;
            // 
            // btnLDirectory
            // 
            this.btnLDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLDirectory.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btnLDirectory.Location = new System.Drawing.Point(214, 207);
            this.btnLDirectory.Name = "btnLDirectory";
            this.btnLDirectory.Size = new System.Drawing.Size(88, 42);
            this.btnLDirectory.TabIndex = 7;
            this.btnLDirectory.Text = global::FES__File_Encryption_Software_.Resources.Languages.MainForm.MainFormLang.btnLDirectory;
            this.btnLDirectory.UseVisualStyleBackColor = true;
            this.btnLDirectory.Click += new System.EventHandler(this.btnLDirectory_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btnReset.ForeColor = System.Drawing.Color.Firebrick;
            this.btnReset.Location = new System.Drawing.Point(214, 175);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(88, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = global::FES__File_Encryption_Software_.Resources.Languages.MainForm.MainFormLang.btnReset;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.Transparent;
            this.errorLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.errorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.errorLabel.Location = new System.Drawing.Point(208, 12);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 16);
            this.errorLabel.TabIndex = 9;
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btnShow.Location = new System.Drawing.Point(214, 146);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(88, 23);
            this.btnShow.TabIndex = 10;
            this.btnShow.Text = global::FES__File_Encryption_Software_.Resources.Languages.MainForm.MainFormLang.btnShow;
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSet.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.btnSet.Location = new System.Drawing.Point(214, 117);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(88, 23);
            this.btnSet.TabIndex = 11;
            this.btnSet.Text = global::FES__File_Encryption_Software_.Resources.Languages.MainForm.MainFormLang.btnSet;
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(423, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 23);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Tag = "";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Font = new System.Drawing.Font("Century Gothic", 6.75F);
            this.usernameLabel.Location = new System.Drawing.Point(9, 5);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(30, 13);
            this.usernameLabel.TabIndex = 14;
            this.usernameLabel.Text = "User:";
            // 
            // resourcesMonitor
            // 
            this.resourcesMonitor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.resourcesMonitor_DoWork);
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Font = new System.Drawing.Font("Century Gothic", 6.75F);
            this.connectionLabel.Location = new System.Drawing.Point(305, 5);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(26, 13);
            this.connectionLabel.TabIndex = 15;
            this.connectionLabel.Text = "SQL:";
            // 
            // connectionChecker
            // 
            this.connectionChecker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.connectionChecker_DoWork);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogout.BackgroundImage")));
            this.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Location = new System.Drawing.Point(452, 8);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(23, 23);
            this.btnLogout.TabIndex = 16;
            this.btnLogout.Tag = "";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.BackColor = System.Drawing.Color.Transparent;
            this.btnHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHelp.BackgroundImage")));
            this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHelp.FlatAppearance.BorderSize = 0;
            this.btnHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Location = new System.Drawing.Point(481, 8);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(23, 23);
            this.btnHelp.TabIndex = 17;
            this.btnHelp.Tag = "";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 293);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnLDirectory);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.btnEncrypt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxEF);
            this.Controls.Add(this.listBoxUF);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(532, 500);
            this.MinimumSize = new System.Drawing.Size(532, 332);
            this.Name = "MainForm";
            this.Text = "File Encryption Software [FES]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxUF;
        private System.Windows.Forms.ListBox listBoxEF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnLDirectory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label usernameLabel;
        private System.ComponentModel.BackgroundWorker resourcesMonitor;
        private System.Windows.Forms.Label connectionLabel;
        private System.ComponentModel.BackgroundWorker connectionChecker;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnHelp;
    }
}

