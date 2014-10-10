using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Loop.Forms;
using Loop.Forms.Model;

namespace Loop
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            MainFormModel mainFormModel = new MainFormModel();
            MainFormController mainFormController = new MainFormController(mainFormModel);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(mainFormModel, mainFormController));
        }
    }
}