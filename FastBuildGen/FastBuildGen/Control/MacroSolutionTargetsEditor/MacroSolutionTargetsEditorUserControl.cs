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
    internal partial class MacroSolutionTargetsEditorUserControl : BaseUserControl
    {
#warning TODO - ALPHA point - do a better refresh for availlable modules : don't reset position of the list after an add
        // 2 way : a good refresh by add/remove only the right items
        // or scroll list after removing the element (bad but fast)

        #region Members

        private MacroSolutionTargetsEditorController _controller;
        private MacroSolutionTargetsEditorModel _model;

        #endregion Members

        #region ctor

        public MacroSolutionTargetsEditorUserControl()
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

        public void Initialize(MacroSolutionTargetsEditorModel model, MacroSolutionTargetsEditorController controller)
        {
            MacroSolutionTargetEditorModelWrapper moduleEditorModel = new MacroSolutionTargetEditorModelWrapper(model);
            MacroSolutionTargetEditorController moduleEditorController = new MacroSolutionTargetEditorController(moduleEditorModel);

            Initialize(model, controller, moduleEditorModel, moduleEditorController);
        }

        public void Initialize(MacroSolutionTargetsEditorModel model, MacroSolutionTargetsEditorController controller
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