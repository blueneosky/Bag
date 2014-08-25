namespace FastBuildGen.Forms.Main
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
            this._mainTabControl = new System.Windows.Forms.TabControl();
            this._modulesTabPage = new System.Windows.Forms.TabPage();
            this._modulesEditorUserControl = new FastBuildGen.Control.ModulesEditor.ModulesEditorUserControl();
            this._targetsTabPage = new System.Windows.Forms.TabPage();
            this._targetsEditorUserControl = new FastBuildGen.Control.TargetsEditor.TargetsEditorUserControl();
            this._propertiesTabPage = new System.Windows.Forms.TabPage();
            this._internalVarsEditorUserControl = new FastBuildGen.Control.InternalVarsEditor.InternalVarsEditorUserControl();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this._importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this._mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this._quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainTabControl.SuspendLayout();
            this._modulesTabPage.SuspendLayout();
            this._targetsTabPage.SuspendLayout();
            this._propertiesTabPage.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this._menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainTabControl
            // 
            this._mainTabControl.Controls.Add(this._modulesTabPage);
            this._mainTabControl.Controls.Add(this._targetsTabPage);
            this._mainTabControl.Controls.Add(this._propertiesTabPage);
            this._mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainTabControl.Location = new System.Drawing.Point(3, 3);
            this._mainTabControl.Name = "_mainTabControl";
            this._mainTabControl.SelectedIndex = 0;
            this._mainTabControl.Size = new System.Drawing.Size(848, 482);
            this._mainTabControl.TabIndex = 0;
            this._mainTabControl.SelectedIndexChanged += new System.EventHandler(this._mainTabControl_SelectedIndexChanged);
            // 
            // _modulesTabPage
            // 
            this._modulesTabPage.Controls.Add(this._modulesEditorUserControl);
            this._modulesTabPage.Location = new System.Drawing.Point(4, 22);
            this._modulesTabPage.Name = "_modulesTabPage";
            this._modulesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._modulesTabPage.Size = new System.Drawing.Size(840, 456);
            this._modulesTabPage.TabIndex = 0;
            this._modulesTabPage.Text = "Modules";
            this._modulesTabPage.UseVisualStyleBackColor = true;
            // 
            // _modulesEditorUserControl
            // 
            this._modulesEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._modulesEditorUserControl.DoubleBufferedEx = false;
            this._modulesEditorUserControl.Location = new System.Drawing.Point(3, 3);
            this._modulesEditorUserControl.Name = "_modulesEditorUserControl";
            this._modulesEditorUserControl.Size = new System.Drawing.Size(834, 450);
            this._modulesEditorUserControl.TabIndex = 0;
            // 
            // _targetsTabPage
            // 
            this._targetsTabPage.Controls.Add(this._targetsEditorUserControl);
            this._targetsTabPage.Location = new System.Drawing.Point(4, 22);
            this._targetsTabPage.Name = "_targetsTabPage";
            this._targetsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._targetsTabPage.Size = new System.Drawing.Size(840, 456);
            this._targetsTabPage.TabIndex = 1;
            this._targetsTabPage.Text = "Targets";
            this._targetsTabPage.UseVisualStyleBackColor = true;
            // 
            // _targetsEditorUserControl
            // 
            this._targetsEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._targetsEditorUserControl.DoubleBufferedEx = false;
            this._targetsEditorUserControl.Location = new System.Drawing.Point(3, 3);
            this._targetsEditorUserControl.Name = "_targetsEditorUserControl";
            this._targetsEditorUserControl.Size = new System.Drawing.Size(186, 68);
            this._targetsEditorUserControl.TabIndex = 0;
            // 
            // _propertiesTabPage
            // 
            this._propertiesTabPage.Controls.Add(this._internalVarsEditorUserControl);
            this._propertiesTabPage.Location = new System.Drawing.Point(4, 22);
            this._propertiesTabPage.Name = "_propertiesTabPage";
            this._propertiesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._propertiesTabPage.Size = new System.Drawing.Size(840, 456);
            this._propertiesTabPage.TabIndex = 2;
            this._propertiesTabPage.Text = "Properties";
            this._propertiesTabPage.UseVisualStyleBackColor = true;
            // 
            // _internalVarsEditorUserControl
            // 
            this._internalVarsEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._internalVarsEditorUserControl.DoubleBufferedEx = false;
            this._internalVarsEditorUserControl.Location = new System.Drawing.Point(3, 3);
            this._internalVarsEditorUserControl.Name = "_internalVarsEditorUserControl";
            this._internalVarsEditorUserControl.Size = new System.Drawing.Size(186, 68);
            this._internalVarsEditorUserControl.TabIndex = 0;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this._mainTabControl);
            this.toolStripContainer1.ContentPanel.Padding = new System.Windows.Forms.Padding(3);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(854, 488);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(854, 512);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this._menuStrip);
            // 
            // _menuStrip
            // 
            this._menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(854, 24);
            this._menuStrip.TabIndex = 2;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _fileToolStripMenuItem
            // 
            this._fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newToolStripMenuItem,
            this._saveToolStripMenuItem,
            this._saveAsToolStripMenuItem,
            this.toolStripMenuItem4,
            this._importToolStripMenuItem,
            this.toolStripMenuItem3,
            this._mergeToolStripMenuItem,
            this.toolStripMenuItem1,
            this._quitToolStripMenuItem});
            this._fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
            this._fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this._fileToolStripMenuItem.Text = "&File";
            // 
            // _newToolStripMenuItem
            // 
            this._newToolStripMenuItem.Name = "_newToolStripMenuItem";
            this._newToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this._newToolStripMenuItem.Text = "&New";
            // 
            // _saveToolStripMenuItem
            // 
            this._saveToolStripMenuItem.Name = "_saveToolStripMenuItem";
            this._saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this._saveToolStripMenuItem.Text = "&Save";
            this._saveToolStripMenuItem.Click += new System.EventHandler(this._saveToolStripMenuItem_Click);
            // 
            // _saveAsToolStripMenuItem
            // 
            this._saveAsToolStripMenuItem.Name = "_saveAsToolStripMenuItem";
            this._saveAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this._saveAsToolStripMenuItem.Text = "Save &as";
            this._saveAsToolStripMenuItem.Click += new System.EventHandler(this._saveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 6);
            // 
            // _importToolStripMenuItem
            // 
            this._importToolStripMenuItem.Name = "_importToolStripMenuItem";
            this._importToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this._importToolStripMenuItem.Text = "&Import";
            this._importToolStripMenuItem.Click += new System.EventHandler(this._importToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 6);
            // 
            // _mergeToolStripMenuItem
            // 
            this._mergeToolStripMenuItem.Name = "_mergeToolStripMenuItem";
            this._mergeToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this._mergeToolStripMenuItem.Text = "&Merge (import)";
            this._mergeToolStripMenuItem.Click += new System.EventHandler(this._mergeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
            // 
            // _quitToolStripMenuItem
            // 
            this._quitToolStripMenuItem.Name = "_quitToolStripMenuItem";
            this._quitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this._quitToolStripMenuItem.Text = "&Quit";
            this._quitToolStripMenuItem.Click += new System.EventHandler(this._quitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 512);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this._menuStrip;
            this.Name = "MainForm";
            this.Text = "FastBuild Generator";
            this._mainTabControl.ResumeLayout(false);
            this._modulesTabPage.ResumeLayout(false);
            this._targetsTabPage.ResumeLayout(false);
            this._propertiesTabPage.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _mainTabControl;
        private System.Windows.Forms.TabPage _modulesTabPage;
        private System.Windows.Forms.TabPage _targetsTabPage;
        private Control.ModulesEditor.ModulesEditorUserControl _modulesEditorUserControl;
        private Control.TargetsEditor.TargetsEditorUserControl _targetsEditorUserControl;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem _mergeToolStripMenuItem;
        private System.Windows.Forms.TabPage _propertiesTabPage;
        private Control.InternalVarsEditor.InternalVarsEditorUserControl _internalVarsEditorUserControl;
        private System.Windows.Forms.ToolStripMenuItem _saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem _newToolStripMenuItem;
    }
}