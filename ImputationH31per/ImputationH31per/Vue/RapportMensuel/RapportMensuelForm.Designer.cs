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
            this._ticketsGroupBox = new System.Windows.Forms.GroupBox();
            this._groupementGroupBox = new System.Windows.Forms.GroupBox();
            this._tachesGroupBox = new System.Windows.Forms.GroupBox();
            this._resultatGroupBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this._textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._tableLayoutPanel.SuspendLayout();
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
            this._splitContainer.Size = new System.Drawing.Size(1155, 551);
            this._splitContainer.SplitterDistance = 487;
            this._splitContainer.TabIndex = 0;
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 1;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.Controls.Add(this._ticketsGroupBox, 0, 1);
            this._tableLayoutPanel.Controls.Add(this._groupementGroupBox, 0, 2);
            this._tableLayoutPanel.Controls.Add(this._tachesGroupBox, 0, 0);
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 3;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(487, 551);
            this._tableLayoutPanel.TabIndex = 0;
            // 
            // _ticketsGroupBox
            // 
            this._ticketsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ticketsGroupBox.Location = new System.Drawing.Point(3, 186);
            this._ticketsGroupBox.Name = "_ticketsGroupBox";
            this._ticketsGroupBox.Size = new System.Drawing.Size(481, 177);
            this._ticketsGroupBox.TabIndex = 3;
            this._ticketsGroupBox.TabStop = false;
            this._ticketsGroupBox.Text = "Tickets";
            // 
            // _groupementGroupBox
            // 
            this._groupementGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupementGroupBox.Location = new System.Drawing.Point(3, 369);
            this._groupementGroupBox.Name = "_groupementGroupBox";
            this._groupementGroupBox.Size = new System.Drawing.Size(481, 179);
            this._groupementGroupBox.TabIndex = 2;
            this._groupementGroupBox.TabStop = false;
            this._groupementGroupBox.Text = "Groupement";
            // 
            // _tachesGroupBox
            // 
            this._tachesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tachesGroupBox.Location = new System.Drawing.Point(3, 3);
            this._tachesGroupBox.Name = "_tachesGroupBox";
            this._tachesGroupBox.Size = new System.Drawing.Size(481, 177);
            this._tachesGroupBox.TabIndex = 1;
            this._tachesGroupBox.TabStop = false;
            this._tachesGroupBox.Text = "Tâches";
            // 
            // _resultatGroupBox
            // 
            this._resultatGroupBox.Controls.Add(this.button1);
            this._resultatGroupBox.Controls.Add(this._textBox);
            this._resultatGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._resultatGroupBox.Location = new System.Drawing.Point(0, 0);
            this._resultatGroupBox.Name = "_resultatGroupBox";
            this._resultatGroupBox.Size = new System.Drawing.Size(664, 551);
            this._resultatGroupBox.TabIndex = 0;
            this._resultatGroupBox.TabStop = false;
            this._resultatGroupBox.Text = "Résultat";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(379, 512);
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
            this._textBox.Size = new System.Drawing.Size(646, 478);
            this._textBox.TabIndex = 0;
            this._textBox.WordWrap = false;
            // 
            // RapportMensuelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 559);
            this.Controls.Add(this._splitContainer);
            this.Name = "RapportMensuelForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "Rapport Mensuel";
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            this._tableLayoutPanel.ResumeLayout(false);
            this._resultatGroupBox.ResumeLayout(false);
            this._resultatGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        private System.Windows.Forms.GroupBox _ticketsGroupBox;
        private System.Windows.Forms.GroupBox _groupementGroupBox;
        private System.Windows.Forms.GroupBox _tachesGroupBox;
        private System.Windows.Forms.GroupBox _resultatGroupBox;
        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.Button button1;
    }
}