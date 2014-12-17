namespace RegexpConv
{
    partial class NewFormatForm
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
            this.components = new System.ComponentModel.Container();
            this._exp1Label = new System.Windows.Forms.Label();
            this._exp2Label = new System.Windows.Forms.Label();
            this._previewButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._addButton = new System.Windows.Forms.Button();
            this._exp2TextBox = new System.Windows.Forms.TextBox();
            this._exp1TextBox = new System.Windows.Forms.TextBox();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _exp1Label
            // 
            this._exp1Label.AutoSize = true;
            this._exp1Label.Location = new System.Drawing.Point(12, 9);
            this._exp1Label.Name = "_exp1Label";
            this._exp1Label.Size = new System.Drawing.Size(68, 13);
            this._exp1Label.TabIndex = 0;
            this._exp1Label.Text = "1st char expr";
            // 
            // _exp2Label
            // 
            this._exp2Label.AutoSize = true;
            this._exp2Label.Location = new System.Drawing.Point(135, 9);
            this._exp2Label.Name = "_exp2Label";
            this._exp2Label.Size = new System.Drawing.Size(49, 13);
            this._exp2Label.TabIndex = 2;
            this._exp2Label.Text = "End expr";
            // 
            // _previewButton
            // 
            this._previewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._previewButton.Location = new System.Drawing.Point(424, 23);
            this._previewButton.Name = "_previewButton";
            this._previewButton.Size = new System.Drawing.Size(75, 23);
            this._previewButton.TabIndex = 4;
            this._previewButton.Text = "Preview";
            this._previewButton.UseVisualStyleBackColor = true;
            this._previewButton.Click += new System.EventHandler(this._previewButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(424, 85);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 6;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _addButton
            // 
            this._addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._addButton.Location = new System.Drawing.Point(343, 85);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(75, 23);
            this._addButton.TabIndex = 5;
            this._addButton.Text = "Add";
            this._addButton.UseVisualStyleBackColor = true;
            this._addButton.Click += new System.EventHandler(this._addButton_Click);
            // 
            // _exp2TextBox
            // 
            this._exp2TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._exp2TextBox.Location = new System.Drawing.Point(138, 25);
            this._exp2TextBox.Name = "_exp2TextBox";
            this._exp2TextBox.Size = new System.Drawing.Size(280, 20);
            this._exp2TextBox.TabIndex = 3;
            // 
            // _exp1TextBox
            // 
            this._exp1TextBox.Location = new System.Drawing.Point(15, 25);
            this._exp1TextBox.Name = "_exp1TextBox";
            this._exp1TextBox.Size = new System.Drawing.Size(117, 20);
            this._exp1TextBox.TabIndex = 1;
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // NewFormatForm
            // 
            this.AcceptButton = this._addButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(511, 120);
            this.Controls.Add(this._exp1TextBox);
            this.Controls.Add(this._exp2TextBox);
            this.Controls.Add(this._addButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._previewButton);
            this.Controls.Add(this._exp2Label);
            this.Controls.Add(this._exp1Label);
            this.Name = "NewFormatForm";
            this.Text = "New format";
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _exp1Label;
        private System.Windows.Forms.Label _exp2Label;
        private System.Windows.Forms.Button _previewButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _addButton;
        private System.Windows.Forms.TextBox _exp2TextBox;
        private System.Windows.Forms.TextBox _exp1TextBox;
        private System.Windows.Forms.ErrorProvider _errorProvider;
    }
}