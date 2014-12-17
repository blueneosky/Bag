using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegexpConv
{
    public partial class NewFormatForm : Form
    {
        private readonly MainForm _mainForm;

        private NewFormatForm()
        {
            InitializeComponent();
        }

        public string Exp1 { get { return _exp1TextBox.Text; } }
        public string Exp2 { get { return _exp2TextBox.Text; } }

        public NewFormatForm(MainForm mainForm)
            : this()
        {
            _mainForm = mainForm;
        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            if (false == Check())
                return;

            _mainForm.SelectedFormat = new Couple(Exp1, Exp2);
        }

        private void _addButton_Click(object sender, EventArgs e)
        {
            if (false == Check())
                return;

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private bool Check()
        {
            // reset
            _errorProvider.SetError(_exp1TextBox, null);
            _errorProvider.SetError(_exp2TextBox, null);

            string exp1 = Exp1;
            string exp2 = Exp2;
            bool exp1Succes = CheckExp(exp1);
            bool exp2Succes = CheckExp(exp2);

            if (false == exp1Succes)
                _errorProvider.SetError(_exp1TextBox, "Not Regexp expression");
            if (false == exp2Succes)
                _errorProvider.SetError(_exp2TextBox, "Not Regexp expression");
            if (false == (exp1Succes && exp2Succes))
                return false;

            string exp = MainForm.GetExpRegExp(exp1, exp2);
            bool expSucces = CheckExp(exp);
            if (false == exp1Succes)
            {
                _errorProvider.SetError(_exp1TextBox, "Combinaison not valid for a Regexp : " + exp);
                _errorProvider.SetError(_exp2TextBox, "Combinaison not valid for a Regexp : " + exp);
            }

            return expSucces;
        }

        private bool CheckExp(string exp)
        {
            try
            {
                new Regex(exp);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}