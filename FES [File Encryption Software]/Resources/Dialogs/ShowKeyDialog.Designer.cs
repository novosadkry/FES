namespace FES__File_Encryption_Software_
{
    partial class ShowKeyDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.keyLabel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnClipboard = new System.Windows.Forms.Button();
            this.clipboardLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lenghtLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = Resources.Languages.MainForm.MainFormLang.showKeyKLabel;
            // 
            // keyLabel
            // 
            this.keyLabel.AutoSize = true;
            this.keyLabel.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.keyLabel.Location = new System.Drawing.Point(50, 13);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(0, 15);
            this.keyLabel.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(319, 52);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(45, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClipboard
            // 
            this.btnClipboard.Location = new System.Drawing.Point(197, 52);
            this.btnClipboard.Name = "btnClipboard";
            this.btnClipboard.Size = new System.Drawing.Size(116, 23);
            this.btnClipboard.TabIndex = 3;
            this.btnClipboard.Text = Resources.Languages.MainForm.MainFormLang.showKeyCCBtn;
            this.btnClipboard.UseVisualStyleBackColor = true;
            this.btnClipboard.Click += new System.EventHandler(this.btnClipboard_Click);
            // 
            // clipboardLabel
            // 
            this.clipboardLabel.AutoSize = true;
            this.clipboardLabel.ForeColor = System.Drawing.Color.DarkBlue;
            this.clipboardLabel.Location = new System.Drawing.Point(13, 59);
            this.clipboardLabel.Name = "clipboardLabel";
            this.clipboardLabel.Size = new System.Drawing.Size(0, 16);
            this.clipboardLabel.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = Resources.Languages.MainForm.MainFormLang.showKeyLLabel;
            // 
            // lenghtLabel
            // 
            this.lenghtLabel.AutoSize = true;
            this.lenghtLabel.Location = new System.Drawing.Point(61, 29);
            this.lenghtLabel.Name = "lenghtLabel";
            this.lenghtLabel.Size = new System.Drawing.Size(0, 16);
            this.lenghtLabel.TabIndex = 6;
            // 
            // ShowKeyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 87);
            this.Controls.Add(this.lenghtLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clipboardLabel);
            this.Controls.Add(this.btnClipboard);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.keyLabel);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShowKeyDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = Resources.Languages.MainForm.MainFormLang.showKeyTitle;
            this.Load += new System.EventHandler(this.ShowKeyDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label keyLabel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnClipboard;
        private System.Windows.Forms.Label clipboardLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lenghtLabel;
    }
}