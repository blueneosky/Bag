using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface IInformationImputationTfs : IInformationTicketTfs
    {
        #region Propriétés

        #region Identification

        DateTimeOffset DateHorodatage { get; }

        #endregion Identification

        DateTimeOffset? DateEstimCourant { get; set; }

        DateTimeOffset? DateSommeConsommee { get; set; }

        double? EstimCourant { get; set; }

        double? SommeConsommee { get; set; }

        string Commentaire { get; set; }

        #endregion Propriétés
    }
}