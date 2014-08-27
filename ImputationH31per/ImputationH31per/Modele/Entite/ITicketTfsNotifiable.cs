using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace ImputationH31per.Modele.Entite
{
    public interface ITicketTfsNotifiable<out TImputationTfs> : ITicketTfs<TImputationTfs>, IInformationTicketTfsNotifiable
        where TImputationTfs : IImputationTfsNotifiable
    {
        #region Evennements

        event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        #endregion Evennements
    }
}