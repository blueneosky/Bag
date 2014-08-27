using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ImputationH31per.Util;

namespace ImputationH31per.Vue.EditeurImputationTfs.Modele
{
    public abstract class EditeurImputationTfsFormModeleBase : IEditeurImputationTfsFormModele
    {
        #region Membres

        private bool _estNumeroImputationTfsModifiable;
        private ImputationTfsDataEditeur _imputationTfs;

        #endregion Membres

        #region ctor

        protected EditeurImputationTfsFormModeleBase()
        {
            ImputationTfs = ImputationTfsDataEditeur.Vide;
        }

        #endregion ctor

        #region Propriétés

        public bool EstNumeroImputationTfsModifiable
        {
            get { return _estNumeroImputationTfsModifiable; }
            set
            {
                _estNumeroImputationTfsModifiable = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIEditeurImputationTfsFormModele.ConstanteProprieteEstNumeroImputationTfsModifiable));
            }
        }

        public ImputationTfsDataEditeur ImputationTfs
        {
            get { return _imputationTfs; }
            protected set
            {
                _imputationTfs = value;
                NotifierPropertyChanged(this, new PropertyChangedEventArgs(ConstanteIEditeurImputationTfsFormModele.ConstanteProprieteImputationTfs));
            }
        }

        public abstract IEnumerable<string> ObtenirChoixNomGroupements();

        public abstract IEnumerable<int?> ObtenirChoixNumeroComplementaires();

        #endregion Propriétés

        #region Evennements

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifierPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged.Notifier(this, e);
        }

        #endregion Evennements
    }
}