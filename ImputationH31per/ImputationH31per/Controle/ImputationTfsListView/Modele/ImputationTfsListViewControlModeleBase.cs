using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Modele.Entite;
using ImputationH31per.Util;

namespace ImputationH31per.Controle.ImputationTfsListView.Modele
{
#warning TODO - point ZETA - mettre en place le mécanisme de triage par colonne; exploiter ComparateurTriAffichage + 2 autre prop pour colonne et croissant/dec ainsi qu'une fonction de la couche controleur pour lancer le changement;

    public abstract class ImputationTfsListViewControlModeleBase : IImputationTfsListViewControlModele
    {
        #region Membres

        private IComparer<IImputationTfsNotifiable> _comparateurTriAffichage;
        private bool _estImputationTfsModifiable;
        private bool _estImputationTfsSupprimable;

        #endregion Membres

        #region Propriétés

        public IComparer<IImputationTfsNotifiable> ComparateurTriAffichage
        {
            get { return _comparateurTriAffichage; }
            set
            {
                if (_comparateurTriAffichage == value)
                    return;
                _comparateurTriAffichage = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIImputationTfsListViewControlModele.ConstanteProprieteComparateurTriAffichage));
            }
        }

        public bool EstImputationTfsModifiable
        {
            get { return _estImputationTfsModifiable; }
            protected set
            {
                _estImputationTfsModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIImputationTfsListViewControlModele.ConstanteProprieteEstImputationTfsModifiable));
            }
        }

        public bool EstImputationTfsSupprimable
        {
            get { return _estImputationTfsSupprimable; }
            protected set
            {
                _estImputationTfsSupprimable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIImputationTfsListViewControlModele.ConstanteProprieteEstImputationTfsSupprimable));
            }
        }

        public abstract IEnumerable<IImputationTfsNotifiable> ImputationTfss { get; }

        #endregion Propriétés

        #region Evennements

        public event NotifyCollectionChangedEventHandler ImputationTfssAChange;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifierImputationTfssAChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            ImputationTfssAChange.Notifier(sender, e);
        }

        protected void NotifierPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notifier(sender, e);
        }

        #endregion Evennements
    }
}