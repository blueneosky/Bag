namespace Mahjong
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
            this._buttonStartOrNext = new System.Windows.Forms.Button();
            this._groupBoxTileFamily = new System.Windows.Forms.GroupBox();
            this._pictureBoxResult = new System.Windows.Forms.PictureBox();
            this._pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _buttonStartOrNext
            // 
            this._buttonStartOrNext.Location = new System.Drawing.Point(12, 12);
            this._buttonStartOrNext.Name = "_buttonStartOrNext";
            this._buttonStartOrNext.Size = new System.Drawing.Size(75, 23);
            this._buttonStartOrNext.TabIndex = 0;
            this._buttonStartOrNext.Text = "__";
            this._buttonStartOrNext.UseVisualStyleBackColor = true;
            this._buttonStartOrNext.Click += new System.EventHandler(this._buttonStartOrNext_Click);
            // 
            // _groupBoxTileFamily
            // 
            this._groupBoxTileFamily.Location = new System.Drawing.Point(12, 178);
            this._groupBoxTileFamily.Name = "_groupBoxTileFamily";
            this._groupBoxTileFamily.Size = new System.Drawing.Size(144, 149);
            this._groupBoxTileFamily.TabIndex = 2;
            this._groupBoxTileFamily.TabStop = false;
            // 
            // _pictureBoxResult
            // 
            this._pictureBoxResult.Location = new System.Drawing.Point(161, 119);
            this._pictureBoxResult.Name = "_pictureBoxResult";
            this._pictureBoxResult.Size = new System.Drawing.Size(53, 53);
            this._pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._pictureBoxResult.TabIndex = 3;
            this._pictureBoxResult.TabStop = false;
            // 
            // _pictureBox
            // 
            this._pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureBox.Location = new System.Drawing.Point(12, 41);
            this._pictureBox.Name = "_pictureBox";
            this._pictureBox.Size = new System.Drawing.Size(143, 131);
            this._pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._pictureBox.TabIndex = 1;
            this._pictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 460);
            this.Controls.Add(this._pictureBoxResult);
            this.Controls.Add(this._groupBoxTileFamily);
            this.Controls.Add(this._pictureBox);
            this.Controls.Add(this._buttonStartOrNext);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _buttonStartOrNext;
        private System.Windows.Forms.PictureBox _pictureBox;
        private System.Windows.Forms.GroupBox _groupBoxTileFamily;
        private System.Windows.Forms.PictureBox _pictureBoxResult;
    }
}

