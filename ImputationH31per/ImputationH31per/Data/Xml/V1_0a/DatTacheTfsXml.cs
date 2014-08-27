using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Data.Xml.V1_0a
{
    [Serializable]
    [XmlType("aTac")]
    public class DatTacheTfsXml
    {
        internal DatTacheTfsXml(V0_9999.DatTacheTfsXml oldTacheTfs)
        {
            this.Numero = oldTacheTfs.Numero;

            this.Nom = oldTacheTfs.Nom;
            this.NomGroupement = oldTacheTfs.NomGroupement;

            XmlTicketTfss = oldTacheTfs.TicketTfss
                .Select(i => new DatTicketTfsXml(i, this))
                .ToArray();
        }

        /// <summary>
        /// Pour serialization
        /// </summary>
        private DatTacheTfsXml()
        {
        }

        #region Xml

        #region Propriétés scalaire

        [XmlAttribute("Nom")]
        public string XmlNom { get; set; }

        [XmlAttribute("NGr")]
        public string XmlNomGroupement { get; set; }

        [XmlAttribute("Num")]
        public int XmlNumero { get; set; }

        #endregion Propriétés scalaire

        #region Propriétés non scalaire

        [XmlElement("Tic")]
        public DatTicketTfsXml[] XmlTicketTfss { get; set; }

        #endregion Propriétés non scalaire

        #endregion Xml

        #region Propriétés non sérialisés

        [XmlIgnore]
        public string Nom
        {
            get { return XmlNom; }
            set { XmlNom = value ?? String.Empty; }
        }

        [XmlIgnore]
        public string NomGroupement
        {
            get { return XmlNomGroupement; }
            set { XmlNomGroupement = value ?? String.Empty; }
        }

        [XmlIgnore]
        public int Numero
        {
            get { return XmlNumero; }
            set { XmlNumero = value; }
        }

        [XmlIgnore]
        public IEnumerable<DatTicketTfsXml> TicketTfss
        {
            get { return XmlTicketTfss; }
        }

        #endregion Propriétés non sérialisés
    }
}