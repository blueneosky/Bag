using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Modele.Entite
{
    public abstract class TicketTfsBase<TImputationTfs> : InformationTicketTfsBase, ITicketTfsNotifiable<TImputationTfs>
        where TImputationTfs : ImputationTfsBase
    {
        #region ctor

        protected TicketTfsBase(IInformationTacheTfs tacheTfs, int? numeroComplementaire)
            : this(tacheTfs.Numero, numeroComplementaire)
        {
        }

        protected TicketTfsBase(int numero, int? numeroComplementaire)
            : base(numero, numeroComplementaire)
        {
        }

        #endregion ctor

        #region Evennement

        public event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        protected virtual void NotifierImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            ImputationTfssAChange.Notifier(sender, e);
        }

        #endregion Evennement

        #region Propriétés

        public abstract IEnumerable<TImputationTfs> ImputationTfss { get; }

        #endregion Propriétés

        #region Méthodes

        public abstract void AjouterImputationTfs(TImputationTfs imputationTfs);

        public abstract bool SupprimerImputationTfs(DateTimeOffset dateHorodatage);

        public abstract void ViderImputationTfss();

        #endregion Méthodes
    }
}