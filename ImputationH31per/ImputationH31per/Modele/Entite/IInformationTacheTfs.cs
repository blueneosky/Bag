using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface IInformationTacheTfs
    {
        #region Propriétés

        #region Identification

        int Numero { get; }

        #endregion Identification

        string Nom { get; set; }

        string NomGroupement { get; set; }

        #endregion Propriétés
    }
}