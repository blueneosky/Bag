using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;
using FastBuildGen.Common;

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

        internal XmlFastBuild Serialize(FBModel fbModel)
        {
            Xml01SolutionTargets = fbModel.SolutionTargets
               .Select(t => new XmlSolutionTarget().Serialize(t))
               .ToArray();
            Xml02MacroSolutionTargets = fbModel.MacroSolutionTargets
                .Select(mt => new XmlMacroSolutionTarget().Serialize(mt))
                .ToArray();
            Xml03WithEchoOff = fbModel.WithEchoOff;

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

            return result;
        }
    }
}