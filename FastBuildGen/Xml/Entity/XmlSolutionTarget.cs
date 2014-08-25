using System;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("SolutionTarget")]
    public class XmlSolutionTarget : XmlBaseTarget
    {
        public XmlSolutionTarget()
        {
        }

        public XmlSolutionTarget(FBSolutionTarget fbSolutionTarget)
            : base(fbSolutionTarget)
        {
            Xml05MSBuildTarget = fbSolutionTarget.MSBuildTarget;
            Xml06Enabled = fbSolutionTarget.Enabled;
        }

        [XmlAttribute("MSBuildTarget")]
        public string Xml05MSBuildTarget { get; set; }

        [XmlAttribute("Enable")]
        public bool Xml06Enabled { get; set; }
    }
}