using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FastBuildGen.Xml.Entity
{
    [Serializable]
    public class XmlFastBuildInternalVar : XmlObjectId<FastBuildGen.BusinessModel.Old.IFastBuildInternalVarModel>
    {
        public XmlFastBuildInternalVar()
        {
        }

        [XmlIgnore]
        public IDictionary<string, string> Properties
        {
            get { return XmlProperties.Entries; }
        }

        [XmlElement("Properties")]
        public XmlStringDictionary XmlProperties { get; set; }

        internal bool Equals(BusinessModel.Old.IFastBuildInternalVarModel model)
        {
            IDictionary<string, string> properties = Properties;    // execute more than once

            bool result = (model != null)
                 && (properties.Count() == model.Properties.Count());

            result = result && model.Properties
                .GroupJoin(
                    properties
                    , kvp => kvp.Key
                    , xmlKvp => xmlKvp.Key
                    , (kvp, xmlKvps) => (xmlKvps.Count() == 1) && (xmlKvps.First().Value == kvp.Value)
                )
                .All(s => s);

            return result;
        }

        protected override void CopyToCore(BusinessModel.Old.IFastBuildInternalVarModel instance)
        {
            instance.ResetToDefault();

            //foreach (KeyValuePair<string, string> kvp in Properties)
            foreach (XmlStringDictionary.XmlStringDictionaryEntry kvp in (XmlProperties.XmlEntries ?? new FastBuildGen.Xml.XmlStringDictionary.XmlStringDictionaryEntry[0]))  // optimisation
            {
                if (instance.ContainsPropertyName(kvp.Key))
                    instance[kvp.Key] = kvp.Value;
            }
        }

        protected override void DeserializeCore()
        {
            // nothing
        }

        protected override void SerializeCore(BusinessModel.Old.IFastBuildInternalVarModel instance)
        {
            Dictionary<string, string> properties = new Dictionary<string, string> { };
            IDictionary<string, string> defaultProperties = instance.DefaultProperties;
            foreach (KeyValuePair<string, string> property in instance.Properties)
            {
                string name = property.Key;
                string value = property.Value;
                string defaultValue = null;
                bool success = defaultProperties.TryGetValue(name, out defaultValue);
                if (success && (value != defaultValue))
                    properties[name] = value;
            }

            XmlProperties = new XmlStringDictionary(properties);
        }
    }
}