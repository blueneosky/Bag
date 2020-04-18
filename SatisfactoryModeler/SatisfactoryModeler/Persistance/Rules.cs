using SatisfactoryModeler.Models;
using SatisfactoryModeler.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SatisfactoryModeler.Persistance
{
    public class Rules
    {
        public static Rules Instance { get; } = ExtractRules();

        private static Rules ExtractRules()
            => PersistanceFactory.Instance.Get<Rules>("XML").Restore(new MemoryStream(Encoding.UTF8.GetBytes(Resources.Rules)));

        public Rules() { }

        //public ItemType[] ItemTypes { get; }
        public FluideType[] FluideTypes { get; set; }
    }
}
