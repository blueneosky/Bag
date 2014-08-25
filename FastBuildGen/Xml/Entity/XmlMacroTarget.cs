using System;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("MacroTarget")]
    public class XmlMacroTarget : XmlBaseTarget
    {
        public XmlMacroTarget()
        {
        }

        public XmlMacroTarget(FBMacroTarget fbMacroTarget)
            : base(fbMacroTarget)
        {
            Xml05TargetIds = fbMacroTarget.TargetIds.ToArray();
        }

        [XmlElement("TargetId")]
        public Guid[] Xml05TargetIds { get; set; }
    }
}