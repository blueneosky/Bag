using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Modele.Entite
{
    public abstract class TacheTfsBase<TTicketTfs, TImputationTfs> : InformationTacheTfsBase, ITacheTfsNotifiable<TTicketTfs, TImputationTfs>
        where TTicketTfs : TicketTfsBase<TImputationTfs>
        where TImputationTfs : ImputationTfsBase
    {
        #region ctor

        protected TacheTfsBase(int numero)
            : base(numero)
        {
        }

        #endregion ctor

        #region Evennement

        public event NotifyCollectionChangedEventHandler TicketTfssAChange;

        protected virtual void NotifierTicketTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            TicketTfssAChange.Notifier(sender, e);
        }

        #endregion Evennement

        #region Propriétés

        public abstract IEnumerable<TTicketTfs> TicketTfss { get; }

        #endregion Propriétés

        #region Méthodes

        public abstract void AjouterTicketTfs(TTicketTfs ticketTfs);

        public abstract bool SupprimerTicketTfs(int? numeroComplementaire);

        public abstract void ViderTicketTfss();

        #endregion Méthodes
    }
}