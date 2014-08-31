using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.UI;
using FastBuildGen.Forms.Main;
using FastBuildGen.BusinessModel.Old;

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
            //string solutionPath = @"D:\_Workspaces\HEO\V1\Developpement\ProduitCommercial\ProduitCommercial.sln";
            //Solution solution = new Solution(solutionPath);
            //solution.MSBuildCompatibleProjects
            //    .Select(p => p.ProjectName)
            //    .ToList()
            //    .ForEach(t => Debug.WriteLine(t));

            //return;

            AppDomain.CurrentDomain.AssemblyResolve += (sender, arg) =>
            {
                if (arg.Name.StartsWith("BatchGen"))
                    return Assembly.Load(Properties.Resources.BatchGen);
                return null;
            };

            // create application model
            ApplicationModel applicationModel = new ApplicationModel();

            // Style
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            UIDoubleBufferedModeManager.GlobalDoubleBufferedEx = true;

            // Main form
            MainFormModel mainFormModel = new MainFormModel(applicationModel);
            MainFormController mainFormController = new MainFormController(mainFormModel);
            MainForm mainForm = new MainForm(mainFormModel, mainFormController);

            //Application.Run(new Forms.Form1());
            Application.Run(mainForm);
        }
    }
}