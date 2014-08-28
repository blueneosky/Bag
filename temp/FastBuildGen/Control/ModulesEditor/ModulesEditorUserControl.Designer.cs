namespace FastBuildGen.Control.ModulesEditor
{
    partial class ModulesEditorUserControl
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
            this._moduleEditorUserControl = new FastBuildGen.Control.ModuleEditor.ModuleEditorUserControl();
            this.SuspendLayout();
            // 
            // _listEditorUserControl
            // 
            this._listEditorUserControl.AddButtonText = "Add module";
            this._listEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listEditorUserControl.EditorGroupBoxText = "Module edition";
            this._listEditorUserControl.ListColumnName = "Name";
            this._listEditorUserControl.ListGroupBoxText = "Modules list";
            this._listEditorUserControl.Location = new System.Drawing.Point(0, 0);
            this._listEditorUserControl.Name = "_listEditorUserControl";
            this._listEditorUserControl.Size = new System.Drawing.Size(891, 641);
            this._listEditorUserControl.TabIndex = 0;
            // 
            // _moduleEditorUserControl
            // 
            this._moduleEditorUserControl.Location = new System.Drawing.Point(348, 76);
            this._moduleEditorUserControl.Name = "_moduleEditorUserControl";
            this._moduleEditorUserControl.Size = new System.Drawing.Size(440, 451);
            this._moduleEditorUserControl.TabIndex = 1;
            // 
            // ModulesEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._moduleEditorUserControl);
            this.Controls.Add(this._listEditorUserControl);
            this.Name = "ModulesEditorUserControl";
            this.Size = new System.Drawing.Size(891, 641);
            this.ResumeLayout(false);

        }

        #endregion

        private ListEditor.ListEditorUserControl _listEditorUserControl;
        private ModuleEditor.ModuleEditorUserControl _moduleEditorUserControl;

    }
}
