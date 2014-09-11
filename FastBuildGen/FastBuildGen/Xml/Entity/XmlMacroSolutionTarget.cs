using System;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("MacroSolutionTarget")]
    public class XmlMacroSolutionTarget : XmlTarget
    {
        public XmlMacroSolutionTarget()
        {
        }

        [XmlElement("SolutionTargetId")]
        public Guid[] Xml05SolutionTargetIds { get; set; }

        internal XmlMacroSolutionTarget Serialize(FBMacroSolutionTarget fbMacroSolutionTarget)
        {
            base.Serialize(fbMacroSolutionTarget);
            Xml05SolutionTargetIds = fbMacroSolutionTarget.SolutionTargetIds.ToArray();

            return this;
        }

        internal FBMacroSolutionTarget Deserialize()
        {
            FBMacroSolutionTarget result = new FBMacroSolutionTarget(this.Xml01Id, false);
            base.Deserialize(result);
            result.SolutionTargetIds.AddRange(this.Xml05SolutionTargetIds);

            return result;
        }
    }
}