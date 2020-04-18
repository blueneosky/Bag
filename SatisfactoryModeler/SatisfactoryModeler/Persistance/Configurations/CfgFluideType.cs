using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SatisfactoryModeler.Persistance.Configurations
{
    [XmlType("FluideType")]
    public class CfgFluideType
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("Lbl")]
        public string Label { get; set; }
    }
}
