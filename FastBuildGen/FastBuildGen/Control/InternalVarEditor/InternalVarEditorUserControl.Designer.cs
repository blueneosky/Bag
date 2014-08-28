namespace FastBuildGen.Control.InternalVarEditor
{
    partial class InternalVarEditorUserControl
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
            this.components = new System.ComponentModel.Container();
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._keywordTextBox = new System.Windows.Forms.TextBox();
            this._keywordLabel = new System.Windows.Forms.Label();
            this._valueLabel = new System.Windows.Forms.Label();
            this._valueTextBox = new System.Windows.Forms.TextBox();
            this._disclamerLabel = new System.Windows.Forms.Label();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 2;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.Controls.Add(this._keywordTextBox, 1, 1);
            this._tableLayoutPanel.Controls.Add(this._keywordLabel, 0, 1);
            this._tableLayoutPanel.Controls.Add(this._valueLabel, 0, 2);
            this._tableLayoutPanel.Controls.Add(this._valueTextBox, 1, 2);
            this._tableLayoutPanel.Controls.Add(this._disclamerLabel, 1, 0);
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 4;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(577, 250);
            this._tableLayoutPanel.TabIndex = 13;
            // 
            // _keywordTextBox
            // 
            this._keywordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._keywordTextBox.Location = new System.Drawing.Point(103, 31);
            this._keywordTextBox.Name = "_keywordTextBox";
            this._keywordTextBox.ReadOnly = true;
            this._keywordTextBox.Size = new System.Drawing.Size(471, 20);
            this._keywordTextBox.TabIndex = 1;
            // 
            // _keywordLabel
            // 
            this._keywordLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._keywordLabel.AutoSize = true;
            this._keywordLabel.Location = new System.Drawing.Point(3, 35);
            this._keywordLabel.Name = "_keywordLabel";
            this._keywordLabel.Size = new System.Drawing.Size(48, 13);
            this._keywordLabel.TabIndex = 0;
            this._keywordLabel.Text = "Keyword";
            // 
            // _valueLabel
            // 
            this._valueLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._valueLabel.AutoSize = true;
            this._valueLabel.Location = new System.Drawing.Point(3, 63);
            this._valueLabel.Name = "_valueLabel";
            this._valueLabel.Size = new System.Drawing.Size(34, 13);
            this._valueLabel.TabIndex = 2;
            this._valueLabel.Text = "Value";
            // 
            // _valueTextBox
            // 
            this._valueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._valueTextBox.Location = new System.Drawing.Point(103, 59);
            this._valueTextBox.Name = "_valueTextBox";
            this._valueTextBox.Size = new System.Drawing.Size(471, 20);
            this._valueTextBox.TabIndex = 3;
            this._valueTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._valueTextBox_Validating);
            // 
            // _disclamerLabel
            // 
            this._disclamerLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._disclamerLabel.AutoSize = true;
            this._disclamerLabel.Location = new System.Drawing.Point(103, 7);
            this._disclamerLabel.Name = "_disclamerLabel";
            this._disclamerLabel.Size = new System.Drawing.Size(178, 13);
            this._disclamerLabel.TabIndex = 4;
            this._disclamerLabel.Text = "Modify this variable at tour own risk !";
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // InternalVarEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tableLayoutPanel);
            this.Name = "InternalVarEditorUserControl";
            this.Size = new System.Drawing.Size(577, 250);
            this._tableLayoutPanel.ResumeLayout(false);
            this._tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        private System.Windows.Forms.TextBox _keywordTextBox;
        private System.Windows.Forms.Label _keywordLabel;
        private System.Windows.Forms.Label _valueLabel;
        private System.Windows.Forms.TextBox _valueTextBox;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.Label _disclamerLabel;

    }
}
