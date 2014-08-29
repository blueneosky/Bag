using System;
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
    internal partial class ModuleEditorUserControl : BaseUserControl
    {
        #region Members

        private ModuleEditorController _controller;
        private ModuleEditorModel _model;

        private IParamDescriptionHeoModule _module;

        #endregion Members

        #region ctor

        public ModuleEditorUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(ModuleEditorModel model, ModuleEditorController controller)
        {
            PDEditorModel pdEditorModel = new PDEditorModelWrapper(model);
            PDEditorController pdEditorController = new PDEditorController(pdEditorModel);

            Initialize(model, controller, pdEditorModel, pdEditorController);
        }

        public void Initialize(ModuleEditorModel model, ModuleEditorController controller
            , PDEditorModel pdEditorModel, PDEditorController pdEditorController)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;
            _pdEditorUserControl.Initialize(pdEditorModel, pdEditorController);

            UpdateModule();
        }

        #endregion ctor

        #region Properties

        private IParamDescriptionHeoModule Module
        {
            get { return _module; }
            set
            {
                if (Object.Equals(_module, value))
                    return;
                if (_module != null)
                {
                    _module.PropertyChanged -= _module_PropertyChanged;
                }
                _module = value;
                if (_module != null)
                {
                    _module.PropertyChanged += _module_PropertyChanged;
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
                Module = null;
            }
            base.PartialDispose(disposing);
        }

        #endregion Overrides

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstModuleEditorModelEvent.ConstModule:
                    UpdateModule();
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        private void _module_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        private void UpdateModule()
        {
            Module = _model.Module;

            RefreshModule();
        }

        #endregion Modele Update

        #region UI Update

        private void RefreshModule()
        {
            BeginUpdate();

            bool withModule = (Module != null);
            this.Enabled = withModule;

            RefreshMSBuildTarget();
            RefreshPlatform();

            EndUpdate();
        }

        private void RefreshMSBuildTarget()
        {
            BeginUpdate();
            string text = (Module != null) ? Module.MSBuildTarget : String.Empty;
            _msBuildTargetTextBox.Text = text;
            EndUpdate();
        }

        private void RefreshPlatform()
        {
            BeginUpdate();

            if (Module != null)
            {
                switch (Module.Platform)
                {
                    case EnumPlatform.Win32:
                        _win32PlatformRadioButton.Checked = true;
                        break;

                    case EnumPlatform.X86:
                        _x86PlatformRadioButton.Checked = true;
                        break;

                    case EnumPlatform.Default:
                    default:
                        Debug.Fail("Non managed case");
                        break;
                }
            }
            else
            {
                _win32PlatformRadioButton.Checked = false;
                _x86PlatformRadioButton.Checked = false;
            }
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
            if (IsUpdating)
                return;

            if (_x86PlatformRadioButton.Checked)
                _controller.SetPlatform(EnumPlatform.X86);
        }

        private void win32PlatformRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            if (_win32PlatformRadioButton.Checked)
                _controller.SetPlatform(EnumPlatform.Win32);
        }

        #endregion User Inputs
    }
}