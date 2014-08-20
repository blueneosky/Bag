using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.UI;
using FastBuildGen.Common.UndoRedo;
using FastBuildGen.Forms.Main;
using FastBuildGen.Common;
using System.Diagnostics;
using System.Text;
using FastBuildGen.VisualStudio;

namespace FastBuildGen
{
#warning TODO - do not use a default config => load/save/import into ...
#warning TODO - import from .sln
#warning TODO - ALPHA BETA - Undo/Redo

    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            string solutionPath = @"D:\_Workspaces\HEO\V1\Developpement\ProduitCommercial\ProduitCommercial.sln";
            Solution solution = new Solution(solutionPath);

            return;

            AppDomain.CurrentDomain.AssemblyResolve += (sender, arg) =>
            {
                if (arg.Name.StartsWith("BatchGen"))
                    return Assembly.Load(Properties.Resources.BatchGen);
                return null;
            };

            // create business model
            IFastBuildModel model = new FastBuildModel();
            IFastBuildController controller = new FastBuildController(model);
            model.Initialize();

            // Style
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UIDoubleBufferedModeManager.GlobalDoubleBufferedEx = true;

            // Main form
            IUndoRedoManager undoRedoManager = new UndoRedoManager();
            MainFormModel mainFormModel = new MainFormModel(model, undoRedoManager);
            MainFormController mainFormController = new MainFormController(mainFormModel);
            MainForm mainForm = new MainForm(mainFormModel, mainFormController);

            //Application.Run(new Forms.Form1());
            Application.Run(mainForm);
        }
    }
}