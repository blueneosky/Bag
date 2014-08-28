using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.Control;

namespace FastBuildGen.Control.InternalVarEditor
{
    internal partial class InternalVarEditorUserControl : BaseUserControl
    {
        #region Members

        private InternalVarEditorController _controller;
        private InternalVarEditorModel _model;

        #endregion Members

        #region ctor

        public InternalVarEditorUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(InternalVarEditorModel model, InternalVarEditorController controller)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;

            RefreshModel();
        }

        #endregion ctor

        #region Overrides

        protected override void PartialDispose(bool disposing)
        {
            if (disposing && (_model != null))
            {
                _model.PropertyChanged -= _model_PropertyChanged;
            }
            base.PartialDispose(disposing);
        }

        #endregion Overrides

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstInternalVarEditorModelEvent.ConstKeyword:
                    RefreshModel();
                    break;

                case ConstInternalVarEditorModelEvent.ConstValue:
                    RefreshValue();
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        #endregion Model events

        #region UI Update

        private void RefreshKeyword()
        {
            BeginUpdate();

            string keyword = _model.Keyword;
            _keywordTextBox.Text = keyword;

            EndUpdate();
        }

        private void RefreshModel()
        {
            BeginUpdate();

            bool withValideKeyWord = false == String.IsNullOrEmpty(_model.Keyword);

            RefreshKeyword();
            RefreshValue();

            this.Enabled = withValideKeyWord;

            EndUpdate();
        }

        private void RefreshValue()
        {
            BeginUpdate();

            string value = _model.Value;
            _valueTextBox.Text = value;

            EndUpdate();
        }

        #endregion UI Update

        #region User Inputs

        private void _valueTextBox_Validating(object sender, CancelEventArgs e)
        {
            string value = _valueTextBox.Text;
            Action action = delegate { _controller.SetValue(value); };
            ValidationWithErrorProvider(action, _valueTextBox, _errorProvider, e, ErrorIconAlignment.MiddleLeft);
        }

        #endregion User Inputs
    }
}