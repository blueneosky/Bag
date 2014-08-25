using System;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("Target")]
    public class XmlTarget : XmlBaseTarget
    {
        public XmlTarget()
        {
        }

        public XmlTarget(FBTarget fbTarget)
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