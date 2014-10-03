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

        [XmlArray("MacroSolutionTargetAllIds")]
        public Guid[] Xml04MacroSolutionTargetAllIds { get; set; }

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
            Xml04MacroSolutionTargetAllIds = fbModel.MacroSolutionTargets
                .Where(mst => mst.Id == FBModel.ConstGuidAll)   // get 'all' target
                .SelectMany(mst => mst.SolutionTargetIds)
                .ToArray();

            return this;
        }

        internal FBModel Deserializase()
        {
            FBModel result = new FBModel();

            IEnumerable<FBSolutionTarget> solutionTargets = (Xml01SolutionTargets ?? new XmlSolutionTarget[0])
                .Select(xst => xst.Deserialize());
            IEnumerable<FBMacroSolutionTarget> macroSolutionTargets = (Xml02MacroSolutionTargets ?? new XmlMacroSolutionTarget[0])
                .Select(xst => xst.Deserialize());

            result.SolutionTargets.AddRange(solutionTargets);
            result.MacroSolutionTargets.AddRange(macroSolutionTargets);
            result.WithEchoOff = Xml03WithEchoOff;
            FBMacroSolutionTarget macroSolutionTargetAll = result.MacroSolutionTargets
                .FirstOrDefault(mst => mst.Id == FBModel.ConstGuidAll);
            Debug.Assert(macroSolutionTargetAll != null && macroSolutionTargetAll is FBMacroSolutionTargetAll);
            // restore 'all' target
            macroSolutionTargetAll.SolutionTargetIds.Clear();
            macroSolutionTargetAll.SolutionTargetIds.AddRange(Xml04MacroSolutionTargetAllIds ?? new Guid[0]);

            return result;
        }
    }
}