using System;
using System.Collections.Generic;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface ITacheTfs<out TTicketTfs, out TImputationTfs> : IInformationTacheTfs
        where TTicketTfs : ITicketTfs<TImputationTfs>
        where TImputationTfs : IImputationTfs
    {
        #region Propriétés

        IEnumerable<TTicketTfs> TicketTfss { get; }

        #endregion Propriétés
    }
}