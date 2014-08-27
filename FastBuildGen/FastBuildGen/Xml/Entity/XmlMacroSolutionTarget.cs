using System;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;
using System.Collections.Generic;
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

        internal static XmlMacroSolutionTarget Serialize(FBMacroSolutionTarget fbMacroSolutionTarget)
        {
            XmlMacroSolutionTarget result = XmlTarget.Serialize<XmlMacroSolutionTarget>(fbMacroSolutionTarget);
            result.Xml05SolutionTargetIds = fbMacroSolutionTarget.SolutionTargetIds.ToArray();

            return result;
        }

        internal static FBMacroSolutionTarget Deserialize(XmlMacroSolutionTarget xmlMacroSolutionTarget, FBModel fbModel)
        {
            FBMacroSolutionTarget result = new FBMacroSolutionTarget(xmlMacroSolutionTarget.Xml01Id);
            XmlTarget.Deserialize(xmlMacroSolutionTarget, result);
            IEnumerable<Guid> idValides = xmlMacroSolutionTarget.Xml05SolutionTargetIds
                .Join(
                    fbModel.SolutionTargets.Keys
                    , id => id
                    , id => id
                    , (id, id2) => id)
                ;
            result.SolutionTargetIds.AddRange(idValides);

            return result;
        }
    }
}