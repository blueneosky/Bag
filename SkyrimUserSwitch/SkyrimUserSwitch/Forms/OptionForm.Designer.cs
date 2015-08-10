namespace SkyrimUserSwitch.Forms
{
    partial class OptionForm
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
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._skyrimFolderPathTextBox = new SkyrimUserSwitch.Controls.PathTextBox();
            this._skyrimUserFolderPathTextBox = new SkyrimUserSwitch.Controls.PathTextBox();
            this._skyrimLauncherPathTextBox = new SkyrimUserSwitch.Controls.PathTextBox();
            this.SuspendLayout();
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(358, 197);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(439, 197);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 3;
            this._okButton.Text = "&OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _skyrimFolderPathTextBox
            // 
            this._skyrimFolderPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._skyrimFolderPathTextBox.Description = "Skyrim folder (wich contains TESV, SkyrimLuncher...)";
            this._skyrimFolderPathTextBox.IsFolderPath = true;
            this._skyrimFolderPathTextBox.Location = new System.Drawing.Point(12, 56);
            this._skyrimFolderPathTextBox.MaximumSize = new System.Drawing.Size(4000, 38);
            this._skyrimFolderPathTextBox.MinimumSize = new System.Drawing.Size(180, 38);
            this._skyrimFolderPathTextBox.Name = "_skyrimFolderPathTextBox";
            this._skyrimFolderPathTextBox.Path = "";
            this._skyrimFolderPathTextBox.Size = new System.Drawing.Size(502, 38);
            this._skyrimFolderPathTextBox.TabIndex = 1;
            // 
            // _skyrimUserFolderPathTextBox
            // 
            this._skyrimUserFolderPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._skyrimUserFolderPathTextBox.Description = "Skyrim user folder (wich contains saves)";
            this._skyrimUserFolderPathTextBox.IsFolderPath = true;
            this._skyrimUserFolderPathTextBox.Location = new System.Drawing.Point(12, 12);
            this._skyrimUserFolderPathTextBox.MaximumSize = new System.Drawing.Size(4000, 38);
            this._skyrimUserFolderPathTextBox.MinimumSize = new System.Drawing.Size(180, 38);
            this._skyrimUserFolderPathTextBox.Name = "_skyrimUserFolderPathTextBox";
            this._skyrimUserFolderPathTextBox.Path = "";
            this._skyrimUserFolderPathTextBox.Size = new System.Drawing.Size(502, 38);
            this._skyrimUserFolderPathTextBox.TabIndex = 0;
            // 
            // _skyrimLauncherPathTextBox
            // 
            this._skyrimLauncherPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._skyrimLauncherPathTextBox.Description = "Skyrim launcher (SkyrimLauncher.exe)";
            this._skyrimLauncherPathTextBox.IsFolderPath = false;
            this._skyrimLauncherPathTextBox.Location = new System.Drawing.Point(12, 100);
            this._skyrimLauncherPathTextBox.MaximumSize = new System.Drawing.Size(4000, 38);
            this._skyrimLauncherPathTextBox.MinimumSize = new System.Drawing.Size(180, 38);
            this._skyrimLauncherPathTextBox.Name = "_skyrimLauncherPathTextBox";
            this._skyrimLauncherPathTextBox.Path = "";
            this._skyrimLauncherPathTextBox.Size = new System.Drawing.Size(502, 38);
            this._skyrimLauncherPathTextBox.TabIndex = 4;
            // 
            // OptionForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(526, 232);
            this.Controls.Add(this._skyrimLauncherPathTextBox);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._skyrimFolderPathTextBox);
            this.Controls.Add(this._skyrimUserFolderPathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.Text = "Options";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PathTextBox _skyrimUserFolderPathTextBox;
        private Controls.PathTextBox _skyrimFolderPathTextBox;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
        private Controls.PathTextBox _skyrimLauncherPathTextBox;

    }
}