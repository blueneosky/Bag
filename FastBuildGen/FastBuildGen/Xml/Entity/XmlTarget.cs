using System;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    public abstract class XmlTarget
    {
        protected XmlTarget()
        {
        }

        [XmlAttribute("Id")]
        public Guid Xml01Id { get; set; }

        [XmlAttribute("Keyword")]
        public string Xml02Keyword { get; set; }

        [XmlAttribute("HelpText")]
        public string Xml03HelpText { get; set; }

        internal XmlTarget Serialize(FBTarget fbTarget)
        {
            Xml01Id = fbTarget.Id;
            Xml02Keyword = fbTarget.Keyword;
            Xml03HelpText = fbTarget.HelpText;

            return this;
        }

        internal FBTarget Deserialize(FBTarget fbTarget)
        {
            fbTarget.Keyword = Xml02Keyword;
            fbTarget.HelpText = Xml03HelpText;

            return fbTarget;
        }
    }
}