using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SkyrimUserSwitch.Config;
using SkyrimUserSwitch.Forms;
using SkyrimUserSwitch.Model;
using SkyrimUserSwitch.Properties;

namespace SkyrimUserSwitch
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            string configFileName = Resources.ConfigFileName;
            string configFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), configFileName);
            ConfigXml configXml;
            XmlService.TryLoad<ConfigXml>(configFilePath, out configXml);

            SkusModel model = new SkusModel();
            model.SetConfig(configXml);
            //model.LoadUsersAndSaves();
            //model.CheckSkyrimFolder();
            model.RunSkyrimLauncher();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(model));

            model.SetApplicationClose();

            if (model.IsConfigModified)
            {
                configXml = new ConfigXml(model);
                XmlService.TrySave(configFilePath, configXml);
            }
        }
    }
}