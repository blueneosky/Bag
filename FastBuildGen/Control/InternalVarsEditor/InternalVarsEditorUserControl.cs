using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.Control;
using FastBuildGen.Common.UI;
using FastBuildGen.Control.InternalVarEditor;

namespace FastBuildGen.Control.InternalVarsEditor
{
    internal partial class InternalVarsEditorUserControl : BaseUserControl
    {
        #region Members

        private InternalVarsEditorController _controller;
        private IUIModel _internalVarEditorModel;
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
            base.Initialize(model, controller);

            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _internalVarEditorModel = internalVarEditorModel;

            _listEditorUserControl.Initialize(_model, _controller);
            _internalVarEditorUserControl.Initialize(internalVarEditorModel, internalVarEditorController);

            _internalVarEditorModel.UIEnableViewRequested += OnUIEnableViewRequested;
        }

        #endregion ctor

        #region UIEnableView

        protected override void OnUIEnableViewRequested(object sender, UIEnableViewRequestedEventArgs e)
        {
            bool success = true;
            UIEnableViewRequestedEventArgs eventArgs = e;

            if (sender == _internalVarEditorModel)
            {
                success = UIEnableViewInternalVarEditor(sender, e);
                if (success)
                    eventArgs = new UIEnableViewRequestedEventArgs();
            }

            if (success)
            {
                base.OnUIEnableViewRequested(sender, eventArgs);
                e.Canceled = eventArgs.Canceled;
            }
            else
            {
                e.Canceled = true;
            }
        }

        private bool UIEnableViewInternalVarEditor(object sender, UIEnableViewRequestedEventArgs e)
        {
            Debug.Assert(sender == _internalVarEditorModel);

            if (e == null)
                return false;

            string keyword = e.Param as string;
            if (String.IsNullOrEmpty(keyword))
                return false;

            bool success = _controller.SelectKeyword(keyword);

            return success;
        }

        #endregion UIEnableView
    }
}