﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using FastBuildGen.Common.Control;
using FastBuildGen.Control.ModuleEditor;

namespace FastBuildGen.Control.ModulesEditor
{
    internal partial class ModulesEditorUserControl : BaseUserControl
    {
        #region Members

        private ModulesEditorController _controller;
        private ModulesEditorModel _model;

        #endregion Members

        #region ctor

        public ModulesEditorUserControl()
        {
            InitializeComponent();

            if (false == DesignMode)
            {
                UserControl control = _moduleEditorUserControl;
                this.Controls.Remove(control);
                _listEditorUserControl.EditorPanel.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
        }

        public void Initialize(ModulesEditorModel model, ModulesEditorController controller)
        {
            ModuleEditorModelWrapper moduleEditorModel = new ModuleEditorModelWrapper(model);
            ModuleEditorController moduleEditorController = new ModuleEditorController(moduleEditorModel);

            Initialize(model, controller, moduleEditorModel, moduleEditorController);
        }

        public void Initialize(ModulesEditorModel model, ModulesEditorController controller
            , ModuleEditorModel moduleEditorModel, ModuleEditorController moduleEditorController)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _listEditorUserControl.Initialize(_model, _controller);
            _moduleEditorUserControl.Initialize(moduleEditorModel, moduleEditorController);
        }

        #endregion ctor
    }
}