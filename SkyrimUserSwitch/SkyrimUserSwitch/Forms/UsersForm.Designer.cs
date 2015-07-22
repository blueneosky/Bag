namespace SkyrimUserSwitch.Forms
{
    partial class UsersForm
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
            this.components = new System.ComponentModel.Container();
            this._closeButton = new System.Windows.Forms.Button();
            this._userItemsPanel = new System.Windows.Forms.Panel();
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._changeNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._changeAvatarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.Location = new System.Drawing.Point(196, 347);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 23);
            this._closeButton.TabIndex = 2;
            this._closeButton.Text = "&Close";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _userItemsPanel
            // 
            this._userItemsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._userItemsPanel.AutoScroll = true;
            this._userItemsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._userItemsPanel.Location = new System.Drawing.Point(12, 12);
            this._userItemsPanel.Name = "_userItemsPanel";
            this._userItemsPanel.Size = new System.Drawing.Size(259, 329);
            this._userItemsPanel.TabIndex = 0;
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._addToolStripMenuItem,
            this._removeToolStripMenuItem,
            this._changeNameToolStripMenuItem,
            this._changeAvatarToolStripMenuItem});
            this._contextMenuStrip.Name = "_contextMenuStrip";
            this._contextMenuStrip.Size = new System.Drawing.Size(151, 92);
            // 
            // _addToolStripMenuItem
            // 
            this._addToolStripMenuItem.Name = "_addToolStripMenuItem";
            this._addToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this._addToolStripMenuItem.Text = "&Add";
            this._addToolStripMenuItem.Click += new System.EventHandler(this._addToolStripMenuItem_Click);
            // 
            // _removeToolStripMenuItem
            // 
            this._removeToolStripMenuItem.Name = "_removeToolStripMenuItem";
            this._removeToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this._removeToolStripMenuItem.Text = "&Remove";
            this._removeToolStripMenuItem.Click += new System.EventHandler(this._removeToolStripMenuItem_Click);
            // 
            // _changeNameToolStripMenuItem
            // 
            this._changeNameToolStripMenuItem.Name = "_changeNameToolStripMenuItem";
            this._changeNameToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this._changeNameToolStripMenuItem.Text = "Change &name";
            this._changeNameToolStripMenuItem.Click += new System.EventHandler(this._changeNameToolStripMenuItem_Click);
            // 
            // _changeAvatarToolStripMenuItem
            // 
            this._changeAvatarToolStripMenuItem.Name = "_changeAvatarToolStripMenuItem";
            this._changeAvatarToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this._changeAvatarToolStripMenuItem.Text = "Change a&vatar";
            this._changeAvatarToolStripMenuItem.Click += new System.EventHandler(this._changeAvatarToolStripMenuItem_Click);
            // 
            // UsersForm
            // 
            this.AcceptButton = this._closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._closeButton;
            this.ClientSize = new System.Drawing.Size(283, 382);
            this.Controls.Add(this._userItemsPanel);
            this.Controls.Add(this._closeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UsersForm";
            this.Text = "Users";
            this._contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.Panel _userItemsPanel;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _changeNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _changeAvatarToolStripMenuItem;
    }
}