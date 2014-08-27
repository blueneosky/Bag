using System;
using System.IO;
using FastBuildGen.Xml;
using FastBuildGen.Xml.Entity;

namespace FastBuildGen.BusinessModel.Old
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

#warning TODO BETA point - cleaning
            //XmlFastBuild xmlFastBuild = DeserializeFastBuildConfig(configFilePath);
            //if (xmlFastBuild != null)
            //{
            //    xmlSession.CopyTo(_model, xmlFastBuild);
            //    _model.ResetDataChanged();
            //    success = true;
            //}

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

        private XmlFastBuild DeserializeFastBuildConfig(string configFilePath)
        {
            XmlFastBuild xmlFastBuild = XmlService.LoadXmlFastBuild(configFilePath);

            return xmlFastBuild;
        }

        #endregion Private
    }
}