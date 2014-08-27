namespace ImputationH31per.Controle.ImputationTfsListView
{
    partial class ImputationTfsListViewControl
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
            this._imputationTfsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // _imputationTfsListView
            // 
            this._imputationTfsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader7,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this._imputationTfsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._imputationTfsListView.Location = new System.Drawing.Point(0, 0);
            this._imputationTfsListView.MultiSelect = false;
            this._imputationTfsListView.Name = "_imputationTfsListView";
            this._imputationTfsListView.Size = new System.Drawing.Size(725, 291);
            this._imputationTfsListView.TabIndex = 2;
            this._imputationTfsListView.UseCompatibleStateImageBehavior = false;
            this._imputationTfsListView.View = System.Windows.Forms.View.Details;
            this._imputationTfsListView.DoubleClick += new System.EventHandler(this._imputationTfsListView_DoubleClick);
            this._imputationTfsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this._imputationTfsListView_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Numéro";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Nom";
            this.columnHeader2.Width = 258;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Estim.";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date Estim.";
            this.columnHeader4.Width = 124;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Conso.";
            this.columnHeader5.Width = 45;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Date conso.";
            this.columnHeader6.Width = 112;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Groupement";
            // 
            // ImputationTfsListViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._imputationTfsListView);
            this.Name = "ImputationTfsListViewControl";
            this.Size = new System.Drawing.Size(725, 291);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _imputationTfsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
    }
}
