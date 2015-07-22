namespace SkyrimUserSwitch.Controls
{
    partial class PathTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._browseButton = new System.Windows.Forms.Button();
            this._descriptionLabel = new System.Windows.Forms.Label();
            this._pathTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _browseButton
            // 
            this._browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browseButton.Location = new System.Drawing.Point(284, 14);
            this._browseButton.Name = "_browseButton";
            this._browseButton.Size = new System.Drawing.Size(75, 23);
            this._browseButton.TabIndex = 0;
            this._browseButton.Text = "Browse";
            this._browseButton.UseVisualStyleBackColor = true;
            this._browseButton.Click += new System.EventHandler(this._browseButton_Click);
            // 
            // _descriptionLabel
            // 
            this._descriptionLabel.AutoSize = true;
            this._descriptionLabel.Location = new System.Drawing.Point(-3, 0);
            this._descriptionLabel.Name = "_descriptionLabel";
            this._descriptionLabel.Size = new System.Drawing.Size(35, 13);
            this._descriptionLabel.TabIndex = 1;
            this._descriptionLabel.Text = "label1";
            // 
            // _pathTextBox
            // 
            this._pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._pathTextBox.Location = new System.Drawing.Point(0, 16);
            this._pathTextBox.Name = "_pathTextBox";
            this._pathTextBox.Size = new System.Drawing.Size(278, 20);
            this._pathTextBox.TabIndex = 2;
            this._pathTextBox.TextChanged += new System.EventHandler(this._pathTextBox_TextChanged);
            // 
            // PathTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._pathTextBox);
            this.Controls.Add(this._descriptionLabel);
            this.Controls.Add(this._browseButton);
            this.MaximumSize = new System.Drawing.Size(4000, 38);
            this.MinimumSize = new System.Drawing.Size(180, 38);
            this.Name = "PathTextBox";
            this.Size = new System.Drawing.Size(359, 38);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _browseButton;
        private System.Windows.Forms.Label _descriptionLabel;
        private System.Windows.Forms.TextBox _pathTextBox;
    }
}
