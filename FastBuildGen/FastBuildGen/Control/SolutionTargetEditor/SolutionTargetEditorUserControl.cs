using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.Control;
using FastBuildGen.Control.TargetEditor;

namespace FastBuildGen.Control.SolutionTargetEditor
{
    internal partial class SolutionTargetEditorUserControl : BaseUserControl
    {
        #region Members

        private SolutionTargetEditorController _controller;
        private SolutionTargetEditorModel _model;

        private FBSolutionTarget _solutionTarget;

        #endregion Members

        #region ctor

        public SolutionTargetEditorUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(SolutionTargetEditorModel model, SolutionTargetEditorController controller)
        {
            TargetEditorModel targetEditorModel = new TargetEditorModelWrapper(model);
            TargetEditorController targetEditorController = new TargetEditorController(targetEditorModel);

            Initialize(model, controller, targetEditorModel, targetEditorController);
        }

        public void Initialize(SolutionTargetEditorModel model, SolutionTargetEditorController controller
            , TargetEditorModel targetEditorModel, TargetEditorController targetEditorController)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;
            _targetEditorUserControl.Initialize(targetEditorModel, targetEditorController);

            UpdateSolutionTarget();
        }

        #endregion ctor

        #region Properties

        private FBSolutionTarget SolutionTarget
        {
            get { return _solutionTarget; }
            set
            {
                if (Object.Equals(_solutionTarget, value))
                    return;
                if (_solutionTarget != null)
                {
                    _solutionTarget.PropertyChanged -= _solutionTarget_PropertyChanged;
                }
                _solutionTarget = value;
                if (_solutionTarget != null)
                {
                    _solutionTarget.PropertyChanged += _solutionTarget_PropertyChanged;
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
                SolutionTarget = null;
            }
            base.PartialDispose(disposing);
        }

        #endregion Overrides

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstSolutionTargetEditorModelEvent.ConstSolutionTarget:
                    UpdateSolutionTarget();
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        private void _solutionTarget_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstFBEvent.ConstFBTargetMSBuildTarget:
                    RefreshMSBuildTarget();
                    break;

                case ConstFBEvent.ConstFBTargetEnabled:
                    RefreshEnable();
                    break;

                default:
                    // nothing - non managed case
                    break;
            }
        }

        #endregion Model events

        #region Modele Update

        private void UpdateSolutionTarget()
        {
            SolutionTarget = _model.SolutionTarget;

            RefreshSolutionTarget();
        }

        #endregion Modele Update

        #region UI Update

        private void RefreshSolutionTarget()
        {
            BeginUpdate();

            bool withSolutionTarget = (SolutionTarget != null);
            bool isAllReadOnly = withSolutionTarget ? SolutionTarget.ReadOnly.HasFlag(EnumFBTargetReadonly.MaskFBSolutionTarget) : true;
            this.Enabled = withSolutionTarget && (false == isAllReadOnly);

            RefreshMSBuildTarget();
            RefreshEnable();

            EndUpdate();
        }

        private void RefreshMSBuildTarget()
        {
            BeginUpdate();
            bool withSolutionTarget = (SolutionTarget != null);
            bool isReadOnly = withSolutionTarget ? SolutionTarget.ReadOnly.HasFlag(EnumFBTargetReadonly.MSBuildTarget) : true;
            string text = withSolutionTarget ? SolutionTarget.MSBuildTarget : String.Empty;
            _msBuildTargetTextBox.Text = text;
            _msBuildTargetTextBox.ReadOnly = isReadOnly;
            EndUpdate();
        }

        private void RefreshEnable()
        {
            BeginUpdate();

            bool withSolutionTarget = (SolutionTarget != null);
            bool isReadOnly = withSolutionTarget ? SolutionTarget.ReadOnly.HasFlag(EnumFBTargetReadonly.MSBuildTarget) : true;
            if (withSolutionTarget)
            {
                _enabledCheckBox.Checked = SolutionTarget.Enabled;
            }
            else
            {
                _enabledCheckBox.CheckState = CheckState.Indeterminate;
            }
            _enabledCheckBox.Enabled = (false == isReadOnly);

            EndUpdate();
        }

        #endregion UI Update

        #region User Inputs

        private void _msBuildTargetTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (IsUpdating)
                return;

            Action action = delegate { _controller.SetMSBuildTarget(_msBuildTargetTextBox.Text); };
            ValidationWithErrorProvider(action, _msBuildTargetTextBox, _errorProvider, e, ErrorIconAlignment.MiddleLeft);
        }

        private void _enabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            _controller.SetEnabled(_enabledCheckBox.Checked);
        }

        #endregion User Inputs
    }
}