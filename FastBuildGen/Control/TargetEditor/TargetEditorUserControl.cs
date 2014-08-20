using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.Control;
using FastBuildGen.Control.PDEditor;

namespace FastBuildGen.Control.TargetEditor
{
    internal partial class TargetEditorUserControl : BaseUserControl
    {
        #region Members

        private TargetEditorController _controller;
        private TargetEditorModel _model;

        private IParamDescriptionHeoTarget _target;

        #endregion Members

        #region ctor

        public TargetEditorUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(TargetEditorModel model, TargetEditorController controller)
        {
            PDEditorModel pdEditorModel = new PDEditorModelWrapper(model);
            PDEditorController pdEditorController = new PDEditorController(pdEditorModel);

            Initialize(model, controller, pdEditorModel, pdEditorController);
        }

        public void Initialize(TargetEditorModel model, TargetEditorController controller
            , PDEditorModel pdEditorModel, PDEditorController pdEditorController)
        {
            base.Initialize(model, controller);

            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;
            _pdEditorUserControl.Initialize(pdEditorModel, pdEditorController);

            UpdateTarget();
            RefreshAvailableModules();
        }

        #endregion ctor

        #region Properties

        private IParamDescriptionHeoTarget Target
        {
            get { return _target; }
            set
            {
                if (Object.Equals(_target, value))
                    return;
                if (_target != null)
                {
                    _target.PropertyChanged -= _target_PropertyChanged;
                    _target.DependenciesChanged -= _target_DependenciesChanged;
                }
                _target = value;
                if (_target != null)
                {
                    _target.PropertyChanged += _target_PropertyChanged;
                    _target.DependenciesChanged += _target_DependenciesChanged;
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
                    UpdateTarget();
                    break;

                case ConstTargetEditorModelEvent.ConstAvailableModules:
                    RefreshAvailableModules();
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        private void _target_DependenciesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // for any new state
            RefreshDependecies();
        }

        private void _target_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                default:
                    // nothing - non managed case
                    break;
            }
        }

        #endregion Model events

        #region Modele Update

        private void UpdateTarget()
        {
            Target = _model.Target;

            RefreshModule();
        }

        #endregion Modele Update

        #region UI Update

        private void RefreshAvailableModules()
        {
            BeginUpdate();
            _availableListBox.BeginUpdate();

            IEnumerable<IParamDescriptionHeoModule> availableModules = _model.AvailableModules;

            _availableListBox.Items.Clear();
            object[] items = availableModules
                .Select(m => new ModuleItem(m))
                .OrderBy(m => m.ToString())
                .ToArray();
            _availableListBox.Items.AddRange(items);

            _availableListBox.EndUpdate();
            EndUpdate();
        }

        private void RefreshDependecies()
        {
            BeginUpdate();
            _modulesListBox.BeginUpdate();

            IEnumerable<IParamDescriptionHeoModule> modules = (Target != null) ? Target.Dependencies : new IParamDescriptionHeoModule[0];

            _modulesListBox.Items.Clear();
            object[] items = modules
                .Select(m => new ModuleItem(m))
                .OrderBy(m => m.ToString())
                .ToArray();
            _modulesListBox.Items.AddRange(items);

            _modulesListBox.EndUpdate();
            EndUpdate();
        }

        private void RefreshModule()
        {
            BeginUpdate();

            bool withTarget = (Target != null);
            this.Enabled = withTarget;

            RefreshDependecies();

            EndUpdate();
        }

        #endregion UI Update

        #region User Inputs

        private void _availableListBox_DoubleClick(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            ModuleItem item = _availableListBox.SelectedItem as ModuleItem;
            if (item == null)
                return;
            IParamDescriptionHeoModule module = item.Value;
            Debug.Assert(module != null);

            _controller.AddDependency(module);
        }

        private void _modulesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            ModuleItem item = _modulesListBox.SelectedItem as ModuleItem;
            if (item == null)
                return;
            IParamDescriptionHeoModule module = item.Value;
            Debug.Assert(module != null);

            _controller.RemoveDependency(module);
        }

        #endregion User Inputs
    }
}