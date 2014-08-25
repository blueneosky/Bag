using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.Control;
using FastBuildGen.Control.InternalVarEditor;

namespace FastBuildGen.Control.InternalVarsEditor
{
    internal partial class InternalVarsEditorUserControl : BaseUserControl
    {
        #region Members

        private InternalVarsEditorController _controller;
        private InternalVarsEditorModel _model;

        #endregion Members

        #region ctor

        public InternalVarsEditorUserControl()
        {
            InitializeComponent();

            if (false == DesignMode)
            {
                UserControl control = _internalVarEditorUserControl;
                this.Controls.Remove(control);
                _listEditorUserControl.EditorPanel.Controls.Add(control);
                control.Dock = DockStyle.Fill;
            }
        }

        public void Initialize(InternalVarsEditorModel model, InternalVarsEditorController controller)
        {
            InternalVarEditorModelWrapper internalVarEditorModel = new InternalVarEditorModelWrapper(model);
            InternalVarEditorController internalVarEditorController = new InternalVarEditorController(internalVarEditorModel);

            Initialize(model, controller, internalVarEditorModel, internalVarEditorController);
        }

        public void Initialize(InternalVarsEditorModel model, InternalVarsEditorController controller
            , InternalVarEditorModel internalVarEditorModel, InternalVarEditorController internalVarEditorController)
        {
            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _listEditorUserControl.Initialize(_model, _controller);
            _internalVarEditorUserControl.Initialize(internalVarEditorModel, internalVarEditorController);
        }

        #endregion ctor
    }
}