using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("SolutionTarget")]
    public class XmlSolutionTarget : XmlTarget
    {
        public XmlSolutionTarget()
        {
        }

        [XmlAttribute("MSBuildTarget")]
        public string Xml05MSBuildTarget { get; set; }

        [XmlAttribute("Enable")]
        public bool Xml06Enabled { get; set; }

        internal static XmlSolutionTarget Serialize(FBSolutionTarget fbSolutionTarget)
        {
            XmlSolutionTarget result = XmlTarget.Serialize<XmlSolutionTarget>(fbSolutionTarget);
            result.Xml05MSBuildTarget = fbSolutionTarget.MSBuildTarget;
            result.Xml06Enabled = fbSolutionTarget.Enabled;

            return result;
        }

        internal static FBSolutionTarget Deserialize(XmlSolutionTarget xmlSolutionTarget)
        {
            FBSolutionTarget result = new FBSolutionTarget(xmlSolutionTarget.Xml01Id);
            XmlTarget.Deserialize(xmlSolutionTarget, result);
            result.MSBuildTarget = xmlSolutionTarget.Xml05MSBuildTarget;
            result.Enabled = xmlSolutionTarget.Xml06Enabled;

            return result;
        }
    }
}