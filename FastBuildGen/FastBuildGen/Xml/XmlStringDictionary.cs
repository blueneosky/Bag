using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FastBuildGen.Xml
{
    [Serializable]
    [XmlType("StrDictionary")]
    public class XmlStringDictionary
    {
        public XmlStringDictionary()
        {
        }

        public XmlStringDictionary(IDictionary<string, string> dictionary)
        {
            XmlEntries = dictionary
                .Select(kvp => new XmlStringDictionaryEntry(kvp))
                .ToArray();
        }

        [XmlIgnore]
        public IDictionary<string, string> Entries
        {
            get { return (XmlEntries ?? new XmlStringDictionaryEntry[0]).ToDictionary(e => e.Key, e => e.Value); }
        }

        [XmlElement("Entries")]
        public XmlStringDictionaryEntry[] XmlEntries { get; set; }

        #region XmlDictionaryEntry

        [Serializable]
        [XmlType("Entry")]
        public class XmlStringDictionaryEntry
        {
            public XmlStringDictionaryEntry(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public XmlStringDictionaryEntry()
            {
            }

            public XmlStringDictionaryEntry(KeyValuePair<string, string> kvp)
            {
                Key = kvp.Key;
                Value = kvp.Value;
            }

            [XmlAttribute("Key")]
            public string Key { get; set; }

            [XmlAttribute("Value")]
            public string Value { get; set; }
        }

        #endregion XmlDictionaryEntry
    }
}