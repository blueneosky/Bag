using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FastBuildGen.Xml;
using FastBuildGen.Xml.Entity;

namespace FastBuildGen.BusinessModel
{
    internal class FastBuildController : IFastBuildController
    {
        #region Const

        private static string ConstDefaultPreferenceFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".fastbuildgen");

        #endregion Const

        #region Memebers

        private readonly IFastBuildModel _model;

        #endregion Memebers

        #region Ctor

        public FastBuildController(IFastBuildModel model)
        {
            _model = model;
        }

        #endregion Ctor

        #region IFastBuildController

        public bool LoadFastBuildConfig(string configFilePath)
        {
            bool success = false;

            using (XmlSession xmlSession = new XmlSession())
            {
                XmlFastBuild xmlFastBuild = DeserializeFastBuildConfig(configFilePath, xmlSession);
                if (xmlFastBuild != null)
                {
                    xmlSession.CopyTo(_model, xmlFastBuild);
                    _model.ResetDataChanged();
                    success = true;
                }
            }

            return success;
        }

        public void SaveFastBuildConfig(string configFilePath)
        {
            XmlService.Save(configFilePath, _model);
#warning TODO - need to be checked
            _model.ResetDataChanged();
        }

        #endregion IFastBuildController

        #region Private

        private string DefaultPreferenceFilePath
        {
            get { return ConstDefaultPreferenceFilePath; }
        }

        private XmlFastBuild DeserializeFastBuildConfig(string configFilePath, XmlSession xmlSession = null)
        {
            XmlFastBuild xmlFastBuild = null;
            if (xmlSession == null)
            {
                using (xmlSession = new XmlSession())
                {
                    DeserializeFastBuildConfig(configFilePath, xmlSession);
                }
            }
            else
            {
                xmlFastBuild = XmlService.LoadXmlFastBuild(configFilePath);
                xmlSession.Deserialize(xmlFastBuild);
            }

            return xmlFastBuild;
        }

        #endregion Private
    }
}