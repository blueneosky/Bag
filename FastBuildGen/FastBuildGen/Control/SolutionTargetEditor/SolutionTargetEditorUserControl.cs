﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.Control;
using FastBuildGen.Control.PDEditor;
using FastBuildGen.BusinessModel.Old;

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
            PDEditorModel pdEditorModel = new PDEditorModelWrapper(model);
            PDEditorController pdEditorController = new PDEditorController(pdEditorModel);

            Initialize(model, controller, pdEditorModel, pdEditorController);
        }

        public void Initialize(SolutionTargetEditorModel model, SolutionTargetEditorController controller
            , PDEditorModel pdEditorModel, PDEditorController pdEditorController)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;
            _pdEditorUserControl.Initialize(pdEditorModel, pdEditorController);

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
                case ConstIParamDescriptionHeoModuleEvent.ConstMSBuildTarget:
                    RefreshMSBuildTarget();
                    break;

                case ConstIParamDescriptionHeoModuleEvent.ConstPlatform:
                    RefreshPlatform();
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

            RefreshModule();
        }

        #endregion Modele Update

        #region UI Update

        private void RefreshModule()
        {
            BeginUpdate();

            bool withModule = (SolutionTarget != null);
            this.Enabled = withModule;

            RefreshMSBuildTarget();
            RefreshPlatform();

            EndUpdate();
        }

        private void RefreshMSBuildTarget()
        {
            BeginUpdate();
            string text = (SolutionTarget != null) ? SolutionTarget.MSBuildTarget : String.Empty;
            _msBuildTargetTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshPlatform()
        {
            BeginUpdate();
#warning TODO BETA point - reactivate this code - replace Platform by Enabled ... (and rename stuff)

            //if (SolutionTarget != null)
            //{
            //    switch (SolutionTarget.Platform)
            //    {
            //        case EnumPlatform.Win32:
            //            _win32PlatformRadioButton.Checked = true;
            //            break;

            //        case EnumPlatform.X86:
            //            _x86PlatformRadioButton.Checked = true;
            //            break;

            //        case EnumPlatform.Default:
            //        default:
            //            Debug.Fail("Non managed case");
            //            break;
            //    }
            //}
            //else
            //{
            //    _win32PlatformRadioButton.Checked = false;
            //    _x86PlatformRadioButton.Checked = false;
            //}
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

        private void _x86PlatformRadioButton_CheckedChanged(object sender, EventArgs e)
        {
#warning TODO BETA point - reactivate this code - replace Platform by Enabled ... (and rename stuff)
            //if (IsUpdating)
            //    return;

            //if (_x86PlatformRadioButton.Checked)
            //    _controller.SetPlatform(EnumPlatform.X86);
        }

        private void win32PlatformRadioButton_CheckedChanged(object sender, EventArgs e)
        {
#warning TODO BETA point - reactivate this code - replace Platform by Enabled ... (and rename stuff)
            //if (IsUpdating)
            //    return;

            //if (_win32PlatformRadioButton.Checked)
            //    _controller.SetPlatform(EnumPlatform.Win32);
        }

        #endregion User Inputs
    }
}