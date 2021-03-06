﻿namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    partial class MacroSolutionTargetsEditorUserControl
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
            this._targetEditorUserControl = new FastBuildGen.Control.MacroSolutionTargetEditor.MacroSolutionTargetEditorUserControl();
            this.SuspendLayout();
            // 
            // _listEditorUserControl
            // 
            this._listEditorUserControl.AddButtonText = "Add macro";
            this._listEditorUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listEditorUserControl.DoubleBufferedEx = false;
            this._listEditorUserControl.EditorGroupBoxText = "Macro edition";
            this._listEditorUserControl.ListColumnName = "Name";
            this._listEditorUserControl.ListGroupBoxText = "Macros list";
            this._listEditorUserControl.Location = new System.Drawing.Point(0, 0);
            this._listEditorUserControl.Name = "_listEditorUserControl";
            this._listEditorUserControl.Size = new System.Drawing.Size(847, 683);
            this._listEditorUserControl.TabIndex = 0;
            // 
            // _targetEditorUserControl
            // 
            this._targetEditorUserControl.DoubleBufferedEx = false;
            this._targetEditorUserControl.Location = new System.Drawing.Point(352, 50);
            this._targetEditorUserControl.Name = "_targetEditorUserControl";
            this._targetEditorUserControl.Size = new System.Drawing.Size(363, 417);
            this._targetEditorUserControl.TabIndex = 1;
            // 
            // MacroSolutionTargetsEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._targetEditorUserControl);
            this.Controls.Add(this._listEditorUserControl);
            this.Name = "MacroSolutionTargetsEditorUserControl";
            this.Size = new System.Drawing.Size(847, 683);
            this.ResumeLayout(false);

        }

        #endregion

        private ListEditor.ListEditorUserControl _listEditorUserControl;
        private MacroSolutionTargetEditor.MacroSolutionTargetEditorUserControl _targetEditorUserControl;
    }
}
