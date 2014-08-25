using System;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("MacroSolutionTarget")]
    public class XmlMacroSolutionTarget : XmlBaseTarget
    {
        public XmlMacroSolutionTarget()
        {
        }

        public XmlMacroSolutionTarget(FBMacroSolutionTarget fbMacroSolutionTarget)
            : base(fbMacroSolutionTarget)
        {
            Xml05SolutionTargetIds = fbMacroSolutionTarget.SolutionTargetIds.ToArray();
        }

        [XmlElement("SolutionTargetId")]
        public Guid[] Xml05SolutionTargetIds { get; set; }
    }
}