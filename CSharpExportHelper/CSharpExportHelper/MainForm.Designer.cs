namespace CSharpExportHelper
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this._backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._label = new System.Windows.Forms.Label();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _backgroundWorker
            // 
            this._backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this._backgroundWorker_DoWork);
            this._backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.Location = new System.Drawing.Point(15, 65);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(702, 37);
            this._progressBar.TabIndex = 0;
            // 
            // _label
            // 
            this._label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._label.Location = new System.Drawing.Point(12, 9);
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(705, 53);
            this._label.TabIndex = 1;
            this._label.Text = "Progression en cours...";
            // 
            // _timer
            // 
            this._timer.Interval = 200;
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 114);
            this.ControlBox = false;
            this.Controls.Add(this._label);
            this.Controls.Add(this._progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Export Helper";
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker _backgroundWorker;
        private System.Windows.Forms.ProgressBar _progressBar;
        private System.Windows.Forms.Label _label;
        private System.Windows.Forms.Timer _timer;
    }
}

