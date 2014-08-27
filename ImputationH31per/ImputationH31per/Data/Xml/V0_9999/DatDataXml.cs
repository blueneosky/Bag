using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;

namespace ImputationH31per.Data.Xml.V0_9999
{
    [Serializable]
    public class DatDataXml
    {
        // Neutralisé
        //public DatDataXml(IDatData<IDatTacheTfs, IDatIHFormParametre> data)
        //{
        //    if (data.TacheTfss != null)
        //    {
        //        ArrayTacheTfss = data.TacheTfss
        //            .Select(i => new DatTacheTfsXml(i))
        //            .ToArray();
        //    }
        //    if (data.IHFormParametres != null)
        //    {
        //        ArrayIHFormParametres = data.IHFormParametres
        //            .Select(i => new DatIHFormParametreXml(i))
        //            .ToArray();
        //    }
        //}

        /// <summary>
        /// costructeur pour la sérialization
        /// </summary>
        private DatDataXml()
        {
        }

        public DatIHFormParametreXml[] ArrayIHFormParametres { get; set; }

        public DatTacheTfsXml[] ArrayTacheTfss { get; set; }

        [XmlIgnore]
        public IEnumerable<DatIHFormParametreXml> IHFormParametres
        {
            get
            {
                IEnumerable<DatIHFormParametreXml> resultat = (ArrayIHFormParametres ?? new DatIHFormParametreXml[0]);

                return resultat;
            }
        }

        [XmlIgnore]
        public IEnumerable<DatTacheTfsXml> TacheTfss
        {
            get
            {
                IEnumerable<DatTacheTfsXml> resultat = (ArrayTacheTfss ?? new DatTacheTfsXml[0]);

                return resultat;
            }
        }
    }
}