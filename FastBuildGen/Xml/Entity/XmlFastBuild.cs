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
            Xml01Targets = fbModel.Targets.Values
                .Select(t => new XmlSolutionTarget(t))
                .ToArray();
            Xml02MacroTargets = fbModel.MacroTargets.Values
                .Select(mt => new XmlMacroSolutionTarget(mt))
                .ToArray();
            Xml03Properties = new XmlStringDictionary(fbModel.InternalVars);
            Xml04WithEchoOff = fbModel.WithEchoOff;
        }

        [XmlArray("Targets")]
        public XmlSolutionTarget[] Xml01Targets { get; set; }

        [XmlArray("MacroTargets")]
        public XmlMacroSolutionTarget[] Xml02MacroTargets { get; set; }

        [XmlElement("Properties")]
        public XmlStringDictionary Xml03Properties { get; set; }

        [XmlElement("WithEchoOff")]
        public bool Xml04WithEchoOff { get; set; }
    }
}