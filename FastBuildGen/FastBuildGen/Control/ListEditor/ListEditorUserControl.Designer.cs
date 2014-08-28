namespace FastBuildGen.Control.ListEditor
{
    partial class ListEditorUserControl
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
            this._listGroupBox = new System.Windows.Forms.GroupBox();
            this._listViewEx = new FastBuildGen.Common.Control.ListViewEx();
            this._columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._buttonsPanel = new System.Windows.Forms.Panel();
            this._addModuleButton = new System.Windows.Forms.Button();
            this._editorGroupBox = new System.Windows.Forms.GroupBox();
            this._editorPanel = new System.Windows.Forms.Panel();
            this._listPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._listGroupBox.SuspendLayout();
            this._buttonsPanel.SuspendLayout();
            this._editorGroupBox.SuspendLayout();
            this._listPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._splitContainer.Location = new System.Drawing.Point(0, 0);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._listGroupBox);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._editorGroupBox);
            this._splitContainer.Size = new System.Drawing.Size(708, 458);
            this._splitContainer.SplitterDistance = 260;
            this._splitContainer.TabIndex = 1;
            // 
            // _listGroupBox
            // 
            this._listGroupBox.Controls.Add(this._listPanel);
            this._listGroupBox.Controls.Add(this._buttonsPanel);
            this._listGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listGroupBox.Location = new System.Drawing.Point(0, 0);
            this._listGroupBox.Name = "_listGroupBox";
            this._listGroupBox.Size = new System.Drawing.Size(260, 458);
            this._listGroupBox.TabIndex = 2;
            this._listGroupBox.TabStop = false;
            this._listGroupBox.Text = "List";
            // 
            // _listViewEx
            // 
            this._listViewEx.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._columnHeader});
            this._listViewEx.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewEx.FullRowSelect = true;
            this._listViewEx.HideSelection = false;
            this._listViewEx.Location = new System.Drawing.Point(3, 3);
            this._listViewEx.MultiSelect = false;
            this._listViewEx.Name = "_listViewEx";
            this._listViewEx.Size = new System.Drawing.Size(248, 403);
            this._listViewEx.TabIndex = 0;
            this._listViewEx.UseCompatibleStateImageBehavior = false;
            this._listViewEx.View = System.Windows.Forms.View.Details;
            this._listViewEx.GlobalSelectionChanged += new System.EventHandler(this._listView_GlobalSelectionChanged);
            this._listViewEx.KeyDown += new System.Windows.Forms.KeyEventHandler(this._listView_KeyDown);
            // 
            // _columnHeader
            // 
            this._columnHeader.Width = 277;
            // 
            // _buttonsPanel
            // 
            this._buttonsPanel.Controls.Add(this._addModuleButton);
            this._buttonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttonsPanel.Location = new System.Drawing.Point(3, 425);
            this._buttonsPanel.Name = "_buttonsPanel";
            this._buttonsPanel.Padding = new System.Windows.Forms.Padding(3);
            this._buttonsPanel.Size = new System.Drawing.Size(254, 30);
            this._buttonsPanel.TabIndex = 2;
            // 
            // _addModuleButton
            // 
            this._addModuleButton.Dock = System.Windows.Forms.DockStyle.Right;
            this._addModuleButton.Location = new System.Drawing.Point(140, 3);
            this._addModuleButton.Name = "_addModuleButton";
            this._addModuleButton.Size = new System.Drawing.Size(111, 24);
            this._addModuleButton.TabIndex = 1;
            this._addModuleButton.Text = "Add";
            this._addModuleButton.UseVisualStyleBackColor = true;
            this._addModuleButton.Click += new System.EventHandler(this._addModuleButton_Click);
            // 
            // _editorGroupBox
            // 
            this._editorGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._editorGroupBox.Controls.Add(this._editorPanel);
            this._editorGroupBox.Location = new System.Drawing.Point(0, 0);
            this._editorGroupBox.Name = "_editorGroupBox";
            this._editorGroupBox.Size = new System.Drawing.Size(444, 458);
            this._editorGroupBox.TabIndex = 0;
            this._editorGroupBox.TabStop = false;
            this._editorGroupBox.Text = "Editor";
            // 
            // _editorPanel
            // 
            this._editorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._editorPanel.Location = new System.Drawing.Point(3, 16);
            this._editorPanel.Name = "_editorPanel";
            this._editorPanel.Size = new System.Drawing.Size(438, 439);
            this._editorPanel.TabIndex = 1;
            // 
            // _listPanel
            // 
            this._listPanel.Controls.Add(this._listViewEx);
            this._listPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listPanel.Location = new System.Drawing.Point(3, 16);
            this._listPanel.Name = "_listPanel";
            this._listPanel.Padding = new System.Windows.Forms.Padding(3);
            this._listPanel.Size = new System.Drawing.Size(254, 409);
            this._listPanel.TabIndex = 2;
            // 
            // ListEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._splitContainer);
            this.Name = "ListEditorUserControl";
            this.Size = new System.Drawing.Size(708, 458);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._listGroupBox.ResumeLayout(false);
            this._buttonsPanel.ResumeLayout(false);
            this._editorGroupBox.ResumeLayout(false);
            this._listPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.Button _addModuleButton;
        private System.Windows.Forms.GroupBox _listGroupBox;
        private System.Windows.Forms.GroupBox _editorGroupBox;
        private System.Windows.Forms.Panel _editorPanel;
        private System.Windows.Forms.ColumnHeader _columnHeader;
        private System.Windows.Forms.Panel _buttonsPanel;
        private Common.Control.ListViewEx _listViewEx;
        private System.Windows.Forms.Panel _listPanel;
    }
}
