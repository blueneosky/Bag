using System;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;
using System.Collections.Generic;
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
            Xml01SolutionTargets = fbModel.SolutionTargets.Values
               .Select(t => new XmlSolutionTarget().Serialize(t))
               .ToArray();
            Xml02MacroSolutionTargets = fbModel.MacroSolutionTargets.Values
                .Select(mt => new XmlMacroSolutionTarget().Serialize(mt))
                .ToArray();
            Xml03WithEchoOff = fbModel.WithEchoOff;

            return this;
        }

        internal FBModel Deserializase(XmlFastBuild xmlFastBuild)
        {
            FBModel result= new FBModel();

            IEnumerable<FBSolutionTarget> solutionTargets = xmlFastBuild.Xml01SolutionTargets
                .Select(xst => xst.Deserialize());
            IEnumerable<FBMacroSolutionTarget> macroSolutionTargets = xmlFastBuild.Xml02MacroSolutionTargets
                .Select(xst => xst.Deserialize());

            result.SolutionTargets.AddRange(solutionTargets, st => st.Id);
            result.MacroSolutionTargets.AddRange(macroSolutionTargets, mst => mst.Id);

            return result;
        }
    }
}