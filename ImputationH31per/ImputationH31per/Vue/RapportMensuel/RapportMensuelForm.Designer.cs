namespace ImputationH31per.Vue.RapportMensuel
{
    partial class RapportMensuelForm
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
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._groupesGroupBox = new System.Windows.Forms.GroupBox();
            this._groupesListBox = new System.Windows.Forms.ListBox();
            this._dateGroupBox = new System.Windows.Forms.GroupBox();
            this._moisAnneeDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this._regroupementGroupBox = new System.Windows.Forms.GroupBox();
            this._regroupementListBox = new System.Windows.Forms.ListBox();
            this._regroupementPanel = new System.Windows.Forms.Panel();
            this._totalHeureRegroupementLabel = new System.Windows.Forms.Label();
            this._nomGroupementTextBox = new System.Windows.Forms.TextBox();
            this._ajouterGroupementButton = new System.Windows.Forms.Button();
            this._regroupementsGroupBox = new System.Windows.Forms.GroupBox();
            this._regroupementsListBox = new System.Windows.Forms.ListBox();
            this._tachesGroupBox = new System.Windows.Forms.GroupBox();
            this._tachesListBox = new System.Windows.Forms.ListBox();
            this._ticketsGroupBox = new System.Windows.Forms.GroupBox();
            this._ticketsListBox = new System.Windows.Forms.ListBox();
            this._resultatGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this._textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._tableLayoutPanel.SuspendLayout();
            this._groupesGroupBox.SuspendLayout();
            this._dateGroupBox.SuspendLayout();
            this._regroupementGroupBox.SuspendLayout();
            this._regroupementPanel.SuspendLayout();
            this._regroupementsGroupBox.SuspendLayout();
            this._tachesGroupBox.SuspendLayout();
            this._ticketsGroupBox.SuspendLayout();
            this._resultatGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(4, 4);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._tableLayoutPanel);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._resultatGroupBox);
            this._splitContainer.Size = new System.Drawing.Size(1209, 565);
            this._splitContainer.SplitterDistance = 811;
            this._splitContainer.TabIndex = 0;
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 3;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 238F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this._tableLayoutPanel.Controls.Add(this._groupesGroupBox, 0, 1);
            this._tableLayoutPanel.Controls.Add(this._dateGroupBox, 0, 0);
            this._tableLayoutPanel.Controls.Add(this._regroupementGroupBox, 0, 2);
            this._tableLayoutPanel.Controls.Add(this._regroupementsGroupBox, 1, 2);
            this._tableLayoutPanel.Controls.Add(this._tachesGroupBox, 1, 0);
            this._tableLayoutPanel.Controls.Add(this._ticketsGroupBox, 2, 0);
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 3;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.09785F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.90215F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(811, 565);
            this._tableLayoutPanel.TabIndex = 0;
            // 
            // _groupesGroupBox
            // 
            this._groupesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._groupesGroupBox.Controls.Add(this._groupesListBox);
            this._groupesGroupBox.Location = new System.Drawing.Point(3, 57);
            this._groupesGroupBox.Name = "_groupesGroupBox";
            this._groupesGroupBox.Size = new System.Drawing.Size(232, 250);
            this._groupesGroupBox.TabIndex = 2;
            this._groupesGroupBox.TabStop = false;
            this._groupesGroupBox.Text = "Groupes";
            // 
            // _groupesListBox
            // 
            this._groupesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupesListBox.FormattingEnabled = true;
            this._groupesListBox.Location = new System.Drawing.Point(3, 16);
            this._groupesListBox.Name = "_groupesListBox";
            this._groupesListBox.Size = new System.Drawing.Size(226, 231);
            this._groupesListBox.TabIndex = 0;
            this._groupesListBox.SelectedIndexChanged += new System.EventHandler(this._groupesListBox_SelectedIndexChanged);
            this._groupesListBox.DoubleClick += new System.EventHandler(this._groupesListBox_DoubleClick);
            // 
            // _dateGroupBox
            // 
            this._dateGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._dateGroupBox.Controls.Add(this._moisAnneeDateTimePicker);
            this._dateGroupBox.Location = new System.Drawing.Point(3, 3);
            this._dateGroupBox.Name = "_dateGroupBox";
            this._dateGroupBox.Size = new System.Drawing.Size(232, 48);
            this._dateGroupBox.TabIndex = 4;
            this._dateGroupBox.TabStop = false;
            this._dateGroupBox.Text = "Date";
            // 
            // _moisAnneeDateTimePicker
            // 
            this._moisAnneeDateTimePicker.CustomFormat = "MMMM yyyy";
            this._moisAnneeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this._moisAnneeDateTimePicker.Location = new System.Drawing.Point(6, 19);
            this._moisAnneeDateTimePicker.Name = "_moisAnneeDateTimePicker";
            this._moisAnneeDateTimePicker.Size = new System.Drawing.Size(152, 20);
            this._moisAnneeDateTimePicker.TabIndex = 0;
            this._moisAnneeDateTimePicker.ValueChanged += new System.EventHandler(this._moisAnneeDateTimePicker_ValueChanged);
            // 
            // _regroupementGroupBox
            // 
            this._regroupementGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._regroupementGroupBox.Controls.Add(this._regroupementListBox);
            this._regroupementGroupBox.Controls.Add(this._regroupementPanel);
            this._regroupementGroupBox.Location = new System.Drawing.Point(3, 313);
            this._regroupementGroupBox.Name = "_regroupementGroupBox";
            this._regroupementGroupBox.Size = new System.Drawing.Size(232, 249);
            this._regroupementGroupBox.TabIndex = 5;
            this._regroupementGroupBox.TabStop = false;
            this._regroupementGroupBox.Text = "Regroupement";
            // 
            // _regroupementListBox
            // 
            this._regroupementListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._regroupementListBox.FormattingEnabled = true;
            this._regroupementListBox.Location = new System.Drawing.Point(3, 16);
            this._regroupementListBox.Name = "_regroupementListBox";
            this._regroupementListBox.Size = new System.Drawing.Size(226, 186);
            this._regroupementListBox.TabIndex = 2;
            this._regroupementListBox.SelectedIndexChanged += new System.EventHandler(this._regroupementListBox_SelectedIndexChanged);
            this._regroupementListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this._regroupementListBox_KeyUp);
            // 
            // _regroupementPanel
            // 
            this._regroupementPanel.Controls.Add(this._totalHeureRegroupementLabel);
            this._regroupementPanel.Controls.Add(this._nomGroupementTextBox);
            this._regroupementPanel.Controls.Add(this._ajouterGroupementButton);
            this._regroupementPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._regroupementPanel.Location = new System.Drawing.Point(3, 202);
            this._regroupementPanel.Name = "_regroupementPanel";
            this._regroupementPanel.Size = new System.Drawing.Size(226, 44);
            this._regroupementPanel.TabIndex = 3;
            // 
            // _totalHeureRegroupementLabel
            // 
            this._totalHeureRegroupementLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._totalHeureRegroupementLabel.Location = new System.Drawing.Point(3, 4);
            this._totalHeureRegroupementLabel.Name = "_totalHeureRegroupementLabel";
            this._totalHeureRegroupementLabel.Size = new System.Drawing.Size(171, 13);
            this._totalHeureRegroupementLabel.TabIndex = 2;
            this._totalHeureRegroupementLabel.Text = "Total heures : --";
            this._totalHeureRegroupementLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _nomGroupementTextBox
            // 
            this._nomGroupementTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._nomGroupementTextBox.Location = new System.Drawing.Point(3, 20);
            this._nomGroupementTextBox.Name = "_nomGroupementTextBox";
            this._nomGroupementTextBox.Size = new System.Drawing.Size(171, 20);
            this._nomGroupementTextBox.TabIndex = 1;
            this._nomGroupementTextBox.Validated += new System.EventHandler(this._nomGroupementTextBox_Validated);
            // 
            // _ajouterGroupementButton
            // 
            this._ajouterGroupementButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ajouterGroupementButton.Location = new System.Drawing.Point(180, 18);
            this._ajouterGroupementButton.Name = "_ajouterGroupementButton";
            this._ajouterGroupementButton.Size = new System.Drawing.Size(43, 23);
            this._ajouterGroupementButton.TabIndex = 0;
            this._ajouterGroupementButton.Text = "Add";
            this._ajouterGroupementButton.UseVisualStyleBackColor = true;
            this._ajouterGroupementButton.Click += new System.EventHandler(this._ajouterGroupementButton_Click);
            // 
            // _regroupementsGroupBox
            // 
            this._regroupementsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tableLayoutPanel.SetColumnSpan(this._regroupementsGroupBox, 2);
            this._regroupementsGroupBox.Controls.Add(this._regroupementsListBox);
            this._regroupementsGroupBox.Location = new System.Drawing.Point(241, 313);
            this._regroupementsGroupBox.Name = "_regroupementsGroupBox";
            this._regroupementsGroupBox.Size = new System.Drawing.Size(567, 249);
            this._regroupementsGroupBox.TabIndex = 6;
            this._regroupementsGroupBox.TabStop = false;
            this._regroupementsGroupBox.Text = "Regroupements";
            // 
            // _regroupementsListBox
            // 
            this._regroupementsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._regroupementsListBox.FormattingEnabled = true;
            this._regroupementsListBox.Location = new System.Drawing.Point(3, 16);
            this._regroupementsListBox.Name = "_regroupementsListBox";
            this._regroupementsListBox.Size = new System.Drawing.Size(561, 230);
            this._regroupementsListBox.TabIndex = 3;
            this._regroupementsListBox.SelectedIndexChanged += new System.EventHandler(this._regroupementsListBox_SelectedIndexChanged);
            this._regroupementsListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this._regroupementsListBox_KeyUp);
            // 
            // _tachesGroupBox
            // 
            this._tachesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._tachesGroupBox.Controls.Add(this._tachesListBox);
            this._tachesGroupBox.Location = new System.Drawing.Point(241, 3);
            this._tachesGroupBox.Name = "_tachesGroupBox";
            this._tableLayoutPanel.SetRowSpan(this._tachesGroupBox, 2);
            this._tachesGroupBox.Size = new System.Drawing.Size(223, 304);
            this._tachesGroupBox.TabIndex = 1;
            this._tachesGroupBox.TabStop = false;
            this._tachesGroupBox.Text = "Tâches";
            // 
            // _tachesListBox
            // 
            this._tachesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tachesListBox.FormattingEnabled = true;
            this._tachesListBox.Location = new System.Drawing.Point(3, 16);
            this._tachesListBox.Name = "_tachesListBox";
            this._tachesListBox.Size = new System.Drawing.Size(217, 285);
            this._tachesListBox.TabIndex = 2;
            this._tachesListBox.SelectedIndexChanged += new System.EventHandler(this._tachesListBox_SelectedIndexChanged);
            this._tachesListBox.DoubleClick += new System.EventHandler(this._tachesListBox_DoubleClick);
            // 
            // _ticketsGroupBox
            // 
            this._ticketsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._ticketsGroupBox.Controls.Add(this._ticketsListBox);
            this._ticketsGroupBox.Location = new System.Drawing.Point(470, 3);
            this._ticketsGroupBox.Name = "_ticketsGroupBox";
            this._tableLayoutPanel.SetRowSpan(this._ticketsGroupBox, 2);
            this._ticketsGroupBox.Size = new System.Drawing.Size(338, 304);
            this._ticketsGroupBox.TabIndex = 3;
            this._ticketsGroupBox.TabStop = false;
            this._ticketsGroupBox.Text = "Tickets";
            // 
            // _ticketsListBox
            // 
            this._ticketsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ticketsListBox.FormattingEnabled = true;
            this._ticketsListBox.Location = new System.Drawing.Point(3, 16);
            this._ticketsListBox.Name = "_ticketsListBox";
            this._ticketsListBox.Size = new System.Drawing.Size(332, 285);
            this._ticketsListBox.TabIndex = 1;
            this._ticketsListBox.SelectedIndexChanged += new System.EventHandler(this._ticketsListBox_SelectedIndexChanged);
            this._ticketsListBox.DoubleClick += new System.EventHandler(this._ticketsListBox_DoubleClick);
            // 
            // _resultatGroupBox
            // 
            this._resultatGroupBox.Controls.Add(this.button1);
            this._resultatGroupBox.Controls.Add(this._textBox);
            this._resultatGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultatGroupBox.Location = new System.Drawing.Point(0, 0);
            this._resultatGroupBox.Name = "_resultatGroupBox";
            this._resultatGroupBox.Size = new System.Drawing.Size(394, 565);
            this._resultatGroupBox.TabIndex = 0;
            this._resultatGroupBox.TabStop = false;
            this._resultatGroupBox.Text = "Résultat";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(316, 536);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // _textBox
            // 
            this._textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._textBox.Location = new System.Drawing.Point(6, 19);
            this._textBox.Multiline = true;
            this._textBox.Name = "_textBox";
            this._textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this._textBox.Size = new System.Drawing.Size(376, 511);
            this._textBox.TabIndex = 0;
            this._textBox.WordWrap = false;
            // 
            // RapportMensuelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 573);
            this.Controls.Add(this._splitContainer);
            this.Name = "RapportMensuelForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "Rapport Mensuel";
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._tableLayoutPanel.ResumeLayout(false);
            this._groupesGroupBox.ResumeLayout(false);
            this._dateGroupBox.ResumeLayout(false);
            this._regroupementGroupBox.ResumeLayout(false);
            this._regroupementPanel.ResumeLayout(false);
            this._regroupementPanel.PerformLayout();
            this._regroupementsGroupBox.ResumeLayout(false);
            this._tachesGroupBox.ResumeLayout(false);
            this._ticketsGroupBox.ResumeLayout(false);
            this._resultatGroupBox.ResumeLayout(false);
            this._resultatGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        private System.Windows.Forms.GroupBox _ticketsGroupBox;
        private System.Windows.Forms.GroupBox _tachesGroupBox;
        private System.Windows.Forms.GroupBox _resultatGroupBox;
        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox _groupesGroupBox;
        private System.Windows.Forms.GroupBox _dateGroupBox;
        private System.Windows.Forms.GroupBox _regroupementGroupBox;
        private System.Windows.Forms.ListBox _tachesListBox;
        private System.Windows.Forms.ListBox _ticketsListBox;
        private System.Windows.Forms.ListBox _groupesListBox;
        private System.Windows.Forms.DateTimePicker _moisAnneeDateTimePicker;
        private System.Windows.Forms.ListBox _regroupementListBox;
        private System.Windows.Forms.GroupBox _regroupementsGroupBox;
        private System.Windows.Forms.ListBox _regroupementsListBox;
        private System.Windows.Forms.Panel _regroupementPanel;
        private System.Windows.Forms.TextBox _nomGroupementTextBox;
        private System.Windows.Forms.Button _ajouterGroupementButton;
        private System.Windows.Forms.Label _totalHeureRegroupementLabel;
    }
}