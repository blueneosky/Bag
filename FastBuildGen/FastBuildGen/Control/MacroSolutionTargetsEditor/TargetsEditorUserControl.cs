using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.Control;
using FastBuildGen.Control.MacroSolutionTargetEditor;

namespace FastBuildGen.Control.MacroSolutionTargetsEditor
{
    internal partial class TargetsEditorUserControl : BaseUserControl
    {
        #region Members

        private TargetsEditorController _controller;
        private TargetsEditorModel _model;

        #endregion Members

        #region ctor

        public TargetsEditorUserControl()
        {
            InitializeComponent();

            if (false == DesignMode)
            {
                UserControl control = _targetEditorUserControl;
                this.Controls.Remove(control);
                _listEditorUserControl.EditorPanel.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
        }

        public void Initialize(TargetsEditorModel model, TargetsEditorController controller)
        {
            TargetEditorModelWrapper moduleEditorModel = new TargetEditorModelWrapper(model);
            MacroSolutionTargetEditorController moduleEditorController = new MacroSolutionTargetEditorController(moduleEditorModel);

            Initialize(model, controller, moduleEditorModel, moduleEditorController);
        }

        public void Initialize(TargetsEditorModel model, TargetsEditorController controller
            , MacroSolutionTargetEditorModel targetEditorModel, MacroSolutionTargetEditorController targetEditorController)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _listEditorUserControl.Initialize(_model, _controller);
            _targetEditorUserControl.Initialize(targetEditorModel, targetEditorController);
        }

        #endregion ctor
    }
}