﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using FastBuildGen.Common.Forms;
using FastBuildGen.Control.MacroSolutionTargetsEditor;
using FastBuildGen.Control.SolutionTargetsEditor;
using ImputationH31per.Util;
using FastBuildGen.Control.Faq;

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

            FaqModel faqModel = new FaqModel();
            FaqController faqController = new FaqController(faqModel);

            Initialize(model, controller
                , modulesEditorModel, modulesEditorController
                , targetsEditorModel, targetsEditorController
                , faqModel, faqController);
        }

        public MainForm(MainFormModel model, MainFormController controller
            , SolutionTargetsEditorModel modulesEditorModel, SolutionTargetsEditorController modulesEditorController
            , MacroSolutionTargetsEditorModel targetsEditorModel, MacroSolutionTargetsEditorController targetsEditorController
            , FaqModel faqModel, FaqController faqController)
            : base()
        {
            InitializeComponent();

            Initialize(model, controller
                , modulesEditorModel, modulesEditorController
                , targetsEditorModel, targetsEditorController
                , faqModel, faqController);
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
                case ConstMainFormModelEvent.ConstFBModelChanged:
                    RefreshGui();
                    break;

                default:
                    // ignored
                    break;
            }
        }

        private void Initialize(MainFormModel model, MainFormController controller
            , SolutionTargetsEditorModel modulesEditorModel, SolutionTargetsEditorController modulesEditorController
            , MacroSolutionTargetsEditorModel targetsEditorModel, MacroSolutionTargetsEditorController targetsEditorController
            , FaqModel faqModel, FaqController faqController)
        {
            _model = model;
            _controller = controller;

            _initialText = this.Text;   // need to be placed after InitializeComponent();

            _solutionTargetsEditorUserControl.Initialize(modulesEditorModel, modulesEditorController);
            _macroSolutionTargetsEditorUserControl.Initialize(targetsEditorModel, targetsEditorController);
            _faqUserControl.Initialize(faqModel, faqController);

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

        private void RefreshGui()
        {
            bool state;
            string filePath;
            if (_model.FBModel == null)
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
            _mergeToolStripMenuItem.Enabled = _model.FBModel != null;
        }

        private void RefreshModel()
        {
            RefreshGui();
        }

        #endregion Refresh

        #region User inputs

        private void _mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (false == Validate()) return;
            _controller.MergeWithSln();
        }

        private void _quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (false == Validate()) return;
            this.Close();
        }

        private void _saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (false == Validate()) return;
            _controller.SaveAs();
        }

        private void _saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool state = _model.FastBuildDataChanged;
            if (this.Validate() && state)
            {
                _controller.Save();
            }
        }

        private void SaveActionShortcut()
        {
            if (this.Validate())
            {
                bool state = _model.FastBuildDataChanged;
                if (state)
                {
                    _controller.Save();
                }
            }
        }

        private void _newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (false == Validate()) return;
            _controller.NewWithSln();
        }

        private void _openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (false == Validate()) return;
            _controller.Open();
        }

        #endregion User inputs
    }
}