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
    [XmlType("bImp")]
    public class DatImputationTfsXml : IDatImputationTfs
    {
        #region ctor

        public DatImputationTfsXml(IDatImputationTfs imputationTfs, DatTicketTfsXml ticketTfs)
        {
            DateHorodatage = imputationTfs.DateHorodatage;

            this.DefinirProprietesImputationTfs(imputationTfs);

            TicketTfs = ticketTfs;
        }

        internal DatImputationTfsXml(V1_0a.DatImputationTfsXml oldImputationTfs, DatTicketTfsXml ticketTfs)
        {
            DateHorodatage = oldImputationTfs.DateHorodatage;

            this.DateEstimCourant = oldImputationTfs.DateEstimCourant;
            this.DateSommeConsommee = oldImputationTfs.DateSommeConsommee;
            this.EstimCourant = oldImputationTfs.EstimCourant;
            this.SommeConsommee = oldImputationTfs.SommeConsommee;
            this.Commentaire = null;

            TicketTfs = ticketTfs;
        }

        /// <summary>
        /// Pour sérialization
        /// </summary>
        private DatImputationTfsXml()
        {
        }

        #endregion ctor

        #region Xml

        #region Propriétés scalaire

        [XmlAttribute("DHo")]
        public string Xml00DateHorodatage { get; set; }

        [XmlAttribute("DEs")]
        public string Xml01DateEstimCourant { get; set; }

        [XmlAttribute("DCo")]
        public string Xml02DateSommeConsommee { get; set; }

        [XmlAttribute("Es")]
        public string Xml03EstimCourant { get; set; }

        [XmlAttribute("Con")]
        public string Xml04SommeConsommee { get; set; }

        [XmlAttribute("Com")]
        public string Xml05Commentaire { get; set; }

        #endregion Propriétés scalaire

        #endregion Xml

        #region Propriétés non sérialisés

        [XmlIgnore]
        public string Commentaire
        {
            get { return Xml05Commentaire; }
            set { Xml05Commentaire = value; }
        }

        [XmlIgnore]
        public DateTimeOffset? DateEstimCourant
        {
            get { return XmlSerialization.ConvertirDateTime(Xml01DateEstimCourant); }
            set { Xml01DateEstimCourant = XmlSerialization.Convertir(value); }
        }

        [XmlIgnore]
        public DateTimeOffset DateHorodatage
        {
            get { return XmlSerialization.ConvertirDateTime(Xml00DateHorodatage).Value; }
            set { Xml00DateHorodatage = XmlSerialization.Convertir(value); }
        }

        [XmlIgnore]
        public DateTimeOffset? DateSommeConsommee
        {
            get { return XmlSerialization.ConvertirDateTime(Xml02DateSommeConsommee); }
            set { Xml02DateSommeConsommee = XmlSerialization.Convertir(value); }
        }

        [XmlIgnore]
        public double? EstimCourant
        {
            get { return XmlSerialization.ConvertirDouble(Xml03EstimCourant); }
            set { Xml03EstimCourant = XmlSerialization.Convertir(value); }
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
        public double? SommeConsommee
        {
            get { return XmlSerialization.ConvertirDouble(Xml04SommeConsommee); }
            set { Xml04SommeConsommee = XmlSerialization.Convertir(value); }
        }

        [XmlIgnore]
        public DatTicketTfsXml TicketTfs { get; set; }

        #endregion Propriétés non sérialisés
    }
}