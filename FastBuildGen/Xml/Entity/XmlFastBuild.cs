using System;
using System.Linq;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("FastBuild")]
    public class XmlFastBuild
    {
        public XmlFastBuild()
        {
        }

        public XmlFastBuild(FBModel fbModel)
        {
            Xml01Targets = fbModel.SolutionTargets.Values
                .Select(t => new XmlSolutionTarget(t))
                .ToArray();
            Xml02MacroSolutionTargets = fbModel.MacroSolutionTargets.Values
                .Select(mt => new XmlMacroSolutionTarget(mt))
                .ToArray();
#warning TODO ALPHA BETA point - ne sérializer que les prop changé
            Xml03Properties = new XmlStringDictionary(fbModel.InternalVars);
            Xml04WithEchoOff = fbModel.WithEchoOff;
        }

        [XmlArray("SolutionTargets")]
        public XmlSolutionTarget[] Xml01Targets { get; set; }

        [XmlArray("MacroSolutionTargets")]
        public XmlMacroSolutionTarget[] Xml02MacroSolutionTargets { get; set; }

        [XmlElement("Properties")]
        public XmlStringDictionary Xml03Properties { get; set; }

        [XmlElement("WithEchoOff")]
        public bool Xml04WithEchoOff { get; set; }
    }
}