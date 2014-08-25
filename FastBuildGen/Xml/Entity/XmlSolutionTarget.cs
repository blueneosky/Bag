using System;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("Target")]
    public class XmlSolutionTarget : XmlBaseTarget
    {
        public XmlSolutionTarget()
        {
        }

        public XmlSolutionTarget(FBSolutionTarget fbTarget)
            : base(fbTarget)
        {
            Xml05MSBuildTarget = fbTarget.MSBuildTarget;
            Xml06Enabled = fbTarget.Enabled;
        }

        [XmlAttribute("MSBuildTarget")]
        public string Xml05MSBuildTarget { get; set; }

        [XmlAttribute("Enable")]
        public bool Xml06Enabled { get; set; }
    }
}