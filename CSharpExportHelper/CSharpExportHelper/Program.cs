using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CSharpExportHelper
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main(string[] arguments)
        {
            if (arguments.Length == 0)
            {
                MessageBox.Show("Veuillez exécuter le programme avec un dossier de projet/solution ou une archive");
                return;
            }

            AppDomain.CurrentDomain.AssemblyResolve += (sender, arg) =>
            {
                if (arg.Name.StartsWith("Ionic.Zip.Reduced"))
                    return Assembly.Load(Properties.Resources.Ionic_Zip_Reduced);
                if (arg.Name.StartsWith("ProgressLib"))
                    return Assembly.Load(Properties.Resources.ProgressLib);
                return null;
            };

            Modele modele = new Modele(arguments);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(modele));
        }
    }
}