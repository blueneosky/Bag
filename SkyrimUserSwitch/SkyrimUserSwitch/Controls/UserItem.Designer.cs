namespace SkyrimUserSwitch.Controls
{
    partial class UserItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._avatarPictureBox = new System.Windows.Forms.PictureBox();
            this._nameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._avatarPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _avatarPictureBox
            // 
            this._avatarPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._avatarPictureBox.Location = new System.Drawing.Point(8, 8);
            this._avatarPictureBox.Name = "_avatarPictureBox";
            this._avatarPictureBox.Size = new System.Drawing.Size(48, 48);
            this._avatarPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._avatarPictureBox.TabIndex = 0;
            this._avatarPictureBox.TabStop = false;
            // 
            // _nameLabel
            // 
            this._nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._nameLabel.Location = new System.Drawing.Point(62, 8);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(74, 48);
            this._nameLabel.TabIndex = 1;
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this._nameLabel);
            this.Controls.Add(this._avatarPictureBox);
            this.MaximumSize = new System.Drawing.Size(0, 64);
            this.MinimumSize = new System.Drawing.Size(136, 64);
            this.Name = "UserItem";
            this.Size = new System.Drawing.Size(136, 64);
            ((System.ComponentModel.ISupportInitialize)(this._avatarPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _avatarPictureBox;
        private System.Windows.Forms.Label _nameLabel;
    }
}
