namespace FastBuildGen.Control.MacroSolutionTargetEditor
{
    partial class MacroSolutionTargetEditorUserControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._availableGroupBox = new System.Windows.Forms.GroupBox();
            this._availablePanel = new System.Windows.Forms.Panel();
            this._availableListBox = new System.Windows.Forms.ListBox();
            this._modulesGroupBox = new System.Windows.Forms.GroupBox();
            this._modulesPanel = new System.Windows.Forms.Panel();
            this._projectsListBox = new System.Windows.Forms.ListBox();
            this._pdEditorUserControl = new FastBuildGen.Control.PDEditor.PDEditorUserControl();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._availableGroupBox.SuspendLayout();
            this._availablePanel.SuspendLayout();
            this._modulesGroupBox.SuspendLayout();
            this._modulesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 186);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._availableGroupBox);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._modulesGroupBox);
            this._splitContainer.Size = new System.Drawing.Size(698, 434);
            this._splitContainer.SplitterDistance = 344;
            this._splitContainer.TabIndex = 1;
            // 
            // _availableGroupBox
            // 
            this._availableGroupBox.Controls.Add(this._availablePanel);
            this._availableGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._availableGroupBox.Location = new System.Drawing.Point(0, 0);
            this._availableGroupBox.Name = "_availableGroupBox";
            this._availableGroupBox.Size = new System.Drawing.Size(344, 434);
            this._availableGroupBox.TabIndex = 0;
            this._availableGroupBox.TabStop = false;
            this._availableGroupBox.Text = "Available projects";
            // 
            // _availablePanel
            // 
            this._availablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._availablePanel.Controls.Add(this._availableListBox);
            this._availablePanel.Location = new System.Drawing.Point(6, 19);
            this._availablePanel.Name = "_availablePanel";
            this._availablePanel.Size = new System.Drawing.Size(332, 409);
            this._availablePanel.TabIndex = 1;
            // 
            // _availableListBox
            // 
            this._availableListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._availableListBox.FormattingEnabled = true;
            this._availableListBox.Location = new System.Drawing.Point(0, 0);
            this._availableListBox.Name = "_availableListBox";
            this._availableListBox.Size = new System.Drawing.Size(332, 409);
            this._availableListBox.TabIndex = 0;
            this._availableListBox.DoubleClick += new System.EventHandler(this._availableListBox_DoubleClick);
            // 
            // _modulesGroupBox
            // 
            this._modulesGroupBox.Controls.Add(this._modulesPanel);
            this._modulesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._modulesGroupBox.Location = new System.Drawing.Point(0, 0);
            this._modulesGroupBox.Name = "_modulesGroupBox";
            this._modulesGroupBox.Size = new System.Drawing.Size(350, 434);
            this._modulesGroupBox.TabIndex = 0;
            this._modulesGroupBox.TabStop = false;
            this._modulesGroupBox.Text = "Projects build for this macro";
            // 
            // _modulesPanel
            // 
            this._modulesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._modulesPanel.Controls.Add(this._projectsListBox);
            this._modulesPanel.Location = new System.Drawing.Point(6, 19);
            this._modulesPanel.Name = "_modulesPanel";
            this._modulesPanel.Size = new System.Drawing.Size(338, 409);
            this._modulesPanel.TabIndex = 1;
            // 
            // _projectsListBox
            // 
            this._projectsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._projectsListBox.FormattingEnabled = true;
            this._projectsListBox.Location = new System.Drawing.Point(0, 0);
            this._projectsListBox.Name = "_projectsListBox";
            this._projectsListBox.Size = new System.Drawing.Size(338, 409);
            this._projectsListBox.TabIndex = 0;
            this._projectsListBox.DoubleClick += new System.EventHandler(this._modulesListBox_DoubleClick);
            // 
            // _pdEditorUserControl
            // 
            this._pdEditorUserControl.Dock = System.Windows.Forms.DockStyle.Top;
            this._pdEditorUserControl.DoubleBufferedEx = false;
            this._pdEditorUserControl.Location = new System.Drawing.Point(0, 0);
            this._pdEditorUserControl.Name = "_pdEditorUserControl";
            this._pdEditorUserControl.Size = new System.Drawing.Size(698, 186);
            this._pdEditorUserControl.TabIndex = 0;
            // 
            // MacroSolutionTargetEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this._pdEditorUserControl);
            this.Name = "MacroSolutionTargetEditorUserControl";
            this.Size = new System.Drawing.Size(698, 620);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._availableGroupBox.ResumeLayout(false);
            this._availablePanel.ResumeLayout(false);
            this._modulesGroupBox.ResumeLayout(false);
            this._modulesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PDEditor.PDEditorUserControl _pdEditorUserControl;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.GroupBox _availableGroupBox;
        private System.Windows.Forms.GroupBox _modulesGroupBox;
        private System.Windows.Forms.ListBox _availableListBox;
        private System.Windows.Forms.ListBox _projectsListBox;
        private System.Windows.Forms.Panel _availablePanel;
        private System.Windows.Forms.Panel _modulesPanel;
    }
}
