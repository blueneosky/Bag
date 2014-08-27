using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Modele.Entite;

namespace ImputationH31per.Controle.ImputationTfsListView.Modele
{
    public interface IImputationTfsListViewControlModele : INotifyPropertyChanged
    {
        #region Propriétés

        IComparer<IImputationTfsNotifiable> ComparateurTriAffichage { get; set; }

        bool EstImputationTfsModifiable { get; }

        bool EstImputationTfsSupprimable { get; }

        IEnumerable<IImputationTfsNotifiable> ImputationTfss { get; }

        #endregion Propriétés

        #region Evennements

        event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        #endregion Evennements
    }
}