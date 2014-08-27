namespace ImputationH31per.Vue.ImportDepuisExcel
{
    partial class ImportDepuisExcelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._extraireButton = new System.Windows.Forms.Button();
            this._texteImportTextBox = new System.Windows.Forms.TextBox();
            this._validerButton = new System.Windows.Forms.Button();
            this._imputationTfsListViewControl = new ImputationH31per.Controle.ImputationTfsListView.ImputationTfsListViewControl();
            this.SuspendLayout();
            // 
            // _extraireButton
            // 
            this._extraireButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._extraireButton.Location = new System.Drawing.Point(379, 98);
            this._extraireButton.Name = "_extraireButton";
            this._extraireButton.Size = new System.Drawing.Size(86, 23);
            this._extraireButton.TabIndex = 0;
            this._extraireButton.Text = "Extraire =>";
            this._extraireButton.UseVisualStyleBackColor = true;
            this._extraireButton.Click += new System.EventHandler(this._extraireButton_Click);
            // 
            // _texteImportTextBox
            // 
            this._texteImportTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._texteImportTextBox.Location = new System.Drawing.Point(12, 12);
            this._texteImportTextBox.Multiline = true;
            this._texteImportTextBox.Name = "_texteImportTextBox";
            this._texteImportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._texteImportTextBox.Size = new System.Drawing.Size(361, 417);
            this._texteImportTextBox.TabIndex = 1;
            this._texteImportTextBox.WordWrap = false;
            // 
            // _validerButton
            // 
            this._validerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._validerButton.Location = new System.Drawing.Point(379, 152);
            this._validerButton.Name = "_validerButton";
            this._validerButton.Size = new System.Drawing.Size(86, 23);
            this._validerButton.TabIndex = 3;
            this._validerButton.Text = "Valider";
            this._validerButton.UseVisualStyleBackColor = true;
            this._validerButton.Click += new System.EventHandler(this._validerButton_Click);
            // 
            // _imputationTfsListViewControl
            // 
            this._imputationTfsListViewControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._imputationTfsListViewControl.Location = new System.Drawing.Point(471, 12);
            this._imputationTfsListViewControl.Name = "_imputationTfsListViewControl";
            this._imputationTfsListViewControl.Size = new System.Drawing.Size(725, 417);
            this._imputationTfsListViewControl.TabIndex = 2;
            // 
            // ImportDepuisExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 441);
            this.Controls.Add(this._validerButton);
            this.Controls.Add(this._imputationTfsListViewControl);
            this.Controls.Add(this._texteImportTextBox);
            this.Controls.Add(this._extraireButton);
            this.Name = "ImportDepuisExcelForm";
            this.Text = "ImportDepuisExcelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _extraireButton;
        private System.Windows.Forms.TextBox _texteImportTextBox;
        private Controle.ImputationTfsListView.ImputationTfsListViewControl _imputationTfsListViewControl;
        private System.Windows.Forms.Button _validerButton;
    }
}