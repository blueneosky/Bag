using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface ITacheTfsNotifiable<out TTicketTfs, out TImputationTfs> : ITacheTfs<TTicketTfs, TImputationTfs>, IInformationTacheTfsNotifiable
        where TTicketTfs : ITicketTfsNotifiable<TImputationTfs>
        where TImputationTfs : IImputationTfsNotifiable
    {
        #region Evennements

        event NotifyCollectionChangedEventHandler TicketTfssAChange;

        #endregion Evennements
    }
}