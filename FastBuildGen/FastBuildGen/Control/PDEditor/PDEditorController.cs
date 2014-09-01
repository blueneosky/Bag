using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

namespace FastBuildGen.Control.PDEditor
{
    internal class PDEditorController
    {
        private readonly PDEditorModel _model;

        public PDEditorController(PDEditorModel model)
        {
            _model = model;
        }

        public void SetHelpText(string value)
        {
            _model.Target.HelpText = value;
        }

        public void SetKeyword(string value, bool feedBackByException = false)
        {
            value = value.Trim();

            if (_model.Target.Keyword == value)
                return;

            bool success =
                CheckValueFormat(value, feedBackByException)
                && CheckKeywordAvailability(value, feedBackByException);
            if (!success)
                return;

            _model.Target.Keyword = value;
        }

        public void SetTargetDescription(FBTarget value)
        {
            _model.Target = value;
        }

        protected void ProcedFeedBack(string message, bool feedBackByException)
        {
            if (feedBackByException)
            {
                throw new FastBuildGenException(message);
            }
            else
            {
                MessageBox.Show(message);
                return;
            }
        }

        private bool CheckKeywordAvailability(string value, bool feedBackByException)
        {
            bool isKeywordUsed = _model.ApplicationModel.FBModel.IsKeywordUsed(value);
            if (isKeywordUsed)
            {
                ProcedFeedBack("This keyword is allready used by an other module or target.", feedBackByException);
                return false;
            }

            return true;
        }

        private bool CheckValueFormat(string value, bool feedBackByException)
        {
            const string ConstValueFormatPattern = "^[a-zA-Z][_0-9a-zA-Z]*$";

            if (String.IsNullOrEmpty(value))
            {
                ProcedFeedBack("Empty value not accepted.", feedBackByException);
                return false;
            }

            bool isMatch = Regex.IsMatch(value, ConstValueFormatPattern);
            if (false == isMatch)
            {
                ProcedFeedBack("Value must be like var name (start with letter, then by alphanumerics or '_').", feedBackByException);
                return false;
            }

            return true;
        }
    }
}