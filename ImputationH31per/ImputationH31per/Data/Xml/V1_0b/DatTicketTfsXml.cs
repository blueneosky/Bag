using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Data.Xml.V1_0b
{
    [Serializable]
    [XmlType("bTic")]
    public class DatTicketTfsXml : IDatTicketTfs
    {
        public DatTicketTfsXml(IDatTicketTfs ticketTfs, DatTacheTfsXml tacheTfs)
        {
            this.NumeroComplementaire = ticketTfs.NumeroComplementaire;

            this.DefinirProprietesTicketTfs(ticketTfs);

            XmlImputationTfss = ticketTfs.ImputationTfss
                .Select(i => new DatImputationTfsXml(i, this))
                .ToArray();

            TacheTfs = tacheTfs;
        }

        internal DatTicketTfsXml(V1_0a.DatTicketTfsXml oldTicketTfs, DatTacheTfsXml tacheTfs)
        {
            this.NumeroComplementaire = oldTicketTfs.NumeroComplementaire;

            this.EstTacheAvecEstim = oldTicketTfs.EstTacheAvecEstim;
            this.NomComplementaire = oldTicketTfs.NomComplementaire;

            XmlImputationTfss = oldTicketTfs.ImputationTfss
                .Select(i => new DatImputationTfsXml(i, this))
                .ToArray();

            TacheTfs = tacheTfs;
        }

        /// <summary>
        /// Pour serialization
        /// </summary>
        private DatTicketTfsXml()
        {
        }

        #region Xml

        #region Propriétés scalaire

        [XmlAttribute("AEs")]
        public int XmlEstTacheAvecEstim { get; set; }

        [XmlAttribute("Nom")]
        public string XmlNomComplementaire { get; set; }

        [XmlAttribute("Num")]
        public string XmlNumeroComplementaire { get; set; }

        #endregion Propriétés scalaire

        #region Propriétés non scalaire

        [XmlElement("Imp")]
        public DatImputationTfsXml[] XmlImputationTfss { get; set; }

        #endregion Propriétés non scalaire

        #endregion Xml

        #region Propriétés non sérialisés

        [XmlIgnore]
        public bool EstTacheAvecEstim
        {
            get { return XmlEstTacheAvecEstim == 1; }
            set { XmlEstTacheAvecEstim = value ? 1 : 0; }
        }

        [XmlIgnore]
        public IEnumerable<IDatImputationTfs> ImputationTfss
        {
            get { return XmlImputationTfss; }
        }

        [XmlIgnore]
        public string Nom
        {
            get { return TacheTfs.Nom; }
            set { throw new IHException("Non permis"); }
        }

        [XmlIgnore]
        public string NomComplementaire
        {
            get { return XmlNomComplementaire; }
            set { XmlNomComplementaire = value ?? String.Empty; }
        }

        [XmlIgnore]
        public string NomGroupement
        {
            get { return TacheTfs.NomGroupement; }
            set { throw new IHException("Non permis"); }
        }

        [XmlIgnore]
        public int Numero
        {
            get { return TacheTfs.Numero; }
            set { throw new IHException("Non permis"); }
        }

        [XmlIgnore]
        public int? NumeroComplementaire
        {
            get { return XmlSerialization.ConvertirInt(XmlNumeroComplementaire); }
            set { XmlNumeroComplementaire = XmlSerialization.Convertir(value); }
        }

        #region Properties

        [XmlIgnore]
        public DatTacheTfsXml TacheTfs { get; set; }

        #endregion Properties

        #endregion Propriétés non sérialisés
    }
}