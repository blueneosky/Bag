using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BatchGen.Gen;
using FastBuildGen.BatchNode;
using FastBuildGen.BusinessModel;
using FastBuildGen.BusinessModel.Old;

namespace FastBuildGen.Forms.Main
{
    internal class MainFormController
    {
        private const string ConstDialogFilter = "FastBuild config file (*.fbconf)|*.fbconf";

        private readonly IFastBuildController _fastBuildController;
        private readonly IFastBuildModel _fastBuildModel;
        private readonly MainFormModel _model;

        public MainFormController(MainFormModel model)
        {
            _model = model;

            _fastBuildModel = _model.FastBuildModel;
            _fastBuildController = new FastBuildController(_fastBuildModel);
        }

#warning TODO - re-use it for save

        internal void Deploy()
        {
            return;
            try
            {
                string filePath = null;
                //string filePath = _fastBuildModel.PreferenceModel.DeployFilePath;

                FastBuildBatchFile file = new FastBuildBatchFile(_fastBuildModel);
                string text = BatchGenerator.GetText(file);
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(text);
                }

                MessageBox.Show("Generated at" + Environment.NewLine + filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal bool SaveAsConfigFile()
        {
#warning TODO - check if modele get new file name or not ...
            bool success = false;
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = ConstDialogFilter;
                DialogResult dialogResult = dialog.ShowDialog();
                if (dialogResult != DialogResult.Cancel)
                {
                    string filePath = dialog.FileName;
                    success = ExportConfigFileCore(filePath);
                }
            }

            return success;
        }

        internal bool ImportConfigFile()
        {
            return ImportOrMergeConfigFileCore(false);
        }

        internal bool MergeConfigFile()
        {
            return ImportOrMergeConfigFileCore(true);
        }

        internal void SaveFastBuildData()
        {
            SaveFastBuildDataCore();
        }

        internal bool SaveFastBuildDataBeforeClosing()
        {
            // no change
            if (false == _fastBuildModel.DataChanged)
                return true;

            DialogResult dialogResult = MessageBox.Show(
                "Save your current configuration before quit ?"
                , "Closing"
                , MessageBoxButtons.YesNoCancel
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return false;

            if (dialogResult == DialogResult.Yes)
            {
                SaveFastBuildDataCore();
            }

            return true;
        }

        internal bool SelectInternalVarsEditor()
        {
            _model.ActivePanel = MainFormModel.ConstActivePanelInternalVarsEditor;

            return true;
        }

        internal bool SelectModulesEditor()
        {
            _model.ActivePanel = MainFormModel.ConstActivePanelModulesEditor;

            return true;
        }

        internal bool SelectTargetsEditor()
        {
            _model.ActivePanel = MainFormModel.ConstActivePanelTargetsEditor;

            return true;
        }

        #region Private

        private bool ExportConfigFileCore(string configFilePath)
        {
            bool success = false;

            try
            {
                _fastBuildController.SaveFastBuildConfig(configFilePath);
                success = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error :" + Environment.NewLine + e.Message);
            }

            return success;
        }

        private bool ImportConfigFileCore(string configFilePath)
        {
            bool success = false;

            try
            {
                success = _fastBuildController.LoadFastBuildConfig(configFilePath);
                if (false == success)
                {
                    MessageBox.Show("Import failed.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error :" + Environment.NewLine + e.Message);
            }

            return success;
        }

        private bool ImportOrMergeConfigFileCore(bool withMerge)
        {
            bool success = false;

            DialogResult dialogResult = MessageBox.Show(
                "Export your current configuration before " + (withMerge ? "merge" : "import") + " ?"
                , (withMerge ? "Merge" : "Import") + " process"
                , MessageBoxButtons.YesNoCancel
                , MessageBoxIcon.Question
                , MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return false;

            if (dialogResult == DialogResult.Yes)
            {
                success = SaveAsConfigFile();
                if (false == success)
                    return false;
            }

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = ConstDialogFilter;
                dialogResult = dialog.ShowDialog();
                if (dialogResult != DialogResult.Cancel)
                {
                    string filePath = dialog.FileName;
                    if (withMerge)
                    {
                        success = MergeConfigFileCore(filePath);
                    }
                    else
                    {
                        success = ImportConfigFileCore(filePath);
                    }
                }
            }

            return success;
        }

        private bool MergeConfigFileCore(string configFilePath)
        {
            bool success = new FastBuildImportMergeController(_fastBuildModel).ImportWithMerge(configFilePath);

            return success;
        }

        private void SaveFastBuildDataCore()
        {
            if (false == _model.FastBuildDataChanged)
                return;

#warning TODO - correct this
            //_fastBuildController.SaveDefaultFastBuildConfig();
        }

        #endregion Private
    }
}