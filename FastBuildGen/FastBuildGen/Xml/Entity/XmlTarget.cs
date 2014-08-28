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

        [XmlAttribute("Name")]
        public string Xml03Name { get; set; }

        [XmlAttribute("HelpText")]
        public string Xml04HelpText { get; set; }

        internal XmlTarget Serialize(FBTarget fbTarget)
        {
            Xml01Id = fbTarget.Id;
            Xml02Keyword = fbTarget.Keyword;
            Xml03Name = fbTarget.Name;
            Xml04HelpText = fbTarget.HelpText;

            return this;
        }

        internal FBTarget Deserialize(FBTarget fbTarget)
        {
            fbTarget.Keyword = Xml02Keyword;
            fbTarget.Name = Xml03Name;
            fbTarget.HelpText = Xml04HelpText;

            return fbTarget;
        }
    }
}