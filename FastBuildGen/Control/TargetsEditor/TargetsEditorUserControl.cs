using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.Control;
using FastBuildGen.Control.TargetEditor;
using FastBuildGen.Common.UI;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Control.TargetsEditor
{
    internal partial class TargetsEditorUserControl : BaseUserControl
    {
        #region Members

        private TargetsEditorController _controller;
        private TargetsEditorModel _model;
        private IUIModel _targetEditorModel;

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
            TargetEditorController moduleEditorController = new TargetEditorController(moduleEditorModel);

            Initialize(model, controller, moduleEditorModel, moduleEditorController);
        }

        public void Initialize(TargetsEditorModel model, TargetsEditorController controller
            , TargetEditorModel targetEditorModel, TargetEditorController targetEditorController)
        {
            base.Initialize(model, controller);

            Debug.Assert(_model == null);
            Debug.Assert(_controller == null);

            _model = model;
            _controller = controller;

            _targetEditorModel = targetEditorModel;

            _listEditorUserControl.Initialize(_model, _controller);
            _targetEditorUserControl.Initialize(targetEditorModel, targetEditorController);

            _targetEditorModel.UIEnableViewRequested += OnUIEnableViewRequested;
        }

        #endregion ctor
        #region UIEnableView

        protected override void OnUIEnableViewRequested(object sender, UIEnableViewRequestedEventArgs e)
        {
            bool success = true;
            UIEnableViewRequestedEventArgs eventArgs = e;

            if (sender == _targetEditorModel)
            {
                success = UIEnableViewTargetEditor(sender, e);
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

        private bool UIEnableViewTargetEditor(object sender, UIEnableViewRequestedEventArgs e)
        {
            Debug.Assert(sender == _targetEditorModel);

            if (e == null)
                return false;

            IParamDescriptionHeoTarget target = e.Param as IParamDescriptionHeoTarget;
            if (target == null)
                return false;

            bool success = _controller.SelectTarget(target);

            return success;
        }

        #endregion UIEnableView
    }
}