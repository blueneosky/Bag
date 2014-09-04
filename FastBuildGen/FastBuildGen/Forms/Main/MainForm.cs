using System;
using System.ComponentModel;
using System.Windows.Forms;
using FastBuildGen.Common.Forms;
using FastBuildGen.Control.MacroSolutionTargetsEditor;
using FastBuildGen.Control.SolutionTargetsEditor;
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

            SolutionTargetsEditorModel modulesEditorModel = new SolutionTargetsEditorModel(model.ApplicationModel);
            SolutionTargetsEditorController modulesEditorController = new SolutionTargetsEditorController(modulesEditorModel);

            MacroSolutionTargetsEditorModel targetsEditorModel = new MacroSolutionTargetsEditorModel(model.ApplicationModel);
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
                case ConstMainFormModelEvent.ConstApplicationModelFilePath:
                    RefreshTitle();
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
            bool success = _controller.SaveFBModelBeforeClosing();
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

        private void RefreshTitle()
        {
            bool state;
            string filePath;
            if (_model.ApplicationModel.FBModel == null)
            {
                state = false;
                filePath = "<none>";
            }
            else
            {
                state = _model.FastBuildDataChanged;
                filePath = _model.FilePath;
                if (String.IsNullOrEmpty(filePath))
                    filePath = "<new>";
            }

            this.Text = _initialText + " - " + filePath + (state ? "(*)" : String.Empty);
            _saveToolStripMenuItem.Enabled = state;
        }

        private void RefreshModel()
        {
            RefreshTitle();
        }

        #endregion Refresh

        #region User inputs

        private void _mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.MergeWithSln();
        }

        private void _quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.SaveAs();
        }

        private void _saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.Save();
        }

        private void SaveActionShortcut()
        {
            bool state = _model.FastBuildDataChanged;
            if (this.Validate() && state)
            {
                _controller.Save();
            }
        }

        private void _newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.NewWithSln();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.Open();
        }

        #endregion User inputs
    }
}