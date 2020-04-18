using System.Xml.Serialization;

namespace SatisfactoryModeler.Persistance.Configurations
{
    [XmlType("ItemType")]
    public class CfgItemType
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("Lbl")]
        public string Label { get; set; }

        [XmlAttribute("SS")]
        public int StackSize { get; set; }

        [XmlAttribute("SV")]
        public int SinkValue { get; set; }
    }
}
