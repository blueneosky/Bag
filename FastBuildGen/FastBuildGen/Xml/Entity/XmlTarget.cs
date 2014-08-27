using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        internal static TXmlTarget Serialize<TXmlTarget>(FBTarget fbTarget)
            where TXmlTarget : XmlTarget, new()
        {
            TXmlTarget result = new TXmlTarget();
            result.Xml01Id = fbTarget.Id;
            result.Xml02Keyword = fbTarget.Keyword;
            result.Xml03Name = fbTarget.Name;
            result.Xml04HelpText = fbTarget.HelpText;

            return result;
        }

        internal static FBTarget Deserialize(XmlTarget xmlTarget, FBTarget fbTarget)
        {
            fbTarget.Keyword =  xmlTarget.Xml02Keyword;
            fbTarget.Name =     xmlTarget.Xml03Name;
            fbTarget.HelpText = xmlTarget.Xml04HelpText;

            return fbTarget;
        }
    }
}