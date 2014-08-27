using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface IInformationTicketTfs : IInformationTacheTfs
    {
        #region Propriétés

        #region Identification

        int? NumeroComplementaire { get; }

        #endregion Identification

        bool EstTacheAvecEstim { get; set; }

        string NomComplementaire { get; set; }

        #endregion Propriétés
    }
}