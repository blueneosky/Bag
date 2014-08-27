using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ImputationH31per.Data.Xml
{
    [Serializable]
    [XmlRoot("DataRoot")]
    [XmlInclude(typeof(V0_9999.DatDataXml))]
    [XmlInclude(typeof(V1_0a.DatDataXml))]
    [XmlInclude(typeof(V1_0b.DatDataXml))]
    public class DataRootXml
    {
        #region Constantes

        public const string ConstanteVersion0_9999 = "0.9999";
        public const string ConstanteVersion1_0a = "1.0a";
        public const string ConstanteVersion1_0b = "1.0b";

        public const string ConstanteVersionCourante = ConstanteVersion1_0b;

        #endregion


        public DataRootXml(string version, object data)
        {
            Version = version;
            Data = data;
        }

        /// <summary>
        /// constructeur pour la sérialization
        /// </summary>
        protected DataRootXml()
        {

        }
        public string Version { get; set; }

        public object Data { get; set; }

        [XmlIgnore]
        public V0_9999.DatDataXml Data0_9999
        {
            get { return Data as V0_9999.DatDataXml; }
        }

        [XmlIgnore]
        public V1_0a.DatDataXml Data1_0a
        {
            get { return Data as V1_0a.DatDataXml; }
        }

        [XmlIgnore]
        public V1_0b.DatDataXml Data1_0b
        {
            get { return Data as V1_0b.DatDataXml; }
        }

    }
}
