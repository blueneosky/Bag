using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.Forms;
using FastBuildGen.Common.UI;
using FastBuildGen.Control.InternalVarsEditor;
using FastBuildGen.Control.ModulesEditor;
using FastBuildGen.Control.TargetsEditor;
using ImputationH31per.Util;
using FastBuildGen.Common.UndoRedo;

namespace FastBuildGen.Forms.Main
{
    internal partial class MainForm : BaseForm
    {
        private MainFormController _controller;
        private string _initialText;
        private IUIModel _internalVarsEditorModel;
        private MainFormModel _model;
        private IUIModel _modulesEditorModel;
        private ShortcutsManager _shortcutsManager;
        private IUIModel _targetsEditorModel;

        #region ctor

        public MainForm(MainFormModel model, MainFormController controller)
            : base(model, controller)
        {
            InitializeComponent();

            ModulesEditorModel modulesEditorModel = new ModulesEditorModel(model.FastBuildModel.FastBuildParamModel, model.UndoRedoManager);
            ModulesEditorController modulesEditorController = new ModulesEditorController(modulesEditorModel);

            TargetsEditorModel targetsEditorModel = new TargetsEditorModel(model.FastBuildModel.FastBuildParamModel, model.UndoRedoManager);
            TargetsEditorController targetsEditorController = new TargetsEditorController(targetsEditorModel);

            InternalVarsEditorModel internalVarsEditorModel = new InternalVarsEditorModel(model.FastBuildModel.FastBuildInternalVarModel, model.UndoRedoManager);
            InternalVarsEditorController internalVarsEditorController = new InternalVarsEditorController(internalVarsEditorModel);

            Initialize(model, controller
                , modulesEditorModel, modulesEditorController
                , targetsEditorModel, targetsEditorController
                , internalVarsEditorModel, internalVarsEditorController);
        }

        public MainForm(MainFormModel model, MainFormController controller
            , ModulesEditorModel modulesEditorModel, ModulesEditorController modulesEditorController
            , TargetsEditorModel targetsEditorModel, TargetsEditorController targetsEditorController
            , InternalVarsEditorModel internalVarsEditorModel, InternalVarsEditorController internalVarsEditorController)
            : base(model, controller)
        {
            InitializeComponent();

            Initialize(model, controller
                , modulesEditorModel, modulesEditorController
                , targetsEditorModel, targetsEditorController
                , internalVarsEditorModel, internalVarsEditorController);
        }

        private MainForm()
        {
            InitializeComponent();
        }

        private void UndoRedoManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstIUndoRedoManagerEvent.ConstCanRedo:
                    break;
                case ConstIUndoRedoManagerEvent.ConstCanUndo:
                    break;
                case ConstIUndoRedoManagerEvent.ConstRedoActions:
                    break;
                case ConstIUndoRedoManagerEvent.ConstUndoActions:
                    break;
                case ConstIUndoRedoManagerEvent.ConstRelativeTokenPosition:
#warning TODO
                    break;
                default:
                    break;
            }
        }


        private void _model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case ConstMainFormModelEvent.ConstFastBuildDataChanged:
                    RefreshFastBuildDataChanged();
                    break;

                case ConstMainFormModelEvent.ConstActivePanel:
                    RefreshActivePanel();
                    break;

                default:
                    // ignored
                    break;
            }
        }

        private void Initialize(MainFormModel model, MainFormController controller
            , ModulesEditorModel modulesEditorModel, ModulesEditorController modulesEditorController
            , TargetsEditorModel targetsEditorModel, TargetsEditorController targetsEditorController
            , InternalVarsEditorModel internalVarsEditorModel, InternalVarsEditorController internalVarsEditorController)
        {
            _model = model;
            _controller = controller;

            _initialText = this.Text;   // need to be placed after InitializeComponent();

            _modulesEditorModel = modulesEditorModel;
            _targetsEditorModel = targetsEditorModel;
            _internalVarsEditorModel = internalVarsEditorModel;

            _modulesEditorUserControl.Initialize(modulesEditorModel, modulesEditorController);
            _targetsEditorUserControl.Initialize(targetsEditorModel, targetsEditorController);
            _internalVarsEditorUserControl.Initialize(internalVarsEditorModel, internalVarsEditorController);

            _model.PropertyChanged += _model_PropertyChanged;
            _model.UndoRedoManager.PropertyChanged += UndoRedoManager_PropertyChanged;

            _modulesEditorModel.UIEnableViewRequested += OnUIEnableViewRequested;
            _targetsEditorModel.UIEnableViewRequested += OnUIEnableViewRequested;
            _internalVarsEditorModel.UIEnableViewRequested += OnUIEnableViewRequested;

            _shortcutsManager = new ShortcutsManager()
            {
                { Keys.Control | Keys.S , SaveActionShortcut },
                { Keys.Control | Keys.Z , UndoActionShortcut },
                { Keys.Control | Keys.Y , RedoActionShortcut },
            };
        }

        #endregion ctor

        #region UIEnableView

        protected override void OnUIEnableViewRequested(object sender, UIEnableViewRequestedEventArgs e)
        {
            bool success = true;
            UIEnableViewRequestedEventArgs eventArgs = e;

            if (sender == _modulesEditorModel)
            {
                success = UIEnableViewModulesEditor(sender, e);
                if (success)
                    eventArgs = new UIEnableViewRequestedEventArgs();
            }
            else if (sender == _targetsEditorModel)
            {
                success = UIEnableViewTargetsEditor(sender, e);
                if (success)
                    eventArgs = new UIEnableViewRequestedEventArgs();
            }
            else if (sender == _internalVarsEditorModel)
            {
                success = UIEnableViewInternalVarsEditor(sender, e);
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

        private bool UIEnableViewInternalVarsEditor(object sender, UIEnableViewRequestedEventArgs e)
        {
            Debug.Assert(sender == _internalVarsEditorModel);

            bool success = _controller.SelectInternalVarsEditor();

            return success;
        }

        private bool UIEnableViewModulesEditor(object sender, UIEnableViewRequestedEventArgs e)
        {
            Debug.Assert(sender == _modulesEditorModel);

            bool success = _controller.SelectModulesEditor();

            return success;
        }

        private bool UIEnableViewTargetsEditor(object sender, UIEnableViewRequestedEventArgs e)
        {
            Debug.Assert(sender == _targetsEditorModel);

            bool success = _controller.SelectTargetsEditor();

            return success;
        }

        #endregion UIEnableView

        #region Override

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            bool success = _controller.SaveFastBuildDataBeforeClosing();
            e.Cancel = (false == success);

            base.OnFormClosing(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RefreshModel();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool traite = _shortcutsManager.ProcessKey(keyData);
            if (traite)
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion Override

        #region Refresh

        private void RefreshActivePanel()
        {
            string activePanel = _model.ActivePanel;
            switch (activePanel)
            {
                case MainFormModel.ConstActivePanelModulesEditor:
                    if (_mainTabControl.SelectedTab != _modulesTabPage)
                        _mainTabControl.SelectedTab = _modulesTabPage;
                    break;

                case MainFormModel.ConstActivePanelTargetsEditor:
                    if (_mainTabControl.SelectedTab != _targetsTabPage)
                        _mainTabControl.SelectedTab = _targetsTabPage;
                    break;

                case MainFormModel.ConstActivePanelInternalVarsEditor:
                    if (_mainTabControl.SelectedTab != _propertiesTabPage)
                        _mainTabControl.SelectedTab = _propertiesTabPage;
                    break;

                default:
                    Debug.Fail("Non managed case");
                    break;
            }
        }

        private void RefreshFastBuildDataChanged()
        {
            bool state = _model.FastBuildDataChanged;

            this.Text = _initialText + (state ? "(*)" : String.Empty);
            _saveToolStripMenuItem.Enabled = state;
        }

        private void RefreshModel()
        {
            RefreshFastBuildDataChanged();
            RefreshActivePanel();
        }

        #endregion Refresh

        #region User inputs

        private void _importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ImportConfigFile();
        }

        private void _mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_mainTabControl.SelectedTab == _modulesTabPage)
            {
                _controller.SelectModulesEditor();
            }
            else if (_mainTabControl.SelectedTab == _targetsTabPage)
            {
                _controller.SelectTargetsEditor();
            }
            else if (_mainTabControl.SelectedTab == _propertiesTabPage)
            {
                _controller.SelectInternalVarsEditor();
            }
            else
            {
                Debug.Fail("Unexpected case");
            }
        }

        private void _mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.MergeConfigFile();
        }

        private void _quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.Redo();
        }

        private void _saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.SaveAsConfigFile();
        }
        private void _saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.SaveFastBuildData();
        }

        private void _undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.Undo();
        }

        private void RedoActionShortcut()
        {
            if (this.Validate())
            {
                _controller.Redo();
            }
        }

        private void SaveActionShortcut()
        {
            if (this.Validate())
            {
                _controller.SaveFastBuildData();
            }
        }

        private void UndoActionShortcut()
        {
            if (this.Validate())
            {
                _controller.Undo();
            }
        }
        #endregion User inputs
    }
}