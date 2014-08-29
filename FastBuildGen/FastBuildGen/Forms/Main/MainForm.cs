using System;
using System.ComponentModel;
using System.Windows.Forms;
using FastBuildGen.Common.Forms;
using FastBuildGen.Control.SolutionTargetsEditor;
using FastBuildGen.Control.MacroSolutionTargetsEditor;
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

            SolutionTargetsEditorModel modulesEditorModel = new SolutionTargetsEditorModel(model.FastBuildModel.FastBuildParamModel);
            SolutionTargetsEditorController modulesEditorController = new SolutionTargetsEditorController(modulesEditorModel);

            MacroSolutionTargetsEditorModel targetsEditorModel = new MacroSolutionTargetsEditorModel(model.FastBuildModel.FastBuildParamModel);
            MacroSolutionTargetsEditorController targetsEditorController = new MacroSolutionTargetsEditorController(targetsEditorModel);

            Initialize(model, controller
                , modulesEditorModel, modulesEditorController
                , targetsEditorModel, targetsEditorController);
        }

        public MainForm(MainFormModel model, MainFormController controller
            , SolutionTargetsEditorModel modulesEditorModel, SolutionTargetsEditorController modulesEditorController
            , MacroSolutionTargetsEditorModel targetsEditorModel, MacroSolutionTargetsEditorController targetsEditorController)
            : base()
        {
            InitializeComponent();

            Initialize(model, controller
                , modulesEditorModel, modulesEditorController
                , targetsEditorModel, targetsEditorController);
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

                default:
                    // ignored
                    break;
            }
        }

        private void Initialize(MainFormModel model, MainFormController controller
            , SolutionTargetsEditorModel modulesEditorModel, SolutionTargetsEditorController modulesEditorController
            , MacroSolutionTargetsEditorModel targetsEditorModel, MacroSolutionTargetsEditorController targetsEditorController)
        {
            _model = model;
            _controller = controller;

            _initialText = this.Text;   // need to be placed after InitializeComponent();

            _modulesEditorUserControl.Initialize(modulesEditorModel, modulesEditorController);
            _targetsEditorUserControl.Initialize(targetsEditorModel, targetsEditorController);

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

        private void RefreshFastBuildDataChanged()
        {
            bool state = _model.FastBuildDataChanged;

            this.Text = _initialText + (state ? "(*)" : String.Empty);
            _saveToolStripMenuItem.Enabled = state;
        }

        private void RefreshModel()
        {
            RefreshFastBuildDataChanged();
        }

        #endregion Refresh

        #region User inputs

        private void _importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.ImportConfigFile();
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