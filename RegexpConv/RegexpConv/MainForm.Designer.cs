namespace RegexpConv
{
    partial class MainForm
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
            this._addRegexpButton = new System.Windows.Forms.Button();
            this._formatsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._sourceTextBox = new System.Windows.Forms.TextBox();
            this._destTextBox = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _addRegexpButton
            // 
            this._addRegexpButton.Location = new System.Drawing.Point(402, 12);
            this._addRegexpButton.Name = "_addRegexpButton";
            this._addRegexpButton.Size = new System.Drawing.Size(75, 23);
            this._addRegexpButton.TabIndex = 0;
            this._addRegexpButton.Text = "Add Regexp";
            this._addRegexpButton.UseVisualStyleBackColor = true;
            this._addRegexpButton.Click += new System.EventHandler(this._addRegexButton_Click);
            // 
            // _formatsListView
            // 
            this._formatsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this._formatsListView.FullRowSelect = true;
            this._formatsListView.GridLines = true;
            this._formatsListView.Location = new System.Drawing.Point(12, 12);
            this._formatsListView.MultiSelect = false;
            this._formatsListView.Name = "_formatsListView";
            this._formatsListView.Size = new System.Drawing.Size(384, 97);
            this._formatsListView.TabIndex = 1;
            this._formatsListView.UseCompatibleStateImageBehavior = false;
            this._formatsListView.View = System.Windows.Forms.View.Details;
            this._formatsListView.SelectedIndexChanged += new System.EventHandler(this._formatsListView_SelectedIndexChanged);
            this._formatsListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this._formatsListView_KeyUp);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "1st";
            this.columnHeader1.Width = 78;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "End";
            this.columnHeader2.Width = 202;
            // 
            // _sourceTextBox
            // 
            this._sourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._sourceTextBox.Location = new System.Drawing.Point(0, 0);
            this._sourceTextBox.Multiline = true;
            this._sourceTextBox.Name = "_sourceTextBox";
            this._sourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._sourceTextBox.Size = new System.Drawing.Size(499, 442);
            this._sourceTextBox.TabIndex = 2;
            this._sourceTextBox.TextChanged += new System.EventHandler(this._sourceTextBox_TextChanged);
            // 
            // _destTextBox
            // 
            this._destTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._destTextBox.Location = new System.Drawing.Point(0, 0);
            this._destTextBox.Multiline = true;
            this._destTextBox.Name = "_destTextBox";
            this._destTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._destTextBox.Size = new System.Drawing.Size(568, 442);
            this._destTextBox.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 115);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._sourceTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._destTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(1071, 442);
            this.splitContainer1.SplitterDistance = 499;
            this.splitContainer1.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 569);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this._formatsListView);
            this.Controls.Add(this._addRegexpButton);
            this.Name = "MainForm";
            this.Text = "RegexpConv";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _addRegexpButton;
        private System.Windows.Forms.ListView _formatsListView;
        private System.Windows.Forms.TextBox _sourceTextBox;
        private System.Windows.Forms.TextBox _destTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}

