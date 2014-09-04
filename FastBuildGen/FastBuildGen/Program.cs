using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common.UI;
using FastBuildGen.Forms.Main;

namespace FastBuildGen
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
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