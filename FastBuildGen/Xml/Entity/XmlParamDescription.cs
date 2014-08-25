using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    public abstract class XmlParamDescription<TInstance> : XmlObjectId<TInstance>
        where TInstance : class, FastBuildGen.BusinessModel.Old.IParamDescription
    {
        public XmlParamDescription()
        {
        }

        [XmlIgnore]
        public string HelpText
        {
            get { return Xml03HelpText; }
        }

        [XmlIgnore]
        public string Keyword
        {
            get { return Xml01Keyword; }
        }

        [XmlIgnore]
        public string Name
        {
            get { return Xml02Name; }
        }

        [XmlAttribute("Keyword")]
        public string Xml01Keyword { get; set; }

        [XmlAttribute("Name")]
        public string Xml02Name { get; set; }

        [XmlAttribute("HelpText")]
        public string Xml03HelpText { get; set; }

        internal bool Equals(BusinessModel.Old.IParamDescription paramDescription)
        {
            bool result = (paramDescription != null)
                && paramDescription.Keyword == Keyword
                && paramDescription.Name == Name
                && paramDescription.HelpText == HelpText;

            return result;
        }

        protected override void CopyToCore(TInstance instance)
        {
            instance.Keyword = Keyword;
            instance.Name = Name;
            instance.HelpText = HelpText;
        }

        protected override void DeserializeCore()
        {
            // nothing
        }

        protected override void SerializeCore(TInstance instance)
        {
            Xml01Keyword = instance.Keyword;
            Xml02Name = instance.Name;
            Xml03HelpText = instance.HelpText;
        }
    }
}