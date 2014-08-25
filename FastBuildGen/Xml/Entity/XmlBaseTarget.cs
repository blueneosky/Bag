using System;
using System.Xml.Serialization;
using FastBuildGen.BusinessModel;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    public abstract class XmlBaseTarget
    {
        public XmlBaseTarget()
        {
        }

        public XmlBaseTarget(FBBaseTarget fbBaseTarget)
        {
            Xml01Guid = fbBaseTarget.Id;
            Xml02Keyword = fbBaseTarget.Keyword;
            Xml03Name = fbBaseTarget.Name;
            Xml04HelpText = fbBaseTarget.HelpText;
        }

        [XmlAttribute("Id")]
        public Guid Xml01Guid { get; set; }

        [XmlAttribute("Keyword")]
        public string Xml02Keyword { get; set; }

        [XmlAttribute("Name")]
        public string Xml03Name { get; set; }

        [XmlAttribute("HelpText")]
        public string Xml04HelpText { get; set; }
    }
}