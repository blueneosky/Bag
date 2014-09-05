using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using FastBuildGen.File;
using FastBuildGen.VisualStudio;

namespace FastBuildGen.Forms.Main
{
    internal class MainFormController
    {
        private const string ConstDialogFilterFBFileExt = "FastBuild file (*.bat)|*.bat";
        private const string ConstDialogFilterVSSolutionFileExt = "Visual Studio Solution file (*.sln)|*.sln";

        private readonly MainFormModel _model;

        public MainFormController(MainFormModel model)
        {
            _model = model;
        }

        internal void Open()
        {
            if (false == SaveFBModelBeforeClosing())
                return;

            OpenCore();
        }

        internal void Save()
        {
            if ((_model.FBModel == null) || (_model.FastBuildDataChanged == false))
                return;

            SaveFBModelCore();
        }

        internal bool SaveAs()
        {
            if (_model.FBModel == null)
                return false;

            return SaveAsFBModelCore();
        }

        internal bool NewWithSln()
        {
            return NewOrMergeConfigFileCore(false);
        }

        internal bool MergeWithSln()
        {
            return NewOrMergeConfigFileCore(true);
        }

        internal bool SaveFBModelBeforeClosing()
        {
            // no change
            if (_model.FBModel == null)
                return true;

            if (false == _model.FastBuildDataChanged)
                return true;

            DialogResult dialogResult = MessageBox.Show(
                "Did you wan't save your work ?"
                , "FastBuild Generator"
                , MessageBoxButtons.YesNoCancel
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return false;

            if (dialogResult == DialogResult.No)
                return true;

            return SaveFBModelCore();
        }

        #region Private

        private bool NewOrMergeConfigFileCore(bool withMerge)
        {
            bool success = false;

            if ((_model.FBModel != null) && _model.FastBuildDataChanged)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Did you want save before " + (withMerge ? "merge" : "create new file") + " ?"
                    , (withMerge ? "Merge" : "New")
                    , MessageBoxButtons.YesNoCancel
                    , MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button1);

                if (dialogResult == DialogResult.Cancel)
                    return false;

                if (dialogResult == DialogResult.Yes)
                {
                    success = SaveAs();
                    if (false == success)
                        return false;
                }
            }

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = ConstDialogFilterVSSolutionFileExt;
                DialogResult dialogResult = dialog.ShowDialog();
                if (dialogResult != DialogResult.Cancel)
                {
                    string solutionFilePath = dialog.FileName;
                    if (withMerge)
                    {
                        success = MergeWithSlnFileCore(solutionFilePath);
                    }
                    else
                    {
                        success = NewWithSlnFileCore(solutionFilePath);
                    }
                }
            }

            return success;
        }

        private bool NewWithSlnFileCore(string solutionFilePath)
        {
            FBModel fbModel = new FBModel();
            _model.ApplicationModel.FBModel = fbModel;
            _model.FilePath = null;
            _model.ApplicationModel.DataChanged = false;    // any at this time

            return MergeWithSlnFileCore(solutionFilePath);
        }

        private bool MergeWithSlnFileCore(string solutionFilePath)
        {
            VSSolution solution = new VSSolution(solutionFilePath);
            FBModel fbModel = _model.FBModel;
            foreach (VSProject project in solution.MSBuildCompatibleProjects)
            {
                Guid id = project.ProjectGuid;
                string projectName = project.UniqueProjectName.Split('\\', '/').LastOrDefault();
                FBSolutionTarget solutionTarget = fbModel.SolutionTargets.FirstOrDefault(st => st.Id == id);
                if (solutionTarget != null)
                {
                    // update target
                    solutionTarget.MSBuildTarget = projectName;
                }
                else
                {
                    if (null != fbModel.AllTargets.FirstOrDefault(st => st.Id == id))
                        throw new FastBuildGenException("VSProject Guid used by something else - try create a new project.");

                    // create new target
                    solutionTarget = new FBSolutionTarget(id)
                    {
                        Keyword = projectName,
                        MSBuildTarget = projectName,
                        HelpText = projectName,
                        Enabled = false,
                    };
                    fbModel.SolutionTargets.Add(solutionTarget);
                }
            }

            return true;
        }

        private bool SaveFBModelCore()
        {
            try
            {
                string filePath = _model.FilePath;
                if (filePath == null)
                {
                    using (SaveFileDialog dialog = new SaveFileDialog())
                    {
                        dialog.Filter = ConstDialogFilterFBFileExt;
                        DialogResult dresult = dialog.ShowDialog();
                        if (dresult != DialogResult.OK)
                            return false;
                        filePath = dialog.FileName;
                        _model.FilePath = filePath;
                    }
                }
                FBFile.Write(filePath, _model.FBModel);
                _model.ApplicationModel.DataChanged = false;    // it's ok, it's save ^^
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return true;
        }

        private bool SaveAsFBModelCore()
        {
            try
            {
                using (SaveFileDialog dialog = new SaveFileDialog())
                {
                    dialog.Filter = ConstDialogFilterFBFileExt;
                    DialogResult dresult = dialog.ShowDialog();
                    if (dresult != DialogResult.OK)
                        return false;

                    string filePath = dialog.FileName;
                    FBFile.Write(filePath, _model.FBModel);
                    _model.FilePath = filePath;
                    _model.ApplicationModel.DataChanged = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return true;
        }

        private void OpenCore()
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = ConstDialogFilterFBFileExt;
                    DialogResult dresult = dialog.ShowDialog();
                    if (dresult != DialogResult.OK)
                        return;

                    string filePath = dialog.FileName;
                    FBModel fbModel;
                    using (FBFile fbFile = FBFile.Read(filePath))
                    {
                        fbModel = fbFile.XmlConfig.Deserializase();
                    }
                    _model.ApplicationModel.FBModel = fbModel;
                    _model.FilePath = filePath;
                    _model.ApplicationModel.DataChanged = false;
                }
            }
            catch (FBFileException)
            {
                MessageBox.Show("Corrupted file or not a FastBuild file");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion Private
    }
}