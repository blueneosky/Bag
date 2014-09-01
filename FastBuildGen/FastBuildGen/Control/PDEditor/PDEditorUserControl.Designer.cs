namespace FastBuildGen.Control.PDEditor
{
    partial class PDEditorUserControl
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


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._keywordLabel = new System.Windows.Forms.Label();
            this._keywordTextBox = new System.Windows.Forms.TextBox();
            this._helpTextTextBox = new System.Windows.Forms.TextBox();
            this._switchKeywordTextBox = new System.Windows.Forms.TextBox();
            this._paramVarNameTextBox = new System.Windows.Forms.TextBox();
            this._varNameTextBox = new System.Windows.Forms.TextBox();
            this._helpTextLabel = new System.Windows.Forms.Label();
            this._switchKeywordLabel = new System.Windows.Forms.Label();
            this._paramVarNameLabel = new System.Windows.Forms.Label();
            this._varNameLabel = new System.Windows.Forms.Label();
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _keywordLabel
            // 
            this._keywordLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._keywordLabel.AutoSize = true;
            this._keywordLabel.Location = new System.Drawing.Point(3, 7);
            this._keywordLabel.Name = "_keywordLabel";
            this._keywordLabel.Size = new System.Drawing.Size(48, 13);
            this._keywordLabel.TabIndex = 0;
            this._keywordLabel.Text = "Keyword";
            // 
            // _keywordTextBox
            // 
            this._keywordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._keywordTextBox.Location = new System.Drawing.Point(103, 3);
            this._keywordTextBox.Name = "_keywordTextBox";
            this._keywordTextBox.Size = new System.Drawing.Size(517, 20);
            this._keywordTextBox.TabIndex = 1;
            this._keywordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._keywordTextBox_Validating);
            // 
            // _helpTextTextBox
            // 
            this._helpTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._helpTextTextBox.Location = new System.Drawing.Point(103, 31);
            this._helpTextTextBox.Multiline = true;
            this._helpTextTextBox.Name = "_helpTextTextBox";
            this._helpTextTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._helpTextTextBox.Size = new System.Drawing.Size(517, 36);
            this._helpTextTextBox.TabIndex = 4;
            this._helpTextTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._helpTextTextBox_Validating);
            // 
            // _switchKeywordTextBox
            // 
            this._switchKeywordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._switchKeywordTextBox.Location = new System.Drawing.Point(103, 73);
            this._switchKeywordTextBox.Name = "_switchKeywordTextBox";
            this._switchKeywordTextBox.ReadOnly = true;
            this._switchKeywordTextBox.Size = new System.Drawing.Size(517, 20);
            this._switchKeywordTextBox.TabIndex = 5;
            // 
            // _paramVarNameTextBox
            // 
            this._paramVarNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._paramVarNameTextBox.Location = new System.Drawing.Point(103, 101);
            this._paramVarNameTextBox.Name = "_paramVarNameTextBox";
            this._paramVarNameTextBox.ReadOnly = true;
            this._paramVarNameTextBox.Size = new System.Drawing.Size(517, 20);
            this._paramVarNameTextBox.TabIndex = 6;
            // 
            // _varNameTextBox
            // 
            this._varNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._varNameTextBox.Location = new System.Drawing.Point(103, 129);
            this._varNameTextBox.Name = "_varNameTextBox";
            this._varNameTextBox.ReadOnly = true;
            this._varNameTextBox.Size = new System.Drawing.Size(517, 20);
            this._varNameTextBox.TabIndex = 7;
            // 
            // _helpTextLabel
            // 
            this._helpTextLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._helpTextLabel.AutoSize = true;
            this._helpTextLabel.Location = new System.Drawing.Point(3, 42);
            this._helpTextLabel.Name = "_helpTextLabel";
            this._helpTextLabel.Size = new System.Drawing.Size(49, 13);
            this._helpTextLabel.TabIndex = 8;
            this._helpTextLabel.Text = "Help text";
            // 
            // _switchKeywordLabel
            // 
            this._switchKeywordLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._switchKeywordLabel.AutoSize = true;
            this._switchKeywordLabel.Location = new System.Drawing.Point(3, 77);
            this._switchKeywordLabel.Name = "_switchKeywordLabel";
            this._switchKeywordLabel.Size = new System.Drawing.Size(82, 13);
            this._switchKeywordLabel.TabIndex = 9;
            this._switchKeywordLabel.Text = "Switch keyword";
            // 
            // _paramVarNameLabel
            // 
            this._paramVarNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._paramVarNameLabel.AutoSize = true;
            this._paramVarNameLabel.Location = new System.Drawing.Point(3, 105);
            this._paramVarNameLabel.Name = "_paramVarNameLabel";
            this._paramVarNameLabel.Size = new System.Drawing.Size(84, 13);
            this._paramVarNameLabel.TabIndex = 10;
            this._paramVarNameLabel.Text = "Param var name";
            // 
            // _varNameLabel
            // 
            this._varNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._varNameLabel.AutoSize = true;
            this._varNameLabel.Location = new System.Drawing.Point(3, 133);
            this._varNameLabel.Name = "_varNameLabel";
            this._varNameLabel.Size = new System.Drawing.Size(52, 13);
            this._varNameLabel.TabIndex = 11;
            this._varNameLabel.Text = "Var name";
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 2;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.Controls.Add(this._keywordTextBox, 1, 0);
            this._tableLayoutPanel.Controls.Add(this._varNameTextBox, 1, 4);
            this._tableLayoutPanel.Controls.Add(this._varNameLabel, 0, 4);
            this._tableLayoutPanel.Controls.Add(this._paramVarNameTextBox, 1, 3);
            this._tableLayoutPanel.Controls.Add(this._keywordLabel, 0, 0);
            this._tableLayoutPanel.Controls.Add(this._switchKeywordTextBox, 1, 2);
            this._tableLayoutPanel.Controls.Add(this._paramVarNameLabel, 0, 3);
            this._tableLayoutPanel.Controls.Add(this._switchKeywordLabel, 0, 2);
            this._tableLayoutPanel.Controls.Add(this._helpTextLabel, 0, 1);
            this._tableLayoutPanel.Controls.Add(this._helpTextTextBox, 1, 1);
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 6;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(623, 242);
            this._tableLayoutPanel.TabIndex = 12;
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // PDEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tableLayoutPanel);
            this.Name = "PDEditorUserControl";
            this.Size = new System.Drawing.Size(623, 242);
            this._tableLayoutPanel.ResumeLayout(false);
            this._tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _keywordLabel;
        private System.Windows.Forms.TextBox _keywordTextBox;
        private System.Windows.Forms.TextBox _helpTextTextBox;
        private System.Windows.Forms.TextBox _switchKeywordTextBox;
        private System.Windows.Forms.TextBox _paramVarNameTextBox;
        private System.Windows.Forms.TextBox _varNameTextBox;
        private System.Windows.Forms.Label _helpTextLabel;
        private System.Windows.Forms.Label _switchKeywordLabel;
        private System.Windows.Forms.Label _paramVarNameLabel;
        private System.Windows.Forms.Label _varNameLabel;
        private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        private System.Windows.Forms.ErrorProvider _errorProvider;
    }
}
