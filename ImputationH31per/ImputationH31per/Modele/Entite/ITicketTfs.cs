using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface ITicketTfs<out TImputationTfs> : IInformationTicketTfs
        where TImputationTfs : IImputationTfs
    {
        #region Propriétés

        IEnumerable<TImputationTfs> ImputationTfss { get; }

        #endregion Propriétés
    }
}