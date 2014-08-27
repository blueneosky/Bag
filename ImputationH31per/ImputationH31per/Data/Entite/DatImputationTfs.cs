using System;
using System.Collections.Generic;
using System.Linq;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Data.Entite
{
    public class DatImputationTfs : IDatImputationTfs
    {
        #region ctor

        public DatImputationTfs(IImputationTfs imputationTfs)
        {
            Numero = imputationTfs.Numero;
            NumeroComplementaire = imputationTfs.NumeroComplementaire;
            DateHorodatage = imputationTfs.DateHorodatage;

            this.DefinirProprietes(imputationTfs);
        }

        #endregion ctor

        #region Propriétés

        public DateTimeOffset? DateEstimCourant { get; set; }

        public DateTimeOffset DateHorodatage { get; set; }

        public DateTimeOffset? DateSommeConsommee { get; set; }

        public double? EstimCourant { get; set; }

        public bool EstTacheAvecEstim { get; set; }

        public string Nom { get; set; }

        public string NomComplementaire { get; set; }

        public string NomGroupement { get; set; }

        public int Numero { get; set; }

        public int? NumeroComplementaire { get; set; }

        public double? SommeConsommee { get; set; }

        public string Commentaire { get; set; }

        #endregion Propriétés
    }
}