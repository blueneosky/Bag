namespace SkyrimUserSwitch.Forms
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
            PartialDispose(disposing);

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
            this._changeButton = new System.Windows.Forms.Button();
            this._optionButton = new System.Windows.Forms.Button();
            this._nameTextBox = new System.Windows.Forms.TextBox();
            this._avatarPictureBox = new System.Windows.Forms.PictureBox();
            this._invalideReasonLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._avatarPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _changeButton
            // 
            this._changeButton.Location = new System.Drawing.Point(92, 126);
            this._changeButton.Name = "_changeButton";
            this._changeButton.Size = new System.Drawing.Size(75, 23);
            this._changeButton.TabIndex = 2;
            this._changeButton.Text = "Change";
            this._changeButton.UseVisualStyleBackColor = true;
            this._changeButton.Click += new System.EventHandler(this._changeButton_Click);
            // 
            // _optionButton
            // 
            this._optionButton.Location = new System.Drawing.Point(92, 155);
            this._optionButton.Name = "_optionButton";
            this._optionButton.Size = new System.Drawing.Size(75, 23);
            this._optionButton.TabIndex = 3;
            this._optionButton.Text = "Option";
            this._optionButton.UseVisualStyleBackColor = true;
            this._optionButton.Click += new System.EventHandler(this._optionButton_Click);
            // 
            // _nameTextBox
            // 
            this._nameTextBox.Location = new System.Drawing.Point(66, 76);
            this._nameTextBox.Name = "_nameTextBox";
            this._nameTextBox.ReadOnly = true;
            this._nameTextBox.Size = new System.Drawing.Size(126, 20);
            this._nameTextBox.TabIndex = 1;
            // 
            // _avatarPictureBox
            // 
            this._avatarPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._avatarPictureBox.Location = new System.Drawing.Point(105, 22);
            this._avatarPictureBox.Name = "_avatarPictureBox";
            this._avatarPictureBox.Size = new System.Drawing.Size(48, 48);
            this._avatarPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._avatarPictureBox.TabIndex = 3;
            this._avatarPictureBox.TabStop = false;
            // 
            // _invalideReasonLabel
            // 
            this._invalideReasonLabel.Location = new System.Drawing.Point(12, 9);
            this._invalideReasonLabel.Name = "_invalideReasonLabel";
            this._invalideReasonLabel.Size = new System.Drawing.Size(235, 114);
            this._invalideReasonLabel.TabIndex = 0;
            this._invalideReasonLabel.Text = "invalide reason";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 221);
            this.Controls.Add(this._nameTextBox);
            this.Controls.Add(this._avatarPictureBox);
            this.Controls.Add(this._optionButton);
            this.Controls.Add(this._changeButton);
            this.Controls.Add(this._invalideReasonLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(20, 20);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Skyrim User Change";
            ((System.ComponentModel.ISupportInitialize)(this._avatarPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _changeButton;
        private System.Windows.Forms.Button _optionButton;
        private System.Windows.Forms.PictureBox _avatarPictureBox;
        private System.Windows.Forms.TextBox _nameTextBox;
        private System.Windows.Forms.Label _invalideReasonLabel;
    }
}

