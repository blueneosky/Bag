using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.Control;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Control.PDEditor
{
    internal partial class PDEditorUserControl : BaseUserControl
    {
        #region Members

        private PDEditorController _controller;
        private PDEditorModel _model;

        private IParamDescription _paramDescription;

        #endregion Members

        #region ctor

        public PDEditorUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(PDEditorModel model, PDEditorController controller)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;

            UpdateParamDescription();
        }

        #endregion ctor

        #region Properties

        private IParamDescription ParamDescription
        {
            get { return _paramDescription; }
            set
            {
                if (Object.Equals(_paramDescription, value))
                    return;
                if (_paramDescription != null)
                {
                    _paramDescription.PropertyChanged -= _paramDescription_PropertyChanged;
                }
                _paramDescription = value;
                if (_paramDescription != null)
                {
                    _paramDescription.PropertyChanged += _paramDescription_PropertyChanged;
                }
            }
        }

        #endregion Properties

        #region Overrides

        protected override void PartialDispose(bool disposing)
        {
            if (disposing && (_model != null))
            {
                _model.PropertyChanged -= _model_PropertyChanged;
                ParamDescription = null;
            }
            base.PartialDispose(disposing);
        }

        #endregion Overrides

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstPDEditorModelEvent.ConstTarget:
                    UpdateParamDescription();
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        private void _paramDescription_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstIParamDescriptionEvent.ConstKeyword:
                    RefreshKeyword();
                    break;

                case ConstIParamDescriptionEvent.ConstName:
                    RefreshName();
                    break;

                case ConstIParamDescriptionEvent.ConstHelpText:
                    RefreshHelpText();
                    break;

                case ConstIParamDescriptionEvent.ConstSwitchKeyword:
                    RefreshSwitchKeyboard();
                    break;

                case ConstIParamDescriptionEvent.ConstParamVarName:
                    RefreshParamVarName();
                    break;

                case ConstIParamDescriptionEvent.ConstVarName:
                    RefreshVarName();
                    break;

                default:
                    // nothing - non managed case
                    break;
            }
        }

        #endregion Model events

        #region Model Update

        private void UpdateParamDescription()
        {
            ParamDescription = _model.Target;

            RefreshParamDescription();
        }

        #endregion Model Update

        #region UI Update

        private void RefreshHelpText()
        {
            BeginUpdate();
            string text = (ParamDescription != null) ? ParamDescription.HelpText : String.Empty;
            _helpTextTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshKeyword()
        {
            BeginUpdate();
            string text = (ParamDescription != null) ? ParamDescription.Keyword : String.Empty;
            _keywordTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshName()
        {
            BeginUpdate();
            string text = (ParamDescription != null) ? ParamDescription.Name : String.Empty;
            _nameTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshParamDescription()
        {
            BeginUpdate();

            bool withParamDescription = (ParamDescription != null);
            this.Enabled = withParamDescription;

            RefreshKeyword();
            RefreshName();
            RefreshHelpText();
            RefreshSwitchKeyboard();
            RefreshParamVarName();
            RefreshVarName();

            EndUpdate();
        }

        private void RefreshParamVarName()
        {
            BeginUpdate();
            string text = (ParamDescription != null) ? ParamDescription.ParamVarName : String.Empty;
            _paramVarNameTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshSwitchKeyboard()
        {
            BeginUpdate();
            string text = (ParamDescription != null) ? ParamDescription.SwitchKeyword : String.Empty;
            _switchKeywordTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshVarName()
        {
            BeginUpdate();
            string text = (ParamDescription != null) ? ParamDescription.VarName : String.Empty;
            _varNameTextBox.Text = text;
            EndUpdate();
        }

        #endregion UI Update

        #region User Inputs

        private void _helpTextTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (IsUpdating)
                return;

            Action action = delegate { _controller.SetHelpText(_helpTextTextBox.Text); };
            ValidationWithErrorProvider(action, _helpTextTextBox, _errorProvider, e, ErrorIconAlignment.MiddleLeft);
        }

        private void _keywordTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (IsUpdating)
                return;

            Action action = delegate { _controller.SetKeyword(_keywordTextBox.Text, true); };
            ValidationWithErrorProvider(action, _keywordTextBox, _errorProvider, e, ErrorIconAlignment.MiddleLeft);
        }

        private void _nameTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (IsUpdating)
                return;

            Action action = delegate { _controller.SetName(_nameTextBox.Text, true); };
            ValidationWithErrorProvider(action, _nameTextBox, _errorProvider, e, ErrorIconAlignment.MiddleLeft);
        }

        #endregion User Inputs
    }
}