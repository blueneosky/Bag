namespace ImputationH31per.Vue.EditeurImputationTfs
{
    partial class EditeurImputationTfsForm
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
            this._numeroTextBox = new System.Windows.Forms.TextBox();
            this._numeroLabel = new System.Windows.Forms.Label();
            this._nomLabel = new System.Windows.Forms.Label();
            this._nomTextBox = new System.Windows.Forms.TextBox();
            this._estimationCouranteTextBox = new System.Windows.Forms.TextBox();
            this._courantLabel = new System.Windows.Forms.Label();
            this._dateLabel = new System.Windows.Forms.Label();
            this._consommeeCouranteTextBox = new System.Windows.Forms.TextBox();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._dateEstimationDateTimePicker = new System.Windows.Forms.DateTimePickerValidatingFixed();
            this._dateConsommeeDateTimePicker = new System.Windows.Forms.DateTimePickerValidatingFixed();
            this._avecEstimationCheckBox = new System.Windows.Forms.CheckBox();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._nomComplementaireTextBox = new System.Windows.Forms.TextBox();
            this._nomComplementaireLabel = new System.Windows.Forms.Label();
            this._numeroComplementaireLabel = new System.Windows.Forms.Label();
            this._nomGroupementLabel = new System.Windows.Forms.Label();
            this._nomGroupementComboBox = new System.Windows.Forms.ComboBox();
            this._numeroComplementaireComboBox = new System.Windows.Forms.ComboBox();
            this._estimationMaintenantButton = new System.Windows.Forms.Button();
            this._consommeeTextBox = new System.Windows.Forms.Label();
            this._copierDateButton = new System.Windows.Forms.Button();
            this._consommeeMaintenantButton = new System.Windows.Forms.Button();
            this._commentaireTextBox = new System.Windows.Forms.TextBox();
            this._commentaireLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _numeroTextBox
            // 
            this._numeroTextBox.Location = new System.Drawing.Point(12, 25);
            this._numeroTextBox.Name = "_numeroTextBox";
            this._numeroTextBox.Size = new System.Drawing.Size(112, 20);
            this._numeroTextBox.TabIndex = 0;
            this._numeroTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._numeroTextBox_Validating);
            // 
            // _numeroLabel
            // 
            this._numeroLabel.AutoSize = true;
            this._numeroLabel.Location = new System.Drawing.Point(9, 9);
            this._numeroLabel.Name = "_numeroLabel";
            this._numeroLabel.Size = new System.Drawing.Size(44, 13);
            this._numeroLabel.TabIndex = 16;
            this._numeroLabel.Text = "Numéro";
            // 
            // _nomLabel
            // 
            this._nomLabel.AutoSize = true;
            this._nomLabel.Location = new System.Drawing.Point(159, 9);
            this._nomLabel.Name = "_nomLabel";
            this._nomLabel.Size = new System.Drawing.Size(29, 13);
            this._nomLabel.TabIndex = 17;
            this._nomLabel.Text = "Nom";
            // 
            // _nomTextBox
            // 
            this._nomTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._nomTextBox.Location = new System.Drawing.Point(162, 25);
            this._nomTextBox.Name = "_nomTextBox";
            this._nomTextBox.Size = new System.Drawing.Size(458, 20);
            this._nomTextBox.TabIndex = 1;
            this._nomTextBox.TextChanged += new System.EventHandler(this._nomTextBox_TextChanged);
            this._nomTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._nomTextBox_Validating);
            // 
            // _estimationCouranteTextBox
            // 
            this._estimationCouranteTextBox.Location = new System.Drawing.Point(162, 200);
            this._estimationCouranteTextBox.Name = "_estimationCouranteTextBox";
            this._estimationCouranteTextBox.Size = new System.Drawing.Size(153, 20);
            this._estimationCouranteTextBox.TabIndex = 7;
            this._estimationCouranteTextBox.TextChanged += new System.EventHandler(this._estimationCouranteTextBox_TextChanged);
            this._estimationCouranteTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._estimationCouranteTextBox_Validating);
            // 
            // _courantLabel
            // 
            this._courantLabel.AutoSize = true;
            this._courantLabel.Location = new System.Drawing.Point(98, 201);
            this._courantLabel.Name = "_courantLabel";
            this._courantLabel.Size = new System.Drawing.Size(44, 13);
            this._courantLabel.TabIndex = 23;
            this._courantLabel.Text = "Courant";
            // 
            // _dateLabel
            // 
            this._dateLabel.AutoSize = true;
            this._dateLabel.Location = new System.Drawing.Point(98, 229);
            this._dateLabel.Name = "_dateLabel";
            this._dateLabel.Size = new System.Drawing.Size(30, 13);
            this._dateLabel.TabIndex = 24;
            this._dateLabel.Text = "Date";
            // 
            // _consommeeCouranteTextBox
            // 
            this._consommeeCouranteTextBox.Location = new System.Drawing.Point(354, 200);
            this._consommeeCouranteTextBox.Name = "_consommeeCouranteTextBox";
            this._consommeeCouranteTextBox.Size = new System.Drawing.Size(153, 20);
            this._consommeeCouranteTextBox.TabIndex = 8;
            this._consommeeCouranteTextBox.TextChanged += new System.EventHandler(this._consommeeCouranteTextBox_TextChanged);
            this._consommeeCouranteTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._consommeeCouranteTextBox_Validating);
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(545, 293);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 14;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(464, 293);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 15;
            this._cancelButton.Text = "Annulé";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _dateEstimationDateTimePicker
            // 
            this._dateEstimationDateTimePicker.Checked = false;
            this._dateEstimationDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            this._dateEstimationDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateEstimationDateTimePicker.Location = new System.Drawing.Point(162, 226);
            this._dateEstimationDateTimePicker.Name = "_dateEstimationDateTimePicker";
            this._dateEstimationDateTimePicker.Size = new System.Drawing.Size(153, 20);
            this._dateEstimationDateTimePicker.TabIndex = 9;
            this._dateEstimationDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this._dateEstimationDateTimePicker_Validating);
            // 
            // _dateConsommeeDateTimePicker
            // 
            this._dateConsommeeDateTimePicker.Checked = false;
            this._dateConsommeeDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            this._dateConsommeeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._dateConsommeeDateTimePicker.Location = new System.Drawing.Point(354, 226);
            this._dateConsommeeDateTimePicker.Name = "_dateConsommeeDateTimePicker";
            this._dateConsommeeDateTimePicker.Size = new System.Drawing.Size(153, 20);
            this._dateConsommeeDateTimePicker.TabIndex = 12;
            this._dateConsommeeDateTimePicker.Validating += new System.ComponentModel.CancelEventHandler(this._dateConsommeeDateTimePicker_Validating);
            // 
            // _avecEstimationCheckBox
            // 
            this._avecEstimationCheckBox.AutoSize = true;
            this._avecEstimationCheckBox.Location = new System.Drawing.Point(201, 176);
            this._avecEstimationCheckBox.Name = "_avecEstimationCheckBox";
            this._avecEstimationCheckBox.Size = new System.Drawing.Size(74, 17);
            this._avecEstimationCheckBox.TabIndex = 6;
            this._avecEstimationCheckBox.Text = "Estimation";
            this._avecEstimationCheckBox.UseVisualStyleBackColor = true;
            this._avecEstimationCheckBox.CheckedChanged += new System.EventHandler(this._avecEstimationCheckBox_CheckedChanged);
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // _nomComplementaireTextBox
            // 
            this._nomComplementaireTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._nomComplementaireTextBox.Location = new System.Drawing.Point(162, 64);
            this._nomComplementaireTextBox.Name = "_nomComplementaireTextBox";
            this._nomComplementaireTextBox.Size = new System.Drawing.Size(458, 20);
            this._nomComplementaireTextBox.TabIndex = 3;
            this._nomComplementaireTextBox.TextChanged += new System.EventHandler(this._nomComplementaireTextBox_TextChanged);
            this._nomComplementaireTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._nomComplementaireTextBox_Validating);
            // 
            // _nomComplementaireLabel
            // 
            this._nomComplementaireLabel.AutoSize = true;
            this._nomComplementaireLabel.Location = new System.Drawing.Point(159, 48);
            this._nomComplementaireLabel.Name = "_nomComplementaireLabel";
            this._nomComplementaireLabel.Size = new System.Drawing.Size(106, 13);
            this._nomComplementaireLabel.TabIndex = 19;
            this._nomComplementaireLabel.Text = "Nom complémentaire";
            // 
            // _numeroComplementaireLabel
            // 
            this._numeroComplementaireLabel.AutoSize = true;
            this._numeroComplementaireLabel.Location = new System.Drawing.Point(9, 48);
            this._numeroComplementaireLabel.Name = "_numeroComplementaireLabel";
            this._numeroComplementaireLabel.Size = new System.Drawing.Size(121, 13);
            this._numeroComplementaireLabel.TabIndex = 18;
            this._numeroComplementaireLabel.Text = "Numéro complémentaire";
            // 
            // _nomGroupementLabel
            // 
            this._nomGroupementLabel.AutoSize = true;
            this._nomGroupementLabel.Location = new System.Drawing.Point(159, 87);
            this._nomGroupementLabel.Name = "_nomGroupementLabel";
            this._nomGroupementLabel.Size = new System.Drawing.Size(88, 13);
            this._nomGroupementLabel.TabIndex = 20;
            this._nomGroupementLabel.Text = "Nom groupement";
            // 
            // _nomGroupementComboBox
            // 
            this._nomGroupementComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._nomGroupementComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._nomGroupementComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this._nomGroupementComboBox.Location = new System.Drawing.Point(162, 103);
            this._nomGroupementComboBox.Name = "_nomGroupementComboBox";
            this._nomGroupementComboBox.Size = new System.Drawing.Size(458, 21);
            this._nomGroupementComboBox.TabIndex = 4;
            this._nomGroupementComboBox.Validating += new System.ComponentModel.CancelEventHandler(this._groupementComboBox_Validating);
            // 
            // _numeroComplementaireComboBox
            // 
            this._numeroComplementaireComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this._numeroComplementaireComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this._numeroComplementaireComboBox.Location = new System.Drawing.Point(12, 64);
            this._numeroComplementaireComboBox.Name = "_numeroComplementaireComboBox";
            this._numeroComplementaireComboBox.Size = new System.Drawing.Size(112, 21);
            this._numeroComplementaireComboBox.TabIndex = 2;
            this._numeroComplementaireComboBox.TextChanged += new System.EventHandler(this._numeroComplementaireComboBox_TextChanged);
            this._numeroComplementaireComboBox.Validating += new System.ComponentModel.CancelEventHandler(this._numeroComplementaireComboBox_Validating);
            // 
            // _estimationMaintenantButton
            // 
            this._estimationMaintenantButton.Location = new System.Drawing.Point(240, 252);
            this._estimationMaintenantButton.Name = "_estimationMaintenantButton";
            this._estimationMaintenantButton.Size = new System.Drawing.Size(75, 23);
            this._estimationMaintenantButton.TabIndex = 10;
            this._estimationMaintenantButton.Text = "Maintenant";
            this._estimationMaintenantButton.UseVisualStyleBackColor = true;
            this._estimationMaintenantButton.Click += new System.EventHandler(this._estimationMaintenantButton_Click);
            // 
            // _consommeeTextBox
            // 
            this._consommeeTextBox.AutoSize = true;
            this._consommeeTextBox.Location = new System.Drawing.Point(398, 177);
            this._consommeeTextBox.Name = "_consommeeTextBox";
            this._consommeeTextBox.Size = new System.Drawing.Size(65, 13);
            this._consommeeTextBox.TabIndex = 22;
            this._consommeeTextBox.Text = "Consommée";
            // 
            // _copierDateButton
            // 
            this._copierDateButton.Location = new System.Drawing.Point(321, 224);
            this._copierDateButton.Name = "_copierDateButton";
            this._copierDateButton.Size = new System.Drawing.Size(27, 23);
            this._copierDateButton.TabIndex = 11;
            this._copierDateButton.Text = "=>";
            this._copierDateButton.UseVisualStyleBackColor = true;
            this._copierDateButton.Click += new System.EventHandler(this._copierDateButton_Click);
            // 
            // _consommeeMaintenantButton
            // 
            this._consommeeMaintenantButton.Location = new System.Drawing.Point(432, 252);
            this._consommeeMaintenantButton.Name = "_consommeeMaintenantButton";
            this._consommeeMaintenantButton.Size = new System.Drawing.Size(75, 23);
            this._consommeeMaintenantButton.TabIndex = 13;
            this._consommeeMaintenantButton.Text = "Maintenant";
            this._consommeeMaintenantButton.UseVisualStyleBackColor = true;
            this._consommeeMaintenantButton.Click += new System.EventHandler(this._consommeeMaintenantButton_Click);
            // 
            // _commentaireTextBox
            // 
            this._commentaireTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._commentaireTextBox.Location = new System.Drawing.Point(162, 143);
            this._commentaireTextBox.Name = "_commentaireTextBox";
            this._commentaireTextBox.Size = new System.Drawing.Size(458, 20);
            this._commentaireTextBox.TabIndex = 5;
            this._commentaireTextBox.TextChanged += new System.EventHandler(this._commentaireTextBox_TextChanged);
            this._commentaireTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._commentaireTextBox_Validating);
            // 
            // _commentaireLabel
            // 
            this._commentaireLabel.AutoSize = true;
            this._commentaireLabel.Location = new System.Drawing.Point(159, 127);
            this._commentaireLabel.Name = "_commentaireLabel";
            this._commentaireLabel.Size = new System.Drawing.Size(68, 13);
            this._commentaireLabel.TabIndex = 21;
            this._commentaireLabel.Text = "Commentaire";
            // 
            // EditeurImputationTfsForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(632, 328);
            this.Controls.Add(this._commentaireTextBox);
            this.Controls.Add(this._commentaireLabel);
            this.Controls.Add(this._consommeeMaintenantButton);
            this.Controls.Add(this._copierDateButton);
            this.Controls.Add(this._consommeeTextBox);
            this.Controls.Add(this._estimationMaintenantButton);
            this.Controls.Add(this._numeroComplementaireComboBox);
            this.Controls.Add(this._nomGroupementComboBox);
            this.Controls.Add(this._nomGroupementLabel);
            this.Controls.Add(this._nomComplementaireTextBox);
            this.Controls.Add(this._nomComplementaireLabel);
            this.Controls.Add(this._numeroComplementaireLabel);
            this.Controls.Add(this._avecEstimationCheckBox);
            this.Controls.Add(this._dateConsommeeDateTimePicker);
            this.Controls.Add(this._dateEstimationDateTimePicker);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._dateLabel);
            this.Controls.Add(this._courantLabel);
            this.Controls.Add(this._consommeeCouranteTextBox);
            this.Controls.Add(this._estimationCouranteTextBox);
            this.Controls.Add(this._nomTextBox);
            this.Controls.Add(this._nomLabel);
            this.Controls.Add(this._numeroLabel);
            this.Controls.Add(this._numeroTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditeurImputationTfsForm";
            this.Text = "Edition";
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _numeroTextBox;
        private System.Windows.Forms.Label _numeroLabel;
        private System.Windows.Forms.Label _nomLabel;
        private System.Windows.Forms.TextBox _nomTextBox;
        private System.Windows.Forms.TextBox _estimationCouranteTextBox;
        private System.Windows.Forms.Label _courantLabel;
        private System.Windows.Forms.Label _dateLabel;
        private System.Windows.Forms.TextBox _consommeeCouranteTextBox;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.DateTimePickerValidatingFixed _dateEstimationDateTimePicker;
        private System.Windows.Forms.DateTimePickerValidatingFixed _dateConsommeeDateTimePicker;
        private System.Windows.Forms.CheckBox _avecEstimationCheckBox;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.TextBox _nomComplementaireTextBox;
        private System.Windows.Forms.Label _nomComplementaireLabel;
        private System.Windows.Forms.Label _numeroComplementaireLabel;
        private System.Windows.Forms.Button _estimationMaintenantButton;
        private System.Windows.Forms.ComboBox _numeroComplementaireComboBox;
        private System.Windows.Forms.ComboBox _nomGroupementComboBox;
        private System.Windows.Forms.Label _nomGroupementLabel;
        private System.Windows.Forms.Label _consommeeTextBox;
        private System.Windows.Forms.Button _consommeeMaintenantButton;
        private System.Windows.Forms.Button _copierDateButton;
        private System.Windows.Forms.TextBox _commentaireTextBox;
        private System.Windows.Forms.Label _commentaireLabel;
    }
}