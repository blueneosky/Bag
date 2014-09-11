﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.Control;
using FastBuildGen.Control.PDEditor;

namespace FastBuildGen.Control.MacroSolutionTargetEditor
{
    internal partial class MacroSolutionTargetEditorUserControl : BaseUserControl
    {
        #region Members

        private MacroSolutionTargetEditorController _controller;
        private MacroSolutionTargetEditorModel _model;

        private FBMacroSolutionTarget _macroSolutionTarget;

        #endregion Members

        #region ctor

        public MacroSolutionTargetEditorUserControl()
        {
            InitializeComponent();
        }

        public void Initialize(MacroSolutionTargetEditorModel model, MacroSolutionTargetEditorController controller)
        {
            PDEditorModel pdEditorModel = new PDEditorModelWrapper(model);
            PDEditorController pdEditorController = new PDEditorController(pdEditorModel);

            Initialize(model, controller, pdEditorModel, pdEditorController);
        }

        public void Initialize(MacroSolutionTargetEditorModel model, MacroSolutionTargetEditorController controller
            , PDEditorModel pdEditorModel, PDEditorController pdEditorController)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _model.PropertyChanged += _model_PropertyChanged;
            _pdEditorUserControl.Initialize(pdEditorModel, pdEditorController);

            UpdateSolutionTargets();
            RefreshAvailableSolutionTargets();
        }

        #endregion ctor

        #region Properties

        private FBMacroSolutionTarget MacroSolutionTarget
        {
            get { return _macroSolutionTarget; }
            set
            {
                if (Object.Equals(_macroSolutionTarget, value))
                    return;
                if (_macroSolutionTarget != null)
                {
                    _macroSolutionTarget.PropertyChanged -= _target_PropertyChanged;
                    _macroSolutionTarget.SolutionTargetIds.CollectionChanged -= SolutionTargetIds_CollectionChanged;
                }
                _macroSolutionTarget = value;
                if (_macroSolutionTarget != null)
                {
                    _macroSolutionTarget.PropertyChanged += _target_PropertyChanged;
                    _macroSolutionTarget.SolutionTargetIds.CollectionChanged += SolutionTargetIds_CollectionChanged;
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
                MacroSolutionTarget = null;
            }
            base.PartialDispose(disposing);
        }

        #endregion Overrides

        #region Model events

        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstMacroSolutionTargetEditorModelEvent.ConstMacroSolutionTarget:
                    UpdateSolutionTargets();
                    break;

                case ConstMacroSolutionTargetEditorModelEvent.ConstAvailableSolutionTargets:
                    RefreshAvailableSolutionTargets();
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        private void SolutionTargetIds_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // for any new state
            RefreshSolutionTargets();
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

        private void UpdateSolutionTargets()
        {
            MacroSolutionTarget = _model.MacroSolutionTarget;

            RefreshMacroSolutionTarget();
        }

        #endregion Modele Update

        #region UI Update

        private void RefreshAvailableSolutionTargets()
        {
            BeginUpdate();
            _availableListBox.BeginUpdate();

            IEnumerable<FBSolutionTarget> availableSolutionTargets = _model.AvailableSolutionTargets;

            _availableListBox.Items.Clear();
            object[] items = availableSolutionTargets
                .Select(m => new SolutionTargetItem(m))
                .OrderBy(m => m.ToString())
                .ToArray();
            _availableListBox.Items.AddRange(items);

            _availableListBox.EndUpdate();
            EndUpdate();
        }

        private void RefreshSolutionTargets()
        {
            BeginUpdate();

            bool withMacroSolutionTarget = (MacroSolutionTarget != null);
            bool isReadOnly = withMacroSolutionTarget ? MacroSolutionTarget.ReadOnly.HasFlag(EnumFBTargetReadonly.SolutionTargetIds) : true;

            _projectsListBox.BeginUpdate();

            IEnumerable<FBSolutionTarget> solutionTargets = new FBSolutionTarget[0];
            if ((MacroSolutionTarget != null))
            {
                solutionTargets = MacroSolutionTarget.SolutionTargetIds
                    .Join(_model.ApplicationModel.FBModel.SolutionTargets, id => id, st => st.Id, (id, st) => st);
            }

            _projectsListBox.Items.Clear();
            object[] items = solutionTargets
                .Select(m => new SolutionTargetItem(m))
                .OrderBy(m => m.ToString())
                .ToArray();
            _projectsListBox.Items.AddRange(items);

            _projectsListBox.EndUpdate();

            _splitContainer.Enabled = (false == isReadOnly);

            EndUpdate();
        }

        private void RefreshMacroSolutionTarget()
        {
            BeginUpdate();

            bool withMacroSolutionTarget = (MacroSolutionTarget != null);
            bool isAllReadOnly = withMacroSolutionTarget ? MacroSolutionTarget.ReadOnly.HasFlag(EnumFBTargetReadonly.MaskFBMacroSolutionTarget) : true;
            this.Enabled = withMacroSolutionTarget && (false == isAllReadOnly);

            RefreshSolutionTargets();

            EndUpdate();
        }

        #endregion UI Update

        #region User Inputs

        private void _availableListBox_DoubleClick(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            SolutionTargetItem item = _availableListBox.SelectedItem as SolutionTargetItem;
            if (item == null)
                return;
            FBSolutionTarget solutionTarget = item.Value;
            Debug.Assert(solutionTarget != null);

            _controller.AddDependency(solutionTarget);
        }

        private void _modulesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (IsUpdating)
                return;

            SolutionTargetItem item = _projectsListBox.SelectedItem as SolutionTargetItem;
            if (item == null)
                return;
            FBSolutionTarget solutionTarget = item.Value;
            Debug.Assert(solutionTarget != null);

            _controller.RemoveDependency(solutionTarget);
        }

        #endregion User Inputs
    }
}