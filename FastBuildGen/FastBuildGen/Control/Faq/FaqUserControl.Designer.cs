namespace FastBuildGen.Control.Faq
{
    partial class FaqUserControl
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
            this._faqWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // _faqWebBrowser
            // 
            this._faqWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this._faqWebBrowser.Location = new System.Drawing.Point(0, 0);
            this._faqWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this._faqWebBrowser.Name = "_faqWebBrowser";
            this._faqWebBrowser.Size = new System.Drawing.Size(907, 569);
            this._faqWebBrowser.TabIndex = 0;
            // 
            // FaqUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._faqWebBrowser);
            this.Name = "FaqUserControl";
            this.Size = new System.Drawing.Size(907, 569);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser _faqWebBrowser;
    }
}
