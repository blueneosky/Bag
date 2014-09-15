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
            this._solutionTargetsTabPage = new System.Windows.Forms.TabPage();
            this._solutionTargetsEditorUserControl = new FastBuildGen.Control.SolutionTargetsEditor.SolutionTargetsEditorUserControl();
            this._macroSolutionTargetsTabPage = new System.Windows.Forms.TabPage();
            this._macroSolutionTargetsEditorUserControl = new FastBuildGen.Control.MacroSolutionTargetsEditor.MacroSolutionTargetsEditorUserControl();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this._saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this._mergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this._quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainTabControl.SuspendLayout();
            this._solutionTargetsTabPage.SuspendLayout();
            this._macroSolutionTargetsTabPage.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this._menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainTabControl
            // 
            this._mainTabControl.Controls.Add(this._solutionTargetsTabPage);
            this._mainTabControl.Controls.Add(this._macroSolutionTargetsTabPage);
            this._mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainTabControl.Location = new System.Drawing.Point(3, 3);
            this._mainTabControl.Name = "_mainTabControl";
            this._mainTabControl.SelectedIndex = 0;
            this._mainTabControl.Size = new System.Drawing.Size(764, 466);
            this._mainTabControl.TabIndex = 0;
            // 
            // _solutionTargetsTabPage
            // 
            this._solutionTargetsTabPage.Controls.Add(this._solutionTargetsEditorUserControl);
            this._solutionTargetsTabPage.Location = new System.Drawing.Point(4, 22);
            this._solutionTargetsTabPage.Name = "_solutionTargetsTabPage";
            this._solutionTargetsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._solutionTargetsTabPage.Size = new System.Drawing.Size(756, 440);
            this._solutionTargetsTabPage.TabIndex = 0;
            this._solutionTargetsTabPage.Text = "Projects";
            this._solutionTargetsTabPage.UseVisualStyleBackColor = true;
            // 
            // _solutionTargetsEditorUserControl
            // 
            this._solutionTargetsEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._solutionTargetsEditorUserControl.DoubleBufferedEx = false;
            this._solutionTargetsEditorUserControl.Location = new System.Drawing.Point(3, 3);
            this._solutionTargetsEditorUserControl.Name = "_solutionTargetsEditorUserControl";
            this._solutionTargetsEditorUserControl.Size = new System.Drawing.Size(750, 434);
            this._solutionTargetsEditorUserControl.TabIndex = 0;
            // 
            // _macroSolutionTargetsTabPage
            // 
            this._macroSolutionTargetsTabPage.Controls.Add(this._macroSolutionTargetsEditorUserControl);
            this._macroSolutionTargetsTabPage.Location = new System.Drawing.Point(4, 22);
            this._macroSolutionTargetsTabPage.Name = "_macroSolutionTargetsTabPage";
            this._macroSolutionTargetsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this._macroSolutionTargetsTabPage.Size = new System.Drawing.Size(756, 440);
            this._macroSolutionTargetsTabPage.TabIndex = 1;
            this._macroSolutionTargetsTabPage.Text = "Macros";
            this._macroSolutionTargetsTabPage.UseVisualStyleBackColor = true;
            // 
            // _macroSolutionTargetsEditorUserControl
            // 
            this._macroSolutionTargetsEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._macroSolutionTargetsEditorUserControl.DoubleBufferedEx = false;
            this._macroSolutionTargetsEditorUserControl.Location = new System.Drawing.Point(3, 3);
            this._macroSolutionTargetsEditorUserControl.Name = "_macroSolutionTargetsEditorUserControl";
            this._macroSolutionTargetsEditorUserControl.Size = new System.Drawing.Size(750, 434);
            this._macroSolutionTargetsEditorUserControl.TabIndex = 0;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this._mainTabControl);
            this.toolStripContainer1.ContentPanel.Padding = new System.Windows.Forms.Padding(3);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(770, 472);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(770, 496);
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
            this._menuStrip.Size = new System.Drawing.Size(770, 24);
            this._menuStrip.TabIndex = 2;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _fileToolStripMenuItem
            // 
            this._fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newToolStripMenuItem,
            this._openToolStripMenuItem,
            this.toolStripMenuItem2,
            this._saveToolStripMenuItem,
            this._saveAsToolStripMenuItem,
            this.toolStripMenuItem4,
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
            this._newToolStripMenuItem.Text = "&New (from sln)";
            this._newToolStripMenuItem.Click += new System.EventHandler(this._newToolStripMenuItem_Click);
            // 
            // _openToolStripMenuItem
            // 
            this._openToolStripMenuItem.Name = "_openToolStripMenuItem";
            this._openToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this._openToolStripMenuItem.Text = "&Open";
            this._openToolStripMenuItem.Click += new System.EventHandler(this._openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 6);
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
            this.ClientSize = new System.Drawing.Size(770, 496);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this._menuStrip;
            this.Name = "MainForm";
            this.Text = "FastBuild Generator";
            this._mainTabControl.ResumeLayout(false);
            this._solutionTargetsTabPage.ResumeLayout(false);
            this._macroSolutionTargetsTabPage.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage _solutionTargetsTabPage;
        private System.Windows.Forms.TabPage _macroSolutionTargetsTabPage;
        private Control.SolutionTargetsEditor.SolutionTargetsEditorUserControl _solutionTargetsEditorUserControl;
        private Control.MacroSolutionTargetsEditor.MacroSolutionTargetsEditorUserControl _macroSolutionTargetsEditorUserControl;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripMenuItem _mergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem _newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}