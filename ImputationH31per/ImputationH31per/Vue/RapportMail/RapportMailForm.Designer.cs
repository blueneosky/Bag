namespace ImputationH31per.Vue.RapportMail
{
    partial class RapportMailForm
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
            this._copierButton = new System.Windows.Forms.Button();
            this._dateDebutLabel = new System.Windows.Forms.Label();
            this._rapportTextBox = new System.Windows.Forms.TextBox();
            this._copierEtFermerButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this._listBox = new System.Windows.Forms.ListBox();
            this._dateDebutDateTimePicker = new System.Windows.Forms.DateTimePickerValidatingFixed();
            this._dateFinDateTimePicker = new System.Windows.Forms.DateTimePickerValidatingFixed();
            this._dateFinLabel = new System.Windows.Forms.Label();
            this._rapportLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _copierButton
            // 
            this._copierButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._copierButton.Location = new System.Drawing.Point(663, 415);
            this._copierButton.Name = "_copierButton";
            this._copierButton.Size = new System.Drawing.Size(75, 23);
            this._copierButton.TabIndex = 0;
            this._copierButton.Text = "Copier";
            this._copierButton.UseVisualStyleBackColor = true;
            this._copierButton.Click += new System.EventHandler(this._copierButton_Click);
            // 
            // _dateDebutLabel
            // 
            this._dateDebutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._dateDebutLabel.AutoSize = true;
            this._dateDebutLabel.Location = new System.Drawing.Point(660, 9);
            this._dateDebutLabel.Name = "_dateDebutLabel";
            this._dateDebutLabel.Size = new System.Drawing.Size(76, 13);
            this._dateDebutLabel.TabIndex = 3;
            this._dateDebutLabel.Text = "Depuis (inclus)";
            // 
            // _rapportTextBox
            // 
            this._rapportTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._rapportTextBox.Location = new System.Drawing.Point(3, 163);
            this._rapportTextBox.Multiline = true;
            this._rapportTextBox.Name = "_rapportTextBox";
            this._rapportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._rapportTextBox.Size = new System.Drawing.Size(636, 260);
            this._rapportTextBox.TabIndex = 5;
            this._rapportTextBox.WordWrap = false;
            // 
            // _copierEtFermerButton
            // 
            this._copierEtFermerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._copierEtFermerButton.Location = new System.Drawing.Point(762, 415);
            this._copierEtFermerButton.Name = "_copierEtFermerButton";
            this._copierEtFermerButton.Size = new System.Drawing.Size(112, 23);
            this._copierEtFermerButton.TabIndex = 6;
            this._copierEtFermerButton.Text = "Copier et fermer";
            this._copierEtFermerButton.UseVisualStyleBackColor = true;
            this._copierEtFermerButton.Click += new System.EventHandler(this._copierEtFermerButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this._listBox);
            this.panel1.Controls.Add(this._rapportTextBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(642, 426);
            this.panel1.TabIndex = 7;
            // 
            // _listBox
            // 
            this._listBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._listBox.Location = new System.Drawing.Point(3, 4);
            this._listBox.Name = "_listBox";
            this._listBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this._listBox.Size = new System.Drawing.Size(636, 147);
            this._listBox.TabIndex = 4;
            // 
            // _dateDebutDateTimePicker
            // 
            this._dateDebutDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._dateDebutDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dateDebutDateTimePicker.Location = new System.Drawing.Point(663, 25);
            this._dateDebutDateTimePicker.Name = "_dateDebutDateTimePicker";
            this._dateDebutDateTimePicker.Size = new System.Drawing.Size(112, 20);
            this._dateDebutDateTimePicker.TabIndex = 8;
            this._dateDebutDateTimePicker.ValueChanged += new System.EventHandler(this._dateDebutDateTimePicker_ValueChanged);
            // 
            // _dateFinDateTimePicker
            // 
            this._dateFinDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._dateFinDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dateFinDateTimePicker.Location = new System.Drawing.Point(663, 81);
            this._dateFinDateTimePicker.Name = "_dateFinDateTimePicker";
            this._dateFinDateTimePicker.Size = new System.Drawing.Size(112, 20);
            this._dateFinDateTimePicker.TabIndex = 9;
            this._dateFinDateTimePicker.ValueChanged += new System.EventHandler(this._dateFinDateTimePicker_ValueChanged);
            // 
            // _dateFinLabel
            // 
            this._dateFinLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._dateFinLabel.AutoSize = true;
            this._dateFinLabel.Location = new System.Drawing.Point(660, 65);
            this._dateFinLabel.Name = "_dateFinLabel";
            this._dateFinLabel.Size = new System.Drawing.Size(50, 13);
            this._dateFinLabel.TabIndex = 10;
            this._dateFinLabel.Text = "A (inclus)";
            // 
            // _rapportLabel
            // 
            this._rapportLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._rapportLabel.AutoSize = true;
            this._rapportLabel.Location = new System.Drawing.Point(660, 178);
            this._rapportLabel.Name = "_rapportLabel";
            this._rapportLabel.Size = new System.Drawing.Size(35, 13);
            this._rapportLabel.TabIndex = 11;
            this._rapportLabel.Text = "label1";
            // 
            // RapportMailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 450);
            this.Controls.Add(this._rapportLabel);
            this.Controls.Add(this._dateFinLabel);
            this.Controls.Add(this._dateFinDateTimePicker);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._dateDebutLabel);
            this.Controls.Add(this._copierEtFermerButton);
            this.Controls.Add(this._dateDebutDateTimePicker);
            this.Controls.Add(this._copierButton);
            this.Name = "RapportMailForm";
            this.Text = "RapportMailForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _copierButton;
        private System.Windows.Forms.Label _dateDebutLabel;
        private System.Windows.Forms.TextBox _rapportTextBox;
        private System.Windows.Forms.Button _copierEtFermerButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePickerValidatingFixed _dateDebutDateTimePicker;
        private System.Windows.Forms.DateTimePickerValidatingFixed _dateFinDateTimePicker;
        private System.Windows.Forms.Label _dateFinLabel;
        private System.Windows.Forms.ListBox _listBox;
        private System.Windows.Forms.Label _rapportLabel;
    }
}