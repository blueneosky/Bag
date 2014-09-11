using System;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    [XmlType("SolutionTarget")]
    public class XmlSolutionTarget : XmlTarget
    {
        public XmlSolutionTarget()
        {
        }

        [XmlAttribute("MSBuildTarget")]
        public string Xml05MSBuildTarget { get; set; }

        [XmlAttribute("Enable")]
        public bool Xml06Enabled { get; set; }

        internal XmlSolutionTarget Serialize(FBSolutionTarget fbSolutionTarget)
        {
            base.Serialize(fbSolutionTarget);
            Xml05MSBuildTarget = fbSolutionTarget.MSBuildTarget;
            Xml06Enabled = fbSolutionTarget.Enabled;

            return this;
        }

        internal FBSolutionTarget Deserialize()
        {
            FBSolutionTarget result = new FBSolutionTarget(this.Xml01Id, false);
            base.Deserialize(result);
            result.MSBuildTarget = this.Xml05MSBuildTarget;
            result.Enabled = Xml06Enabled;

            return result;
        }
    }
}