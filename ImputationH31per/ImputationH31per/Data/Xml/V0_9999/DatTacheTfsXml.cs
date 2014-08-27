using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Data.Xml.V0_9999
{
    [Serializable]
    public class DatTacheTfsXml
    {
        // Neutralisé
        //public DatTacheTfsXml(IDatTacheTfs tacheTfs)
        //{
        //    this.Numero = tacheTfs.Numero;

        //    this.DefinirProprietesTacheTfs(tacheTfs);

        //    ArrayTicketTfss = tacheTfs.TicketTfss
        //        .Select(i => new DatTicketTfsXml(i, this))
        //        .ToArray();
        //}

        /// <summary>
        /// Pour serialization
        /// </summary>
        private DatTacheTfsXml()
        {
        }

        #region Propriétés scalaire

        public string Nom { get; set; }

        public string NomGroupement { get; set; }

        [XmlAttribute("Num")]
        public int Numero { get; set; }

        #endregion Propriétés scalaire

        #region Propriétés non scalaire

        public DatTicketTfsXml[] ArrayTicketTfss { get; set; }

        #endregion Propriétés non scalaire

        #region Propriétés non sérialisés

        [XmlIgnore]
        public IEnumerable<DatTicketTfsXml> TicketTfss
        {
            get { return ArrayTicketTfss; }
        }

        #endregion Propriétés non sérialisés
    }
}