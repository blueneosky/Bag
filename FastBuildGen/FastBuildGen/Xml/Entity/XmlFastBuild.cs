using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        [XmlElement("Properties")]
        public XmlStringDictionary Xml03Properties { get; set; }

        [XmlElement("WithEchoOff")]
        public bool Xml04WithEchoOff { get; set; }

        internal static XmlFastBuild Serialize(FBModel fbModel)
        {
            XmlFastBuild result = new XmlFastBuild();
            result.Xml01SolutionTargets = fbModel.SolutionTargets.Values
                .Select(t => XmlSolutionTarget.Serialize(t))
                .ToArray();
            result.Xml02MacroSolutionTargets = fbModel.MacroSolutionTargets.Values
                .Select(mt => XmlMacroSolutionTarget.Serialize(mt))
                .ToArray();
#warning TODO ALPHA BETA point - ne sérializer que les prop changé
            result.Xml03Properties = new XmlStringDictionary(fbModel.InternalVars);
            result.Xml04WithEchoOff = fbModel.WithEchoOff;

            return result;
        }

        internal static FBModel Deserialize(XmlFastBuild xmlFastBuild)
        {
            FBModel result = new FBModel();
            xmlFastBuild.Xml01SolutionTargets
                .Select(xst => XmlSolutionTarget.Deserialize(xst))
                .ForEach(st => result.SolutionTargets[st.Id] = st);
            // do Xml02MacroSolutionTargets after Xml01SolutionTargets
            xmlFastBuild.Xml02MacroSolutionTargets
                .Select(xmst => XmlMacroSolutionTarget.Deserialize(xmst, result))
                .ForEach(mst => result.MacroSolutionTargets[mst.Id] = mst);
            xmlFastBuild.Xml03Properties.Entries
                .ForEach(kvp => result.InternalVars[kvp.Key] = kvp.Value);
            result.WithEchoOff = xmlFastBuild.Xml04WithEchoOff;

            return result;
        }
    }
}