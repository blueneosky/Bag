using System;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

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
            FBMacroSolutionTarget result = new FBMacroSolutionTarget(this.Xml01Id);
            base.Deserialize(result);
            foreach (Guid solutionTargetId in this.Xml05SolutionTargetIds)
            {
                result.SolutionTargetIds.Add(solutionTargetId);
            }

            return result;
        }
    }
}