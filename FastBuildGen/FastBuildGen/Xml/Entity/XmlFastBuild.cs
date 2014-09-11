using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;
using System.Diagnostics;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("FastBuild")]
    public class XmlFastBuild
    {
        public XmlFastBuild()
        {
        }

        [XmlArray("SolutionTargets")]
        public XmlSolutionTarget[] Xml01SolutionTargets { get; set; }

        [XmlArray("MacroSolutionTargets")]
        public XmlMacroSolutionTarget[] Xml02MacroSolutionTargets { get; set; }

        [XmlElement("WithEchoOff")]
        public bool Xml03WithEchoOff { get; set; }

        [XmlElement("MacroAllSolutionTargetId")]
        public Guid[] Xml04MacroAllSolutionTargetIds { get; set; }

        internal XmlFastBuild Serialize(FBModel fbModel)
        {
            Xml01SolutionTargets = fbModel.SolutionTargets
               .Select(t => new XmlSolutionTarget().Serialize(t))
               .ToArray();
            Xml02MacroSolutionTargets = fbModel.MacroSolutionTargets
                .Where(mst => mst.Id != FBModel.ConstGuidAll)   // exclude 'all' target
                .Select(mst => new XmlMacroSolutionTarget().Serialize(mst))
                .ToArray();
            Xml03WithEchoOff = fbModel.WithEchoOff;
            Xml04MacroAllSolutionTargetIds = fbModel.MacroSolutionTargets
                .Where(mst => mst.Id == FBModel.ConstGuidAll)   // get 'all' target
                .SelectMany(mst => mst.SolutionTargetIds)
                .ToArray();

            return this;
        }

        internal FBModel Deserializase()
        {
            FBModel result = new FBModel();

            IEnumerable<FBSolutionTarget> solutionTargets = Xml01SolutionTargets
                .Select(xst => xst.Deserialize());
            IEnumerable<FBMacroSolutionTarget> macroSolutionTargets = Xml02MacroSolutionTargets
                .Select(xst => xst.Deserialize());

            result.SolutionTargets.AddRange(solutionTargets);
            result.MacroSolutionTargets.AddRange(macroSolutionTargets);
            result.WithEchoOff = Xml03WithEchoOff;
            FBMacroSolutionTarget macroAllSolutionTarget = result.MacroSolutionTargets
                .FirstOrDefault(mst => mst.Id == FBModel.ConstGuidAll);
            Debug.Assert(macroAllSolutionTarget != null && macroAllSolutionTarget is FBMacroAllSolutionTarget);
            // restore 'all' target
            macroAllSolutionTarget.SolutionTargetIds.Clear();
            macroAllSolutionTarget.SolutionTargetIds.AddRange(Xml04MacroAllSolutionTargetIds);

            return result;
        }
    }
}