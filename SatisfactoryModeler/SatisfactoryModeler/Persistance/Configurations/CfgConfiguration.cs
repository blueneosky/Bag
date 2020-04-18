using System.Xml.Serialization;

namespace SatisfactoryModeler.Persistance.Configurations
{
    [XmlType("Configuration")]
    public class CfgConfiguration
    {
        [XmlArray("ItemTypes")]
        public CfgItemType[] ItemTypes { get; set; }

        [XmlArray("FluideTypes")]
        public CfgFluideType[] FluideTypes { get; set; }
    }
}
