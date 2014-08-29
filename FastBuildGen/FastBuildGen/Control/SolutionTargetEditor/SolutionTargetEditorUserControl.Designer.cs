namespace FastBuildGen.Control.SolutionTargetEditor
{
    partial class SolutionTargetEditorUserControl
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
            this._flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._x86PlatformRadioButton = new System.Windows.Forms.RadioButton();
            this._win32PlatformRadioButton = new System.Windows.Forms.RadioButton();
            this._msBuildTargetTextBox = new System.Windows.Forms.TextBox();
            this._msBuildTargetLabel = new System.Windows.Forms.Label();
            this._platformLabel = new System.Windows.Forms.Label();
            this._pdEditorUserControl = new FastBuildGen.Control.PDEditor.PDEditorUserControl();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._tableLayoutPanel.SuspendLayout();
            this._flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 2;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.Controls.Add(this._flowLayoutPanel1, 1, 1);
            this._tableLayoutPanel.Controls.Add(this._msBuildTargetTextBox, 1, 0);
            this._tableLayoutPanel.Controls.Add(this._msBuildTargetLabel, 0, 0);
            this._tableLayoutPanel.Controls.Add(this._platformLabel, 0, 1);
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 186);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 3;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(679, 157);
            this._tableLayoutPanel.TabIndex = 6;
            // 
            // _flowLayoutPanel1
            // 
            this._flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._flowLayoutPanel1.Controls.Add(this._x86PlatformRadioButton);
            this._flowLayoutPanel1.Controls.Add(this._win32PlatformRadioButton);
            this._flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._flowLayoutPanel1.Location = new System.Drawing.Point(103, 31);
            this._flowLayoutPanel1.Name = "_flowLayoutPanel1";
            this._flowLayoutPanel1.Size = new System.Drawing.Size(573, 47);
            this._flowLayoutPanel1.TabIndex = 0;
            // 
            // _x86PlatformRadioButton
            // 
            this._x86PlatformRadioButton.AutoSize = true;
            this._x86PlatformRadioButton.Location = new System.Drawing.Point(3, 3);
            this._x86PlatformRadioButton.Name = "_x86PlatformRadioButton";
            this._x86PlatformRadioButton.Size = new System.Drawing.Size(42, 17);
            this._x86PlatformRadioButton.TabIndex = 4;
            this._x86PlatformRadioButton.TabStop = true;
            this._x86PlatformRadioButton.Text = "x86";
            this._x86PlatformRadioButton.UseVisualStyleBackColor = true;
            this._x86PlatformRadioButton.CheckedChanged += new System.EventHandler(this._x86PlatformRadioButton_CheckedChanged);
            // 
            // _win32PlatformRadioButton
            // 
            this._win32PlatformRadioButton.AutoSize = true;
            this._win32PlatformRadioButton.Location = new System.Drawing.Point(3, 26);
            this._win32PlatformRadioButton.Name = "_win32PlatformRadioButton";
            this._win32PlatformRadioButton.Size = new System.Drawing.Size(53, 17);
            this._win32PlatformRadioButton.TabIndex = 5;
            this._win32PlatformRadioButton.TabStop = true;
            this._win32PlatformRadioButton.Text = "win32";
            this._win32PlatformRadioButton.UseVisualStyleBackColor = true;
            this._win32PlatformRadioButton.CheckedChanged += new System.EventHandler(this.win32PlatformRadioButton_CheckedChanged);
            // 
            // _msBuildTargetTextBox
            // 
            this._msBuildTargetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._msBuildTargetTextBox.Location = new System.Drawing.Point(103, 3);
            this._msBuildTargetTextBox.Name = "_msBuildTargetTextBox";
            this._msBuildTargetTextBox.Size = new System.Drawing.Size(573, 20);
            this._msBuildTargetTextBox.TabIndex = 3;
            this._msBuildTargetTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._msBuildTargetTextBox_Validating);
            // 
            // _msBuildTargetLabel
            // 
            this._msBuildTargetLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._msBuildTargetLabel.AutoSize = true;
            this._msBuildTargetLabel.Location = new System.Drawing.Point(3, 7);
            this._msBuildTargetLabel.Name = "_msBuildTargetLabel";
            this._msBuildTargetLabel.Size = new System.Drawing.Size(77, 13);
            this._msBuildTargetLabel.TabIndex = 1;
            this._msBuildTargetLabel.Text = "MSBuildTarget";
            // 
            // _platformLabel
            // 
            this._platformLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._platformLabel.AutoSize = true;
            this._platformLabel.Location = new System.Drawing.Point(3, 48);
            this._platformLabel.Name = "_platformLabel";
            this._platformLabel.Size = new System.Drawing.Size(45, 13);
            this._platformLabel.TabIndex = 2;
            this._platformLabel.Text = "Platform";
            // 
            // _pdEditorUserControl
            // 
            this._pdEditorUserControl.Dock = System.Windows.Forms.DockStyle.Top;
            this._pdEditorUserControl.Location = new System.Drawing.Point(0, 0);
            this._pdEditorUserControl.Name = "_pdEditorUserControl";
            this._pdEditorUserControl.Size = new System.Drawing.Size(679, 186);
            this._pdEditorUserControl.TabIndex = 0;
            // 
            // _errorProvider
            // 
            this._errorProvider.ContainerControl = this;
            // 
            // ModuleEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._tableLayoutPanel);
            this.Controls.Add(this._pdEditorUserControl);
            this.Name = "ModuleEditorUserControl";
            this.Size = new System.Drawing.Size(679, 343);
            this._tableLayoutPanel.ResumeLayout(false);
            this._tableLayoutPanel.PerformLayout();
            this._flowLayoutPanel1.ResumeLayout(false);
            this._flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PDEditor.PDEditorUserControl _pdEditorUserControl;
        private System.Windows.Forms.Label _msBuildTargetLabel;
        private System.Windows.Forms.Label _platformLabel;
        private System.Windows.Forms.TextBox _msBuildTargetTextBox;
        private System.Windows.Forms.RadioButton _x86PlatformRadioButton;
        private System.Windows.Forms.RadioButton _win32PlatformRadioButton;
        private System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel _flowLayoutPanel1;
        private System.Windows.Forms.ErrorProvider _errorProvider;
    }
}
