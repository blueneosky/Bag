using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Common.Forms;
using FastBuildGen.Control.InternalVarsEditor;
using FastBuildGen.Control.ModulesEditor;
using FastBuildGen.Control.TargetsEditor;
using ImputationH31per.Util;

namespace FastBuildGen.Forms.Main
{
    internal partial class MainForm : BaseForm
    {
        private MainFormController _controller;
        private string _initialText;
        private MainFormModel _model;

        #region ctor

        public MainForm(MainFormModel model, MainFormController controller)
            : base()
        {
            InitializeComponent();

            ModulesEditorModel modulesEditorModel = new ModulesEditorModel(model.FBModel);
            ModulesEditorController modulesEditorController = new ModulesEditorController(modulesEditorModel);

            TargetsEditorModel targetsEditorModel = new TargetsEditorModel(model.FBModel);
            TargetsEditorController targetsEditorController = new TargetsEditorController(targetsEditorModel);

            InternalVarsEditorModel internalVarsEditorModel = new InternalVarsEditorModel(model.FBModel);
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
            : base()
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

            _modulesEditorUserControl.Initialize(modulesEditorModel, modulesEditorController);
            _targetsEditorUserControl.Initialize(targetsEditorModel, targetsEditorController);
            _internalVarsEditorUserControl.Initialize(internalVarsEditorModel, internalVarsEditorController);

            _model.PropertyChanged += _model_PropertyChanged;

            ShortcutsManager.Add(Keys.Control | Keys.S, SaveActionShortcut);
        }

        #endregion ctor

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

        private void _saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.SaveAsConfigFile();
        }

        private void _saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.SaveFastBuildData();
        }

        private void SaveActionShortcut()
        {
            if (this.Validate())
            {
                _controller.SaveFastBuildData();
            }
        }

        #endregion User inputs
    }
}