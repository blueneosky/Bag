using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastBuildGen.Xml;
using FastBuildGen.Xml.Entity;

namespace FastBuildGen.BusinessModel.Old
{
    internal class FastBuildImportMergeController
    {
        private readonly IFastBuildModel _fastBuildModel;

        public FastBuildImportMergeController(IFastBuildModel fastBuildModel)
        {
            _fastBuildModel = fastBuildModel;
        }

        public bool ImportWithMerge(string configFilePath)
        {
            bool success = false;

            using (XmlSession xmlSession = new XmlSession())
            {
                XmlFastBuild xmlFastBuild = XmlService.LoadXmlFastBuild(configFilePath);
                xmlSession.Deserialize(xmlFastBuild);
                if (xmlFastBuild != null)
                {
                    success = ImportWithMerge(xmlFastBuild);
                }
                else
                {
                    MessageBox.Show("Import failed.");
                }
            }

            return success;
        }

        private bool ImportWithMerge(XmlFastBuild xmlFastBuild)
        {
            bool withConflict = DetectConflict(xmlFastBuild);

            // Keep your configuration unchanged and add data import without conflict.
            // Replace your configuration in conflict with the data import.
            // Rename conflicts configuration data import.
            // Cancel.

#warning TODO - ALPHA point

            Dictionary<string, string> newModuleNameByImportNames = new Dictionary<string, string> { };
            Dictionary<string, string> newTargetNameByImportNames = new Dictionary<string, string> { };
            // detect conflicts

            //xmlSession.CopyTo(_fastBuildModel, xmlFastBuild);

#warning TODO - ALPHA point
            return false;
        }

        #region Conflict detection

        private bool DetectConflict(XmlFastBuild xmlFastBuild)
        {
            bool withConflict =
                DetectInternalVarsConflict(xmlFastBuild.Xml02FastBuildInternalVar)
                && DetectParamConflict(xmlFastBuild.Xml01FastBuildParam)
                && (_fastBuildModel.WithEchoOff != xmlFastBuild.Xml03WithEchoOff);

            return withConflict;
        }

        private bool DetectInternalVarsConflict(XmlFastBuildInternalVar xmlFastBuildInternalVar)
        {
#warning TODO - ALPHA point
            throw new NotImplementedException();
        }

        private bool DetectModulesConflict(XmlParamDescriptionHeoModule[] xmlParamDescriptionHeoModule)
        {
#warning TODO - ALPHA point
            throw new NotImplementedException();
        }

        private bool DetectParamConflict(XmlFastBuildParam xmlFastBuildParam)
        {
            bool withConflict =
                DetectModulesConflict(xmlFastBuildParam.Xml01Modules)
                && DetectTargetsConflict(xmlFastBuildParam.Xml02Targets);

            return withConflict;
        }

        private bool DetectTargetsConflict(XmlParamDescriptionHeoTarget[] xmlParamDescriptionHeoTarget)
        {
#warning TODO - ALPHA point
            throw new NotImplementedException();
        }

        #endregion Conflict detection
    }
}