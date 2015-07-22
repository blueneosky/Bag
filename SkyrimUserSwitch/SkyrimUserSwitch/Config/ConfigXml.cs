using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SkyrimUserSwitch.Config
{
    [Serializable]
    [XmlRoot("Config")]
    public class ConfigXml
    {
        public ConfigXml()
        {
        }

        public ConfigXml(Model.SkusModel model)
        {
            SkyrimUserFolder = model.SkyrimUserFolder;
            SkyrimFolder = model.SkyrimFolder;
            SkyrimLauncherPath = model.SkyrimLauncherPath;
        }

        [XmlElement("SkyrimFolder")]
        public string SkyrimFolder { get; set; }

        [XmlElement("SkyrimLauncherPath")]
        public string SkyrimLauncherPath { get; set; }

        [XmlElement("SkyrimUserFolder")]
        public string SkyrimUserFolder { get; set; }
    }
}