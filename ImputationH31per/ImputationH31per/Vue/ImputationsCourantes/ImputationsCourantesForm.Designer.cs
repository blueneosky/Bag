namespace ImputationH31per.Vue.ImputationsCourantes
{
    partial class ImputationsCourantesForm
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this._ajouterImputationTfsButton = new System.Windows.Forms.Button();
            this._imputationTfsListViewControl = new ImputationH31per.Controle.ImputationTfsListView.ImputationTfsListViewControl();
            this._enregistrerEtFermerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _ajouterImputationTfsButton
            // 
            this._ajouterImputationTfsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ajouterImputationTfsButton.Location = new System.Drawing.Point(841, 12);
            this._ajouterImputationTfsButton.Name = "_ajouterImputationTfsButton";
            this._ajouterImputationTfsButton.Size = new System.Drawing.Size(130, 23);
            this._ajouterImputationTfsButton.TabIndex = 1;
            this._ajouterImputationTfsButton.Text = "Ajouter Imputation";
            this._ajouterImputationTfsButton.UseVisualStyleBackColor = true;
            this._ajouterImputationTfsButton.Click += new System.EventHandler(this._ajouterImputationTfsButton_Click);
            // 
            // _imputationTfsListViewControl
            // 
            this._imputationTfsListViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._imputationTfsListViewControl.Location = new System.Drawing.Point(12, 12);
            this._imputationTfsListViewControl.Name = "_imputationTfsListViewControl";
            this._imputationTfsListViewControl.Size = new System.Drawing.Size(737, 313);
            this._imputationTfsListViewControl.TabIndex = 0;
            // 
            // _enregistrerEtFermerButton
            // 
            this._enregistrerEtFermerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._enregistrerEtFermerButton.Location = new System.Drawing.Point(841, 302);
            this._enregistrerEtFermerButton.Name = "_enregistrerEtFermerButton";
            this._enregistrerEtFermerButton.Size = new System.Drawing.Size(130, 23);
            this._enregistrerEtFermerButton.TabIndex = 2;
            this._enregistrerEtFermerButton.Text = "Enregistrer et Fermer";
            this._enregistrerEtFermerButton.UseVisualStyleBackColor = true;
            this._enregistrerEtFermerButton.Click += new System.EventHandler(this._enregistrerEtFermerButton_Click);
            // 
            // ImputationsCourantesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 337);
            this.Controls.Add(this._enregistrerEtFermerButton);
            this.Controls.Add(this._ajouterImputationTfsButton);
            this.Controls.Add(this._imputationTfsListViewControl);
            this.Name = "ImputationsCourantesForm";
            this.Text = "ImputationsCourantesForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _ajouterImputationTfsButton;
        private ImputationH31per.Controle.ImputationTfsListView.ImputationTfsListViewControl _imputationTfsListViewControl;
        private System.Windows.Forms.Button _enregistrerEtFermerButton;
    }
}

