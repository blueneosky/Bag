namespace HSphere
{
    partial class Form1
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this._cubeButton = new System.Windows.Forms.Button();
            this._viewDistanceTrackBar = new System.Windows.Forms.TrackBar();
            this._perspectiveCheckBox = new System.Windows.Forms.CheckBox();
            this._sphereButton = new System.Windows.Forms.Button();
            this._isDpiScalingCheckBox = new System.Windows.Forms.CheckBox();
            this._viewDistanceLabel = new System.Windows.Forms.Label();
            this._panelEngine = new HSphere.Engine.PanelEngine();
            ((System.ComponentModel.ISupportInitialize)(this._viewDistanceTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // _cubeButton
            // 
            this._cubeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._cubeButton.Location = new System.Drawing.Point(674, 70);
            this._cubeButton.Name = "_cubeButton";
            this._cubeButton.Size = new System.Drawing.Size(75, 23);
            this._cubeButton.TabIndex = 1;
            this._cubeButton.Text = "Cube";
            this._cubeButton.UseVisualStyleBackColor = true;
            this._cubeButton.Click += new System.EventHandler(this._cubeBbutton_Click);
            // 
            // _viewDistanceTrackBar
            // 
            this._viewDistanceTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._viewDistanceTrackBar.Location = new System.Drawing.Point(561, 12);
            this._viewDistanceTrackBar.Maximum = 500;
            this._viewDistanceTrackBar.Name = "_viewDistanceTrackBar";
            this._viewDistanceTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this._viewDistanceTrackBar.Size = new System.Drawing.Size(45, 309);
            this._viewDistanceTrackBar.TabIndex = 2;
            this._viewDistanceTrackBar.TickFrequency = 10;
            this._viewDistanceTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this._viewDistanceTrackBar.Value = 14;
            this._viewDistanceTrackBar.ValueChanged += new System.EventHandler(this._viewDistanceTrackBar_ValueChanged);
            // 
            // _perspectiveCheckBox
            // 
            this._perspectiveCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._perspectiveCheckBox.AutoSize = true;
            this._perspectiveCheckBox.Location = new System.Drawing.Point(674, 12);
            this._perspectiveCheckBox.Name = "_perspectiveCheckBox";
            this._perspectiveCheckBox.Size = new System.Drawing.Size(77, 17);
            this._perspectiveCheckBox.TabIndex = 3;
            this._perspectiveCheckBox.Text = "Perpective";
            this._perspectiveCheckBox.UseVisualStyleBackColor = true;
            this._perspectiveCheckBox.CheckedChanged += new System.EventHandler(this._perspectiveCheckBox_CheckedChanged);
            // 
            // _sphereButton
            // 
            this._sphereButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._sphereButton.Location = new System.Drawing.Point(674, 99);
            this._sphereButton.Name = "_sphereButton";
            this._sphereButton.Size = new System.Drawing.Size(75, 23);
            this._sphereButton.TabIndex = 4;
            this._sphereButton.Text = "Spere";
            this._sphereButton.UseVisualStyleBackColor = true;
            this._sphereButton.Click += new System.EventHandler(this._sphereButton_Click);
            // 
            // _isDpiScalingCheckBox
            // 
            this._isDpiScalingCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._isDpiScalingCheckBox.AutoSize = true;
            this._isDpiScalingCheckBox.Location = new System.Drawing.Point(658, 35);
            this._isDpiScalingCheckBox.Name = "_isDpiScalingCheckBox";
            this._isDpiScalingCheckBox.Size = new System.Drawing.Size(93, 17);
            this._isDpiScalingCheckBox.TabIndex = 5;
            this._isDpiScalingCheckBox.Text = "Is DPI Scaling";
            this._isDpiScalingCheckBox.UseVisualStyleBackColor = true;
            this._isDpiScalingCheckBox.CheckedChanged += new System.EventHandler(this._isDpiScalingCheckBox_CheckedChanged);
            // 
            // _viewDistanceLabel
            // 
            this._viewDistanceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._viewDistanceLabel.AutoSize = true;
            this._viewDistanceLabel.Location = new System.Drawing.Point(561, 324);
            this._viewDistanceLabel.Name = "_viewDistanceLabel";
            this._viewDistanceLabel.Size = new System.Drawing.Size(35, 13);
            this._viewDistanceLabel.TabIndex = 6;
            this._viewDistanceLabel.Text = "label1";
            // 
            // _panelEngine
            // 
            this._panelEngine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._panelEngine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._panelEngine.Location = new System.Drawing.Point(12, 12);
            this._panelEngine.Name = "_panelEngine";
            this._panelEngine.ViewPort = null;
            this._panelEngine.Size = new System.Drawing.Size(543, 325);
            this._panelEngine.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 349);
            this.Controls.Add(this._viewDistanceLabel);
            this.Controls.Add(this._isDpiScalingCheckBox);
            this.Controls.Add(this._sphereButton);
            this.Controls.Add(this._perspectiveCheckBox);
            this.Controls.Add(this._viewDistanceTrackBar);
            this.Controls.Add(this._cubeButton);
            this.Controls.Add(this._panelEngine);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this._viewDistanceTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Engine.PanelEngine _panelEngine;
        private System.Windows.Forms.Button _cubeButton;
        private System.Windows.Forms.TrackBar _viewDistanceTrackBar;
        private System.Windows.Forms.CheckBox _perspectiveCheckBox;
        private System.Windows.Forms.Button _sphereButton;
        private System.Windows.Forms.CheckBox _isDpiScalingCheckBox;
        private System.Windows.Forms.Label _viewDistanceLabel;

    }
}

