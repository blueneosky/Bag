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
    public class DatTicketTfsXml
    {
        #region Membres

        private bool _estNumeroComplementaireEnCache;
        private int? _numeroComplementaireCache;

        #endregion Membres

        // Neutralisé
        //public DatTicketTfsXml(IDatTicketTfs ticketTfs, DatTacheTfsXml tacheTfs)
        //{
        //    this.NumeroComplementaire = ticketTfs.NumeroComplementaire;

        //    this.DefinirProprietesTicketTfs(ticketTfs);

        //    ArrayImputationTfss = ticketTfs.ImputationTfss
        //        .Select(i => new DatImputationTfsXml(i, this))
        //        .ToArray();

        //    TacheTfs = tacheTfs;
        //}

        /// <summary>
        /// Pour serialization
        /// </summary>
        private DatTicketTfsXml()
        {
        }

        #region Propriétés scalaire

        public bool EstTacheAvecEstim { get; set; }

        public string NomComplementaire { get; set; }

        [XmlAttribute("NumComp")]
        public string StringNumeroComplementaire { get; set; }

        #endregion Propriétés scalaire

        #region Propriétés non scalaire

        public DatImputationTfsXml[] ArrayImputationTfss { get; set; }

        #endregion Propriétés non scalaire

        #region Propriétés non sérialisés

        [XmlIgnore]
        public IEnumerable<DatImputationTfsXml> ImputationTfss
        {
            get { return ArrayImputationTfss; }
        }

        [XmlIgnore]
        public string Nom
        {
            get { return TacheTfs.Nom; }
            set { throw new IHException("Non permis"); }
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
            get
            {
                if (false == _estNumeroComplementaireEnCache)
                {
                    int resultat;
                    bool succes = Int32.TryParse(StringNumeroComplementaire, out resultat);

                    _numeroComplementaireCache = succes ? resultat : (int?)null;
                    _estNumeroComplementaireEnCache = true;
                }

                return _numeroComplementaireCache;
            }
            set
            {
                _estNumeroComplementaireEnCache = false;
                StringNumeroComplementaire = String.Empty + value;
            }
        }

        [XmlIgnore]
        public DatTacheTfsXml TacheTfs { get; set; }

        #endregion Propriétés non sérialisés
    }
}