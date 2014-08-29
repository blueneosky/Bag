namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    partial class TargetsEditorUserControl
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
            this._targetEditorUserControl = new FastBuildGen.Control.MacroSolutionTargetEditor.TargetEditorUserControl();
            this.SuspendLayout();
            // 
            // _listEditorUserControl
            // 
            this._listEditorUserControl.AddButtonText = "Add target";
            this._listEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listEditorUserControl.EditorGroupBoxText = "Target edition";
            this._listEditorUserControl.ListColumnName = "Name";
            this._listEditorUserControl.ListGroupBoxText = "Targets list";
            this._listEditorUserControl.Location = new System.Drawing.Point(0, 0);
            this._listEditorUserControl.Name = "_listEditorUserControl";
            this._listEditorUserControl.Size = new System.Drawing.Size(847, 683);
            this._listEditorUserControl.TabIndex = 0;
            // 
            // _targetEditorUserControl
            // 
            this._targetEditorUserControl.Location = new System.Drawing.Point(352, 50);
            this._targetEditorUserControl.Name = "_targetEditorUserControl";
            this._targetEditorUserControl.Size = new System.Drawing.Size(363, 417);
            this._targetEditorUserControl.TabIndex = 1;
            // 
            // TargetsEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._targetEditorUserControl);
            this.Controls.Add(this._listEditorUserControl);
            this.Name = "TargetsEditorUserControl";
            this.Size = new System.Drawing.Size(847, 683);
            this.ResumeLayout(false);

        }

        #endregion

        private ListEditor.ListEditorUserControl _listEditorUserControl;
        private TargetEditor.TargetEditorUserControl _targetEditorUserControl;
    }
}
