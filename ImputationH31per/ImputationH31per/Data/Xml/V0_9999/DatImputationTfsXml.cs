using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ImputationH31per.Data.Entite;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Data.Xml.V0_9999
{
    [Serializable]
    public class DatImputationTfsXml
    {
        #region ctor

        // Neutralisé
        //public DatImputationTfsXml(IDatImputationTfs imputationTfs, DatTicketTfsXml ticketTfs)
        //{
        //    DateHorodatage = imputationTfs.DateHorodatage;

        //    this.DefinirProprietesImputationTfs(imputationTfs);

        //    TicketTfs = ticketTfs;
        //}

        /// <summary>
        /// Pour sérialization
        /// </summary>
        private DatImputationTfsXml()
        {
        }

        #endregion ctor

        #region Propriétés scalaire

        [XmlElement("DateEstimCourant")]
        public DateTime? DTDateEstimCourant { get; set; }

        [XmlAttribute("DateHorodatage")]
        public DateTime DTDateHorodatage { get; set; }

        [XmlElement("DateSommeConsommee")]
        public DateTime? DTDateSommeConsommee { get; set; }

        public double? EstimCourant { get; set; }

        public double? SommeConsommee { get; set; }

        #endregion Propriétés scalaire

        #region Propriétés non sérialisés

        [XmlIgnore]
        public DateTimeOffset? DateEstimCourant
        {
            get { return DTDateEstimCourant.AsDateTimeOffset(); }
            set { DTDateEstimCourant = value.UtcDateTime(); }
        }

        [XmlIgnore]
        public DateTimeOffset DateHorodatage
        {
            get { return DTDateHorodatage.AsDateTimeOffset(); }
            set { DTDateHorodatage = value.UtcDateTime(); }
        }

        [XmlIgnore]
        public DateTimeOffset? DateSommeConsommee
        {
            get { return DTDateSommeConsommee.AsDateTimeOffset(); }
            set { DTDateSommeConsommee = value.UtcDateTime(); }
        }

        [XmlIgnore]
        public bool EstTacheAvecEstim
        {
            get { return TicketTfs.EstTacheAvecEstim; }
            set { throw new IHException("Non permis"); }
        }

        [XmlIgnore]
        public string Nom
        {
            get { return TicketTfs.Nom; }
            set { throw new IHException("Non permis"); }
        }

        [XmlIgnore]
        public string NomComplementaire
        {
            get { return TicketTfs.NomComplementaire; }
            set { throw new IHException("Non permis"); }
        }

        [XmlIgnore]
        public string NomGroupement
        {
            get { return TicketTfs.NomGroupement; }
            set { throw new IHException("Non permis"); }
        }

        [XmlIgnore]
        public int Numero
        {
            get { return TicketTfs.Numero; }
        }

        [XmlIgnore]
        public int? NumeroComplementaire
        {
            get { return TicketTfs.NumeroComplementaire; }
        }

        [XmlIgnore]
        public DatTicketTfsXml TicketTfs { get; set; }

        #endregion Propriétés non sérialisés
    }
}