using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace RegexpConv
{
    [Serializable]
    public class Settings
    {
        #region Singleton

        private static Settings _default = new Settings();

        public static Settings Default
        {
            get { return _default; }
        }

        #endregion Singleton

        #region Load/Save

        private const string ConstSettingsFileName = ".regexpconv";

        private static XmlSerializer CreateFormatsXmlSerializer()
        {
            return new XmlSerializer(typeof(Settings));
        }

        private static string GetSettingsPath()
        {
            string userProfileFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string settingsPath = Path.Combine(userProfileFolderPath, ConstSettingsFileName);

            return settingsPath;
        }

        public static void Load()
        {
            string settingsPath = GetSettingsPath();
            using (FileStream settingsFileStream = File.OpenRead(settingsPath))
            {
                XmlSerializer xmlSerializer = CreateFormatsXmlSerializer();
                object data = xmlSerializer.Deserialize(settingsFileStream);
                _default = (data as Settings) ?? new Settings();
            }
        }

        public static void Save()
        {
            string settingsPath = GetSettingsPath();
            using (FileStream settingsFileStream = File.Create(settingsPath))
            {
                XmlSerializer xmlSerializer = CreateFormatsXmlSerializer();
                xmlSerializer.Serialize(settingsFileStream, Default);
            }
        }

        #endregion Load/Save

        /// <summary>
        /// Réserved for Serializable features
        /// </summary>
        public Settings()
        {
        }

        public Couple[] XmlFormats { get; set; }

        [XmlIgnore]
        public Couple[] Formats
        {
            get { return XmlFormats ?? new Couple[0]; }
            set { XmlFormats = value; }
        }
    }
}