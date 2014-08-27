using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;

namespace ImputationH31per.Data.Xml.V1_0a
{
    [Serializable]
    [XmlType("aData")]
    public class DatDataXml
    {
        /// <summary>
        /// costructeur pour la sérialization
        /// </summary>
        private DatDataXml()
        {
        }

        public DatDataXml(V0_9999.DatDataXml oldData)
        {
            if (oldData.TacheTfss != null)
            {
                XmlTacheTfss = oldData.TacheTfss
                    .Select(i => new DatTacheTfsXml(i))
                    .ToArray();
            }
            if (oldData.IHFormParametres != null)
            {
                XmlIHFormParametres = oldData.IHFormParametres
                    .Select(i => new DatIHFormParametreXml(i))
                    .ToArray();
            }
        }

        #region Xml

        [XmlArray("FP")]
        public DatIHFormParametreXml[] XmlIHFormParametres { get; set; }

        [XmlArray("Tfs")]
        public DatTacheTfsXml[] XmlTacheTfss { get; set; }

        #endregion Xml

        #region Propriétés non sérialisés

        [XmlIgnore]
        public IEnumerable<DatIHFormParametreXml> IHFormParametres
        {
            get
            {
                IEnumerable<DatIHFormParametreXml> resultat = (XmlIHFormParametres ?? new DatIHFormParametreXml[0]);

                return resultat;
            }
        }

        [XmlIgnore]
        public IEnumerable<DatTacheTfsXml> TacheTfss
        {
            get
            {
                IEnumerable<DatTacheTfsXml> resultat = (XmlTacheTfss ?? new DatTacheTfsXml[0]);

                return resultat;
            }
        }

        #endregion Propriétés non sérialisés
    }
}