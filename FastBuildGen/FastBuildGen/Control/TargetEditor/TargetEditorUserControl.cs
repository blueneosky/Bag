using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.Control;

namespace FastBuildGen.Control.TargetEditor
{
    internal partial class TargetEditorUserControl : BaseUserControl
    {
        #region Members

        private TargetEditorController _controller;
        private TargetEditorModel _model;

        private FBTarget _target;

        #endregion Members

        #region ctor

        public TargetEditorUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(TargetEditorModel model, TargetEditorController controller)
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

        private FBTarget Target
        {
            get { return _target; }
            set
            {
                if (Object.Equals(_target, value))
                    return;
                if (_target != null)
                {
                    _target.PropertyChanged -= _target_PropertyChanged;
                }
                _target = value;
                if (_target != null)
                {
                    _target.PropertyChanged += _target_PropertyChanged;
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
                Target = null;
            }
            base.PartialDispose(disposing);
        }

        #endregion Overrides

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstTargetEditorModelEvent.ConstTarget:
                    UpdateParamDescription();
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        private void _target_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstFBEvent.ConstFBTargetKeyword:
                    RefreshKeyword();
                    break;

                case ConstFBEvent.ConstFBTargetHelpText:
                    RefreshHelpText();
                    break;

                case ConstFBEvent.ConstFBTargetSwitchKeyword:
                    RefreshSwitchKeyboard();
                    break;

                case ConstFBEvent.ConstFBTargetParamVarName:
                    RefreshParamVarName();
                    break;

                case ConstFBEvent.ConstFBTargetVarName:
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
            Target = _model.Target;

            RefreshTarget();
        }

        #endregion Model Update

        #region UI Update

        private void RefreshHelpText()
        {
            BeginUpdate();
            bool withTarget = (Target != null);
            bool isReadOnly = withTarget ? Target.ReadOnly.HasFlag(EnumFBTargetReadonly.HelpText) : true;
            string text = (Target != null) ? Target.HelpText : String.Empty;
            _helpTextTextBox.Text = text;
            _helpTextTextBox.ReadOnly = isReadOnly;
            EndUpdate();
        }

        private void RefreshKeyword()
        {
            BeginUpdate();
            bool withTarget = (Target != null);
            bool isReadOnly = withTarget ? Target.ReadOnly.HasFlag(EnumFBTargetReadonly.Keyword) : true;
            string text = (Target != null) ? Target.Keyword : String.Empty;
            _keywordTextBox.Text = text;
            _keywordTextBox.ReadOnly = isReadOnly;
            EndUpdate();
        }

        private void RefreshTarget()
        {
            BeginUpdate();

            bool withTarget = (Target != null);
            this.Enabled = withTarget;

            RefreshKeyword();
            RefreshHelpText();
            RefreshSwitchKeyboard();
            RefreshParamVarName();
            RefreshVarName();

            EndUpdate();
        }

        private void RefreshParamVarName()
        {
            BeginUpdate();
            string text = (Target != null) ? Target.ParamVarName : String.Empty;
            _paramVarNameTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshSwitchKeyboard()
        {
            BeginUpdate();
            string text = (Target != null) ? Target.SwitchKeyword : String.Empty;
            _switchKeywordTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshVarName()
        {
            BeginUpdate();
            string text = (Target != null) ? Target.VarName : String.Empty;
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

        #endregion User Inputs
    }
}