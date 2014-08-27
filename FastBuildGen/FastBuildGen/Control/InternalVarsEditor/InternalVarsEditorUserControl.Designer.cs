namespace FastBuildGen.Control.InternalVarsEditor
{
    partial class InternalVarsEditorUserControl
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
            this._listEditorUserControl = new FastBuildGen.Control.ListEditor.ListEditorUserControl();
            this._internalVarEditorUserControl = new FastBuildGen.Control.InternalVarEditor.InternalVarEditorUserControl();
            this.SuspendLayout();
            // 
            // _listEditorUserControl
            // 
            this._listEditorUserControl.AddButtonText = "Add property";
            this._listEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listEditorUserControl.EditorGroupBoxText = "Property edition";
            this._listEditorUserControl.ListColumnName = "Keyword";
            this._listEditorUserControl.ListGroupBoxText = "Properties list";
            this._listEditorUserControl.Location = new System.Drawing.Point(0, 0);
            this._listEditorUserControl.Name = "_listEditorUserControl";
            this._listEditorUserControl.Size = new System.Drawing.Size(830, 648);
            this._listEditorUserControl.TabIndex = 1;
            // 
            // _internalVarEditorUserControl
            // 
            this._internalVarEditorUserControl.Location = new System.Drawing.Point(316, 72);
            this._internalVarEditorUserControl.Name = "_internalVarEditorUserControl";
            this._internalVarEditorUserControl.Size = new System.Drawing.Size(430, 318);
            this._internalVarEditorUserControl.TabIndex = 2;
            // 
            // InternalVarsEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._internalVarEditorUserControl);
            this.Controls.Add(this._listEditorUserControl);
            this.Name = "InternalVarsEditorUserControl";
            this.Size = new System.Drawing.Size(830, 648);
            this.ResumeLayout(false);

        }

        #endregion

        private ListEditor.ListEditorUserControl _listEditorUserControl;
        private InternalVarEditor.InternalVarEditorUserControl _internalVarEditorUserControl;
    }
}
