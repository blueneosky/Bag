using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    public class InputBox : Form
    {
        private Button _cancelButton;
        private TextBox _inputTextBox;
        private Label _messageLabel;
        private Button _okButton;

        private InputBox()
        {
            InitializeComponent();
        }

        public static DialogResult ShowDialog(string title, string message, ref string value)
        {
            DialogResult result = DialogResult.Cancel;

            using (InputBox inputBox = new InputBox())
            {
                inputBox.Text = title;
                inputBox._messageLabel.Text = message;
                inputBox._inputTextBox.Text = value;

                result = inputBox.ShowDialog();

                value = inputBox._inputTextBox.Text;
            }

            return result;
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            // nothing - .Net black magic ;-)
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void InitializeComponent()
        {
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._messageLabel = new System.Windows.Forms.Label();
            this._inputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            //
            // _okButton
            //
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(280, 77);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 3;
            this._okButton.Text = "&OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            //
            // _cancelButton
            //
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(199, 77);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            //
            // _messageLabel
            //
            this._messageLabel.AutoSize = true;
            this._messageLabel.Location = new System.Drawing.Point(26, 21);
            this._messageLabel.Name = "_messageLabel";
            this._messageLabel.Size = new System.Drawing.Size(35, 13);
            this._messageLabel.TabIndex = 0;
            this._messageLabel.Text = "label1";
            //
            // _inputTextBox
            //
            this._inputTextBox.Location = new System.Drawing.Point(12, 46);
            this._inputTextBox.Name = "_inputTextBox";
            this._inputTextBox.Size = new System.Drawing.Size(343, 20);
            this._inputTextBox.TabIndex = 1;
            //
            // InputBox
            //
            this.AcceptButton = this._okButton;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(367, 112);
            this.Controls.Add(this._inputTextBox);
            this.Controls.Add(this._messageLabel);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputBox";
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}